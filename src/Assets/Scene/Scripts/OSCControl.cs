using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityOSC;

//Class to read  in from OSC and change Textures
public class OSCControl : MonoBehaviour {
	//variables for OSC
	private OSCReciever reciever;
    private OSCClient client;
    
    //public variables for OSC connection
    public int port = 8338;
    public string ipaddress = "10.0.0.1";
    
    //private variable for controlling the project
    private FacetrackingWithUVs kinectControler;
    
    
    //bool for sending data
    private bool connected = false;
    private bool tracking = false;
    private int numberTracking = 0;
    private int personIndex = 0;

    // Use this for initialization
    void Start()
    {
        //start receiver
        reciever = new OSCReciever();

        //try to connect to default client
        try
        {
            client = new OSCClient(IPAddress.Parse(ipaddress),port);
            connected = true;
        } catch (Exception e){
            Debug.Log(e);
        }

        //open port to listen
        reciever.Open(port);
        kinectControler = GetComponent<FacetrackingWithUVs>();
        Debug.Log("Started OSC");
    }

    // Update is called once per frame
    void Update()
    {
        //if connected to server send message
        if(connected && tracking != kinectControler.IsTrackingFace())
        {
            int output = 0;
            if (kinectControler.IsTrackingFace())
            {
                output = 1;
            }
            client.Send(new OSCMessage("/osc/tracking", output));
            tracking = kinectControler.IsTrackingFace();
        }

        if (connected && numberTracking != kinectControler.getCount())
        {
            client.Send(new OSCMessage("/osc/users", kinectControler.getCount()));
            numberTracking = kinectControler.getCount();
        }

        if (connected && personIndex != kinectControler.playerIndex)
        {
            client.Send(new OSCMessage("/osc/current", kinectControler.playerIndex));
            personIndex = kinectControler.playerIndex;
        }


        //if there is a message
        if (reciever.hasWaitingMessages())
        {
            OSCMessage message = reciever.getNextMessage();
            string value = string.Format(DataToString(message.Data));

            //read mask messages
            if(message.Address == "/osc/mask")
            {
                int texture = 0;
                if(Int32.TryParse(value, out texture))
                {
                    if(texture < 0)
                    {
                        kinectControler.nextTexture();
                    } else
                    {
                        kinectControler.nextTexture(texture);
                    }
                } else
                {
                    kinectControler.nextTexture();
                }
                
            }

            //read display messages
            if (message.Address == "/osc/display")
            {
                int displayValue = 0;
                if (Int32.TryParse(value, out displayValue))
                {
                    if (displayValue == 1)
                    {
                        kinectControler.setDisplay(true);
                    } else
                    {
                        kinectControler.setDisplay(false);
                    }
                }
            }

            if (message.Address == "/osc/person")
            {
                int person = 0;
                if (Int32.TryParse(value, out person))
                {
                    if (person < 0)
                    {
                        kinectControler.nextPerson();
                    }
                    else
                    {
                        kinectControler.nextPerson(person);
                    }
                }
                else
                {
                    kinectControler.nextPerson();
                }
            }

            if (message.Address == "/osc/connect")
            {
                IPAddress ip;
                try
                {
                    ip = IPAddress.Parse(value);
                    client = new OSCClient(ip, port);
                    ipaddress = value;
                    connected = true;
                } catch (Exception exception)
                {
                    Debug.Log(exception) ;
                } 
            }

            if (message.Address == "/osc/status")
            {
                if (connected)
                {
                    client.Send(new OSCMessage("/osc/status", kinectControler.IsTrackingFace()));
                }
            }
        }
    }
    private void OnDestroy()
    {
        reciever.Close();
    }

    private string DataToString(List<object> data)
    {
        string buffer = "";

        for (int i = 0; i < data.Count; i++)
        {
            buffer += data[i].ToString() + " ";
        }

        buffer += "\n";

        return buffer;
    }

}

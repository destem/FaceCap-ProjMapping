using System;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;

//Class to read  in from OSC and change Textures
public class OSCControl : MonoBehaviour {
	
	private OSCReciever reciever;

	public int port = 8338;


    private FacetrackingWithUVs kinectControler;



    // Use this for initialization
    void Start()
    {
        reciever = new OSCReciever();
        reciever.Open(port);
        kinectControler = GetComponent<FacetrackingWithUVs>();
        Debug.Log("Started OSC");
    }

    // Update is called once per frame
    void Update()
    {
        //if message avaliable
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
                    kinectControler.nextTexture(texture);
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

        }
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
    private void OnDestroy()
    {
        reciever.Close();
    }
}

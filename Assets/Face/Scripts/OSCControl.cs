using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;

//class to handel the storage of data and increase readability
public class OSCData {
	public string data;


	public OSCData(){
		
	}
}

//Class to read  in from OSC and change Textures
public class OSCControl : MonoBehaviour {
	
	private OSCReciever reciever;

	public int port = 8338;
	
	[Tooltip("Game object that will be used to display the HD-face model mesh in the scene.")]
    public GameObject faceModelMesh = null;

    private GameObject kinectControler;


	// Use this for initialization
	void Start () {
		reciever = new OSCReciever();
		reciever.Open(port);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(reciever.hasWaitingMessages()){
			OSCMessage msg = reciever.getNextMessage();
			Debug.Log(string.Format("message received: {0} {1}", msg.Address, DataToString(msg.Data)));
		}
	}
	
	private string DataToString(List<object> data)
	{
		string buffer = "";
		
		for(int i = 0; i < data.Count; i++)
		{
			buffer += data[i].ToString() + " ";
		}
		
		buffer += "\n";
		
		return buffer;
	}
}

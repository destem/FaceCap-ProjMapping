using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LightSwitch(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LightSwitch(bool b)  {
		this.gameObject.GetComponent<Light>().enabled = b;
	}
}

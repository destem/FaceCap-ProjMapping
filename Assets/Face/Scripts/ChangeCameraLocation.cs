using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraLocation : MonoBehaviour {

	public float positionOffset;
	public float rotationOffset;
	private Vector3 offsetx;
	private Vector3 offsety;
	private Vector3 offsetz;
	
	// Use this for initialization
	void Start () {
		offsetx = new Vector3(positionOffset,0,0);
		offsety = new Vector3(0,positionOffset,0);
		offsetz = new Vector3(0,0,positionOffset);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("a"))
        {
        	transform.position = transform.position+offsetx;
        }

        if (Input.GetKey("d"))
        {
        	transform.position = transform.position-offsetx;
        }
        if (Input.GetKey("w"))
        {
        	transform.position = transform.position+offsetz;
        }

        if (Input.GetKey("s"))
        {
        	transform.position = transform.position-offsetz;
        }
        if (Input.GetKey("z"))
        {
        	transform.position = transform.position+offsety;
        }

        if (Input.GetKey("x"))
        {
        	transform.position = transform.position-offsety;
        }
        if (Input.GetKey("q"))
        {		
        	transform.Rotate(0,0,-rotationOffset);
        }
        if (Input.GetKey("e"))
        {
        	transform.Rotate(0,0,rotationOffset);
        }
	}
}

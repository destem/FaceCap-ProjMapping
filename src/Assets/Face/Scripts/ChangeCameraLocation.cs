using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraLocation : MonoBehaviour {
    //public variables for change
    public float positionOffset;
    public float rotationOffset;
    public GameObject KinectManager;
    private FacetrackingWithUVs manager;

    public UnityEngine.UI.Text cameraText;


    //kinect controller

    //offset variables
    private Vector3 offsetx;
    private float offsety;
    private Vector3 offsetz;

        // Use this for initialization
    void Start () {
    	manager = KinectManager.GetComponent<FacetrackingWithUVs>();
        offsetx = new Vector3(positionOffset,0,0);
        offsety = positionOffset;
        offsetz = new Vector3(0,0,positionOffset);
        cameraText.text = transform.position.ToString();
    }
        
    // Update is called once per frame
    void Update () {
        //if text needs to be updated
        bool update = false;
        
        //listen for key inputs        
        if (Input.GetKey("a"))
        {
            transform.position = transform.position+offsetx;
            update = true;
        }

        if (Input.GetKey("d"))
        {
            transform.position = transform.position-offsetx;
            update = true;
        }
        if (Input.GetKey("w"))
        {
            transform.position = transform.position+offsetz;
            update = true;
        }

        if (Input.GetKey("s"))
        {
            transform.position = transform.position-offsetz;
            update = true;
        }
        if (Input.GetKey("r"))
        {
        	manager.setHeight(manager.getHeight() + offsety);
        	update = true;
        }
        if (Input.GetKey("f"))
        {
        	manager.setHeight(manager.getHeight() - offsety);
        	update = true;
        }
        if (Input.GetKey("z"))
        {
            transform.Rotate(0, rotationOffset/10, 0);
            update = true;
        }

        if (Input.GetKey("x"))
        {
            transform.Rotate(0, -rotationOffset/10, 0);
            update = true;
        }
        if (Input.GetKey("q"))
        {               
               transform.Rotate(0,0,-rotationOffset);
            update = true;
        }
        if (Input.GetKey("e"))
        {
            transform.Rotate(0,0,rotationOffset);
            update = true;
        }

        //update text
        if (update)
        {
            cameraText.text = transform.position.ToString() + transform.rotation.ToString();
            
        }

    }
}

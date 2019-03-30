using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {
    //public variables for change
    public float positionOffset;
    public float rotationOffset;
    public float brightnessOffset;

    public GameObject KinectManager;
    private FacetrackingWithUVs manager;

    public UnityEngine.UI.Text cameraText;


    //kinect controller

    //offset variables
    private float offsetx;
    private float offsety;
    private float offsetz;

        // Use this for initialization
    void Start () {
    	manager = KinectManager.GetComponent<FacetrackingWithUVs>();
        offsetx = positionOffset;
        offsety = positionOffset;
        offsetz = positionOffset;
        cameraText.text = transform.position.ToString();
    }
        
    // Update is called once per frame
    void Update () {
        //if text needs to be updated
        bool update = false;
        
        //listen for key inputs        
        if (Input.GetKey("a"))
        {
            manager.setLateral(manager.getLateral() - offsetx);
            update = true;
        }

        if (Input.GetKey("d"))
        {
            manager.setLateral(manager.getLateral() + offsetx);
            update = true;
        }
        if (Input.GetKey("w"))
        {
            manager.setDepth(manager.getDepth() - offsetz);
            update = true;
        }

        if (Input.GetKey("s"))
        {
            manager.setDepth(manager.getDepth() + offsetz);
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
        if (Input.GetKey("o"))
        {
            manager.nextPerson();
            
        }
        if (Input.GetKey("p"))
        {
            manager.nextTexture();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            manager.changeDisplay();
        }
        if (Input.GetKey(KeyCode.Plus))
        {
            manager.setBrightness(manager.getBrightness() + brightnessOffset);
        }
        if (Input.GetKey(KeyCode.Minus))
        {
            manager.setBrightness(manager.getBrightness() - brightnessOffset);

        }
        // CODE TO SAVE VERTICIES 
        /*
        if (Input.GetKeyDown (KeyCode.Q)) {
        	ObjExporter.MeshToFile (GetFaceModelVertices(), GetFaceModelUV(), GetFaceModelTriangleIndices(false), Application.persistentDataPath + "/derp.obj");
        	print ("Saved to: " + Application.persistentDataPath + "/derp.obj");
        	print("Verts:");
        	print (GetFaceModelVertices().Length);
        	print ("UVs");
        	print (GetFaceModelUV ().Length);
        	print ("indices");
        	print (GetFaceModelTriangleIndices (false).Length);

        }*/


        //update text
        if (update)
        {
            cameraText.text = transform.position.ToString() + transform.rotation.ToString();
            
        }
        

    }
}

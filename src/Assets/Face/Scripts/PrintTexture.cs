using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PrintTexture : MonoBehaviour {
    public GameObject face;

    private Material faceMeshMaterial;
    // Use this for initialization
    void Start () {
		faceMeshMaterial =  face.GetComponent<MeshRenderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("t"))
        {
            Texture mainTexture = faceMeshMaterial.mainTexture;
            Texture2D tex = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
            byte[] output = tex.EncodeToPNG();
            File.WriteAllBytes(Application.dataPath + "/../CaseyFace.png", output);

        }
    }
}

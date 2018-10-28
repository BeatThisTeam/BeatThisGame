using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProBuilder.Addons;
using ProBuilder.Core;
using ProBuilder.MeshOperations;

public class GroundColorChanger : MonoBehaviour {

    public pb_Object faces1;
    public pb_Object faces2;

    [ColorUsage(true, true)] public Color col1;
    [ColorUsage(true, true)] public Color col2;

    public Renderer rendererFaces1;
    public Renderer rendererFaces2;

    public Material red;
    public Material blue;

    private void Awake() {

        rendererFaces1 = faces1.GetComponent<Renderer>();
        rendererFaces2 = faces2.GetComponent<Renderer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //test
        if (Input.GetKeyDown(KeyCode.Mouse0)) {

            ChangeColor();
        }
    }

    void ChangeColor() {

        rendererFaces1.material = blue;
        rendererFaces2.material = red;

        Renderer temp = rendererFaces1;
        rendererFaces1 = rendererFaces2;
        rendererFaces2 = temp;
    }
}

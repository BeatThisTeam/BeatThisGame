using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProBuilder.Addons;
using ProBuilder.Core;
using ProBuilder.MeshOperations;

public class GroundColorChanger : MonoBehaviour {

    //public pb_Object faces1;
    //public pb_Object faces2;

    [System.Serializable]
    public class Ring {
        public List<pb_Object> faces;
    }

    public List<Ring> rings = new List<Ring>();

    [ColorUsage(true, true)] public Color col1;
    [ColorUsage(true, true)] public Color col2;

    //public Renderer rendererFaces;
    //public Renderer rendererFaces2;

    public Material red;
    public Material blue;
	
	// Update is called once per frame
	void Update () {

        //test
        if (Input.GetKeyDown(KeyCode.Mouse0)) {

            //ChangeColor(0);

            CharacterController player = FindObjectOfType<CharacterController>();
            ChangeColor(player.transform.position.x, player.transform.position.z);
        }
    }

    /// <summary>
    /// Changes the color of an entire ring
    /// </summary>
    /// <param name="ringIndex">index of the ring, 0 is the inner one</param>
    void ChangeColor(int ringIndex) {

        if (ringIndex < rings.Count) {

            for(int i = 0; i < rings[ringIndex].faces.Count; i++) {
                Renderer rendererFaces;
                rendererFaces = rings[ringIndex].faces[i].GetComponent<Renderer>();

                if (rendererFaces.sharedMaterial == blue) {
                    rendererFaces.material = red;
                } else {
                    rendererFaces.material = blue;
                }
            }
                       
        } else {
            Debug.LogError("ERROR: ringIndex out of bound");
        }
    }

    /// <summary>
    /// Changes the color of the faces under the player
    /// </summary>
    /// <param name="x">player x coordinate</param>
    /// <param name="z">player z coordinate</param>
    void ChangeColor(float x, float z) {

        for(int i = 0; i < rings.Count; i++) {
            for (int j = 0; j < rings[i].faces.Count; j++) {

                if(Vector2.Distance(new Vector2(rings[i].faces[j].transform.position.x, rings[i].faces[j].transform.position.z), new Vector2(x,z)) < 0.5){
                    Renderer rendererFaces;
                    rendererFaces = rings[i].faces[j].GetComponent<Renderer>();
                    if (rendererFaces.sharedMaterial == blue) {
                        rendererFaces.material = red;
                    } else {
                        rendererFaces.material = blue;
                    }
                }
            }
        }
    }
}

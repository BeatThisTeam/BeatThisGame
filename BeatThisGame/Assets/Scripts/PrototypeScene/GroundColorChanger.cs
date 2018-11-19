using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProBuilder.Addons;
using ProBuilder.Core;
using ProBuilder.MeshOperations;

public class GroundColorChanger : MonoBehaviour
{

    public GroundSections groundSections;

    [ColorUsage(true, true)] public Color col1;
    [ColorUsage(true, true)] public Color col2;

    public Material red;
    public Material blue;

    void Update()
    {

        //test
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            //ChangeColor(0);
            //ChangeColor(1);
            //ChangeColor(2);

            CharacterController player = FindObjectOfType<CharacterController>();
            ChangeColor(player.ringIndex, player.faceIndex);
        }
    }

    /// <summary>
    /// Changes the color of an entire ring
    /// </summary>
    /// <param name="ringIndex">index of the ring, 0 is the inner one</param>
    void ChangeColor(int ringIndex)
    {

        if (ringIndex < groundSections.rings.Count)
        {

            for (int i = 0; i < groundSections.rings[ringIndex].faces.Count; i++)
            {
                Renderer rendererFaces;
                rendererFaces = groundSections.rings[ringIndex].faces[i].GetComponent<Renderer>();

                if (rendererFaces.sharedMaterial == blue)
                {
                    rendererFaces.material = red;
                }
                else
                {
                    rendererFaces.material = blue;
                }
            }

        }
        else
        {
            Debug.LogError("ERROR: ringIndex out of bound");
        }
    }

    /// <summary>
    /// Changes the color of the faces under the player
    /// </summary>
    /// <param name="x">index of the ring</param>
    /// <param name="z">index of the face</param>
    public void ChangeColor(int ringIndex, int faceIndex)
    {

        if (ringIndex > groundSections.rings.Count)
        {

            Debug.LogError("ERROR: ringIndex out of bound");
            return;
        }

        if (faceIndex > groundSections.rings[ringIndex].faces.Count)
        {

            Debug.LogError("ERROR: faceIndex out of bound");
            return;
        }

        Renderer rendererFaces;
        rendererFaces = groundSections.rings[ringIndex].faces[faceIndex].GetComponent<Renderer>();
        if (rendererFaces.sharedMaterial == blue)
        {
            rendererFaces.material = red;
            //Debug.Log("red " + SongManager.Instance.SongPositionInBeats + " " + SongManager.Instance.SongPositionInBars);
        }
        else
        {
          //  Debug.Log("blue " + SongManager.Instance.SongPositionInBeats + " " + SongManager.Instance.SongPositionInBars);
            rendererFaces.material = blue;
        }
    }


    public void ChangeColorSlice(int faceIndex)
    {
        ChangeColor(0, faceIndex);
        ChangeColor(1, faceIndex);
        ChangeColor(2, faceIndex);


    }

    public void Red(int ringIndex, int faceIndex)
    {
        Renderer rendererFaces;
        rendererFaces = groundSections.rings[ringIndex].faces[faceIndex].GetComponent<Renderer>();
        if (rendererFaces.sharedMaterial == blue)
        {
            rendererFaces.material = red;
            
        }
    }

    public void AllRed(int faceIndex)
    {
        Red(0, faceIndex);
        Red(1, faceIndex);
        Red(2, faceIndex);
    }

    public void AllBlue(int faceIndex)
    {
        Blue(0, faceIndex);
        Blue(1, faceIndex);
        Blue(2, faceIndex);
    }

    public void Blue(int ringIndex, int faceIndex)
    {
        Renderer rendererFaces;
        rendererFaces = groundSections.rings[ringIndex].faces[faceIndex].GetComponent<Renderer>();
        if (rendererFaces.sharedMaterial == red)
        {
            rendererFaces.material = blue;

        }
    }

    
}


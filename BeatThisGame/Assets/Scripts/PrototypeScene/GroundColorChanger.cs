using UnityEngine;

public class GroundColorChanger : MonoBehaviour
{

    public GroundSections groundSections;

    [ColorUsage(true, true)] public Color col1;
    [ColorUsage(true, true)] public Color col2;

    public Material mat1;
    public Material mat2;
    public Material defaultMat;

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
    public void ChangeColor(int ringIndex) {

        if (ringIndex >= groundSections.rings.Count) {
            Debug.LogError("ERROR: ringIndex out of bound");
        }

        for (int i = 0; i < groundSections.rings[ringIndex].sections.Count; i++){
            ChangeColor(ringIndex, i);
        }
    }

    /// <summary>
    /// Changes the material of a ring
    /// </summary>
    /// <param name="ringIndex">index of the ring</param>
    /// <param name="mat">material to use</param>
    public void ChangeColor(int ringIndex, Material mat) {

        if (ringIndex >= groundSections.rings.Count) {
            Debug.LogError("ERROR: ringIndex out of bound");
            return;
        }

        for (int i = 0; i < groundSections.rings[ringIndex].sections.Count; i++) {
            ChangeColor(ringIndex, i, mat);
        }
    }

    /// <summary>
    /// Changes the material of a face between the two attached to this script
    /// </summary>
    /// <param name="x">index of the ring</param>
    /// <param name="z">index of the face</param>
    public void ChangeColor(int ringIndex, int faceIndex)
    {

        if (ringIndex >= groundSections.rings.Count)
        {

            Debug.LogError("ERROR: ringIndex out of bound");
            return;
        }

        if (faceIndex >= groundSections.rings[ringIndex].sections.Count)
        {

            Debug.LogError("ERROR: faceIndex out of bound");
            return;
        }

        Renderer rendererFaces;
        rendererFaces = groundSections.rings[ringIndex].sections[faceIndex].GetComponent<Renderer>();
        if (rendererFaces.sharedMaterial == mat2)
        {
            rendererFaces.material = mat1;
            //Debug.Log("red " + SongManager.Instance.SongPositionInBeats + " " + SongManager.Instance.SongPositionInBars);
        }
        else
        {
          //  Debug.Log("blue " + SongManager.Instance.SongPositionInBeats + " " + SongManager.Instance.SongPositionInBars);
            rendererFaces.material = mat2;
        }
    }

    /// <summary>
    /// Changes the material of a face with the given one
    /// </summary>
    /// <param name="ringIndex"></param>
    /// <param name="faceIndex"></param>
    /// <param name="mat"></param>
    public void ChangeColor(int ringIndex, int faceIndex, Material mat) {

        if (ringIndex >= groundSections.rings.Count)
        {

            Debug.LogError("ERROR: ringIndex out of bound");
            return;
        }

        if (faceIndex >= groundSections.rings[ringIndex].sections.Count)
        {

            Debug.LogError("ERROR: faceIndex out of bound");
            return;
        }

        Renderer rendererFaces;
        rendererFaces = groundSections.rings[ringIndex].sections[faceIndex].GetComponent<Renderer>();
        rendererFaces.material = mat;
    }

    public void ChangeColorSlice(int faceIndex, Material mat){

        if(faceIndex >= groundSections.rings[0].sections.Count) {
            Debug.LogError("ERROR: faceIndex out of bound");
            return;
        }

        for(int i = 0; i < groundSections.rings.Count; i++) {
            ChangeColor(i, faceIndex, mat);
        }
    }
}


using System.Collections;
using UnityEngine;

public class GroundColorChanger : MonoBehaviour
{

    public GroundSections groundSections;

    [ColorUsage(true, true)] public Color col1;
    [ColorUsage(true, true)] public Color col2;

    public string shader;
    public Material mat1;
    public Material mat2;
    public Material defaultMat;

    /// <summary>
    /// Alternates the value of the _Dissolve property of the material between the minimum and maximum
    /// </summary>
    /// <param name="x">index of the ring</param>
    /// <param name="z">index of the face</param>
    public void ChangeColor(int ringIndex, int faceIndex){

        if (ringIndex >= groundSections.rings.Count){

            Debug.LogError("ERROR: ringIndex out of bound");
            return;
        }

        if (faceIndex >= groundSections.rings[ringIndex].sections.Count){

            Debug.LogError("ERROR: faceIndex out of bound");
            return;
        }

        Renderer rendererFaces;
        rendererFaces = groundSections.rings[ringIndex].sections[faceIndex].GetComponent<Renderer>();
        rendererFaces.material.shader = Shader.Find(shader);

        float value = rendererFaces.material.GetFloat("_Dissolve");


        if (value <= 0) {
            rendererFaces.material.SetFloat("_Dissolve", 0.8f);
        } else {
            rendererFaces.material.SetFloat("_Dissolve", 0f);
        }
       
    }

    /// <summary>
    /// changes the given face, if hurts is true it applies the maximum value of the Dissolve property of the shader
    /// </summary>
    /// <param name="ringIndex"></param>
    /// <param name="faceIndex"></param>
    /// <param name="hurts"></param>
    public void ChangeColor(int ringIndex, int faceIndex, bool hurts) {

        if (ringIndex >= groundSections.rings.Count) {

            Debug.LogError("ERROR: ringIndex out of bound");
            return;
        }

        if (faceIndex >= groundSections.rings[ringIndex].sections.Count) {

            Debug.LogError("ERROR: faceIndex out of bound");
            return;
        }

        Renderer rendererFaces;
        rendererFaces = groundSections.rings[ringIndex].sections[faceIndex].GetComponent<Renderer>();
        rendererFaces.material.shader = Shader.Find(shader);

        float value = rendererFaces.material.GetFloat("_Dissolve");


        if (hurts) {
            rendererFaces.material.SetFloat("_Dissolve", 0.8f);
        } else {
            rendererFaces.material.SetFloat("_Dissolve", 0f);
        }

    }

    /// <summary>
    /// Changes the material of a face with the given one
    /// </summary>
    /// <param name="ringIndex"></param>
    /// <param name="faceIndex"></param>
    /// <param name="mat"></param>
    public void ChangeColor(int ringIndex, int faceIndex, Material mat) {

        if (ringIndex >= groundSections.rings.Count) {

            Debug.LogError("ERROR: ringIndex out of bound");
            return;
        }

        if (faceIndex >= groundSections.rings[ringIndex].sections.Count) {

            Debug.LogError("ERROR: faceIndex out of bound");
            return;
        }

        Renderer rendererFaces;
        rendererFaces = groundSections.rings[ringIndex].sections[faceIndex].GetComponent<Renderer>();
        rendererFaces.material = mat;
    }

    public void ChangeColor(int ringIndex, int faceIndex, float duration) {

        StartCoroutine(ChangeColorCoroutine(ringIndex, faceIndex, duration));
    }

    public void ChangeColor(int ringIndex, int faceIndex, bool hurts, float duration) {

        StartCoroutine(ChangeColorCoroutine(ringIndex, faceIndex, hurts, duration));
    }

    public IEnumerator ChangeColorCoroutine(int ringIndex, int faceIndex, bool hurts, float duration) {

        Renderer rendererFaces;
        float tLerp = 0f;
        float value;
        float start;
        float end;
        float finalValue;
        bool activation;
        rendererFaces = groundSections.rings[ringIndex].sections[faceIndex].GetComponent<Renderer>();
        rendererFaces.material.shader = Shader.Find(shader);

        value = rendererFaces.material.GetFloat("_Dissolve");

        if (hurts) {
            start = 0f;
            end = 0.8f;
            finalValue = 1f;
            activation = true;
        } else {
            start = 0.8f;
            end = 0f;
            finalValue = -0.1f;
            activation = false;
        }

        while (tLerp <= duration) {

            value = Mathf.Lerp(start, end, tLerp/duration);
            rendererFaces.material.SetFloat("_Dissolve", value);
            tLerp += Time.deltaTime;
            if (tLerp >= duration) {
                //groundSections.SwitchFace(ringIndex, faceIndex);
                rendererFaces.material.SetFloat("_Dissolve", finalValue);
            }
            yield return null;
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

    public void ChangeColor(int ringIndex, Material mat) {

        if (ringIndex >= groundSections.rings.Count) {
            Debug.LogError("ERROR: ringIndex out of bound");
        }

        for (int i = 0; i < groundSections.rings[ringIndex].sections.Count; i++) {
            ChangeColor(ringIndex, i, mat);
        }
    }

    /// <summary>
    /// Changes the material of a ring
    /// </summary>
    /// <param name="ringIndex">index of the ring</param>
    /// <param name="mat">material to use</param>
    public void ChangeColor(int ringIndex, float duration) {

        if (ringIndex >= groundSections.rings.Count) {
            Debug.LogError("ERROR: ringIndex out of bound");
            return;
        }

        for (int i = 0; i < groundSections.rings[ringIndex].sections.Count; i++) {
            StartCoroutine(ChangeColorCoroutine(ringIndex, i, duration));
        }
    }

    public void ResetGround(int ringIndex, int faceIndex, float duration) {

        StartCoroutine(ResetGroundCoroutine(ringIndex, faceIndex, duration));
    }

    public IEnumerator ResetGroundCoroutine(int ringIndex, int faceIndex, float duration) {

        Renderer rendererFaces;
        float tLerp = 0f;
        float value;
        float start;
        float end;
        rendererFaces = groundSections.rings[ringIndex].sections[faceIndex].GetComponent<Renderer>();
        rendererFaces.material.shader = Shader.Find(shader);

        value = rendererFaces.material.GetFloat("_Dissolve");

        if (groundSections.rings[ringIndex].sections[faceIndex].hurts) {
         
            start = 0.8f;
            end = 0f;

            while (tLerp <= duration) {

                value = Mathf.Lerp(start, end, tLerp);
                rendererFaces.material.SetFloat("_Dissolve", value);
                tLerp += Time.deltaTime;
                if (tLerp >= duration) {
                    groundSections.rings[ringIndex].sections[faceIndex].isTarget = false;
                    groundSections.rings[ringIndex].sections[faceIndex].hurts = false;
                    rendererFaces.material.SetFloat("_Dissolve", end);
                }
                yield return null;
            }
        } else {
            groundSections.rings[ringIndex].sections[faceIndex].isTarget = false;
            groundSections.rings[ringIndex].sections[faceIndex].hurts = false;
        }       
    }

    public IEnumerator ChangeColorCoroutine(int ringIndex, int faceIndex, float duration) {

        Renderer rendererFaces;
        float tLerp = 0f;
        float value;
        float start;
        float end;
        bool activation;
        rendererFaces = groundSections.rings[ringIndex].sections[faceIndex].GetComponent<Renderer>();
        rendererFaces.material.shader = Shader.Find(shader);

        value = rendererFaces.material.GetFloat("_Dissolve");

        if(value == 0) {
            start = 0f;
            end = 0.8f;
            activation = true;
        } else {
            start = 0.8f;
            end = 0f;
            activation = false;
        }

        while (tLerp <= duration) {
                       
            value = Mathf.Lerp(start, end, tLerp);
            rendererFaces.material.SetFloat("_Dissolve", value);
            tLerp += Time.deltaTime;
            if (tLerp >= duration) {
                //groundSections.SwitchFace(ringIndex, faceIndex);
                rendererFaces.material.SetFloat("_Dissolve", end);
            }
            yield return null;
        }
    }

    public void ChangeColorDelayed(int ringIndex, int faceIndex, Material mat, float delay) {

        StartCoroutine(ChangeColorDelayedCoroutine(ringIndex, faceIndex, mat, delay));
    }

    private IEnumerator ChangeColorDelayedCoroutine(int ringIndex, int faceIndex, Material mat, float delay) {

        yield return new WaitForSeconds(delay);
        ChangeColor(ringIndex, faceIndex);
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

    public void ChangeColorSlice(int faceIndex, float duration) {

        if (faceIndex >= groundSections.rings[0].sections.Count) {
            Debug.LogError("ERROR: faceIndex out of bound");
            return;
        }

        for (int i = 0; i < groundSections.rings.Count; i++) {
            ChangeColor(i, faceIndex, duration);
        }
    }
}


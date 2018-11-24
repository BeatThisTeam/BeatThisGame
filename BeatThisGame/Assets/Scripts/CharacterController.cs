using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public GroundSections ground;

    public int faceIndex;
    public int ringIndex;

    private Animator anim;
    private Transform tr;
    public Renderer rend;



    private void Awake() {

        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rend = GetComponentInChildren<Renderer>();
        rend.material.shader = Shader.Find("Dissolve");
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.D)) {
            ScoreManager.Instance.HitNote();
            StartCoroutine("FadeOut");
            faceIndex = (faceIndex + 1) % ground.rings[ringIndex].sections.Count;
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            ScoreManager.Instance.HitNote();
            StartCoroutine("FadeOut");
            faceIndex --;
            if(faceIndex < 0){
                faceIndex = ground.rings[ringIndex].sections.Count - 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            ScoreManager.Instance.HitNote();
            StartCoroutine("FadeOut");
            ringIndex --;
            if(ringIndex < 0) {
                ringIndex = ground.rings.Count - 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            ScoreManager.Instance.HitNote();
            StartCoroutine("FadeOut");
            ringIndex = (ringIndex + 1) % ground.rings.Count;
        }

        
        tr.position = ground.rings[ringIndex].sections[faceIndex].tr.position;
        Vector3 lookAtPos = Vector3.zero - tr.position;
        lookAtPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookAtPos);
    }

    private IEnumerator FadeOut() {

        float start = 0f;
        float finish = 1f;

        float tLerp = 0f;
        float duration = 0.2f;

        while (tLerp <= duration) {
            rend.material.SetFloat("_Dissolve", Mathf.Lerp(start, finish, tLerp/duration));
            tLerp += Time.deltaTime;

            yield return null;
        }
        rend.material.SetFloat("_Dissolve", 0);
    }
}

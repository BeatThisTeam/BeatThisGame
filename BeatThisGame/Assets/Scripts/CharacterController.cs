using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float angleDistance;
    public float zDistance;
    public float angle;
    public float radius;

    private Animator anim;
    private Transform tr;
    public Renderer rend;



    private void Awake() {

        angleDistance = Mathf.PI / 6;
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rend = GetComponentInChildren<Renderer>();
        rend.material.shader = Shader.Find("Dissolve");
    }

    private void Start() {

        radius = Mathf.Sqrt(Mathf.Pow(tr.position.x, 2) + Mathf.Pow(tr.position.z, 2));
        angle = Mathf.Atan(tr.position.z / tr.position.x);
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.D)) {

            StartCoroutine("FadeOut");
            angle += angleDistance;
        }

        if (Input.GetKeyDown(KeyCode.A)) {

            StartCoroutine("FadeOut");
            angle -= angleDistance;
        }

        if (Input.GetKeyDown(KeyCode.W)) {

            StartCoroutine("FadeOut");
            radius -= 4;
        }

        if (Input.GetKeyDown(KeyCode.S)) {

            StartCoroutine("FadeOut");
            radius += 4;
        }

        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);
        tr.position = new Vector3(x, tr.position.y, z);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMetronome : MonoBehaviour {

    private Transform tr;
    private bool start = false;
    private float tLerp = 0f;

    public Vector3 startScale = new Vector3(1, 1, 1);
    public Vector3 endScale = new Vector3(0.5f, 0.5f, 0.5f);

    private void Awake() {
        tr = GetComponent<Transform>();
    }

    public void StartMetronome() {
        start = true;
    }

    private void FixedUpdate() {

        if (start) {
                tr.localScale = Vector3.Lerp(startScale, endScale, tLerp / (SongManager.Instance.SecondsPerBeat / 2));
                tLerp += Time.deltaTime;

                if (tLerp >= SongManager.Instance.SecondsPerBeat / 2) {

                    tLerp = 0;
                    Vector3 temp = startScale;
                    startScale = endScale;
                    endScale = temp;
                }
        }
    }
}

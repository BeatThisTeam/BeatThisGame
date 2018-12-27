using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCam : MonoBehaviour
{

    private Transform[] view;
    private int viewIndex;

    [Header("Camera Positions")]
    public Transform camPosUp;
    public Transform camPosDown;

    [Header("Camera look direction")]
    public Transform cameraTarget;

    [Header("Camera movement duration")]
    public float transitionDuration;

    [Header("Player")]
    public PlayerController player;

    public void Setup() {

        view = new Transform[2];
        view[0] = camPosUp;
        view[1] = camPosDown;
        viewIndex = 1;
    }

    private void Update() {

        int FaceIndex = player.faceIndex;
        Vector3 DesiredPosition = view[viewIndex].position;

        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, 9f * Time.deltaTime);

        transform.position = SmoothedPosition;
        transform.LookAt(cameraTarget);
    }

    public void ChangeCamera() {

        if (viewIndex == 0) {
            viewIndex = 1;
        } else {
            viewIndex = 0;
        }
        StartCoroutine(UpDown(view[viewIndex], transitionDuration));
    }

    IEnumerator UpDown(Transform EndView, float duration){

        float tLerp = 0;

        while (tLerp <= duration) {
            transform.position = Vector3.Lerp(transform.position, EndView.position, tLerp/duration);

            Vector3 currentAngle = new Vector3
                (Mathf.LerpAngle(transform.rotation.eulerAngles.x, EndView.rotation.eulerAngles.x, tLerp / duration),
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, EndView.rotation.eulerAngles.y, tLerp / duration),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, EndView.rotation.eulerAngles.z, tLerp / duration));

            transform.eulerAngles = currentAngle;
            tLerp += Time.deltaTime;

            yield return null;
        }
    }
}
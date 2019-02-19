using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowCamUI : MonoBehaviour {

    public Menu menu;
    public Transform[] mainMenuRing;
    public Transform[] levelSelectRing;

    private Transform[] currentRing;

    public Transform[] target;
    public Transform curTarget;

    public CanvasRotation[] buttonCanvas;

    public float speed;
    public float speed2;


    public int faceIndex = 0;
    private int sliceCount;

    public int ringIndex = 0;

    private int section;

    private float horizAxisInput;
    private bool axisInUse = false;

    private void Start() {

        currentRing = mainMenuRing;
        curTarget = target[0];
    }

    private void Update(){

        sliceCount = currentRing.Length;
        Debug.Log("sliceCount " + sliceCount);
        horizAxisInput = Input.GetAxisRaw("Horizontal");

        if (!axisInUse && horizAxisInput != 0) {

            axisInUse = true;

            if (horizAxisInput < 0) {
                faceIndex = ((faceIndex - 1) + sliceCount) % sliceCount;
                menu.changeIndex(-1);
            }

            if (horizAxisInput > 0) {
                faceIndex = ((faceIndex + 1) + sliceCount) % sliceCount;
                menu.changeIndex(1);
            }
        }

        if (horizAxisInput == 0) {

            axisInUse = false;
        }

        buttonCanvas[ringIndex].RotateCanvas(faceIndex);

        Vector3 DesiredPosition = currentRing[faceIndex].position;
        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, speed * Time.deltaTime);
        transform.position = SmoothedPosition;

        transform.LookAt(curTarget);

    }

    public void SwitchRing(float duration)
    {

        ringIndex = (ringIndex + 1) % 2;

        if(currentRing == mainMenuRing) {
            currentRing = levelSelectRing;
            curTarget = target[1];
        } else {
            currentRing = mainMenuRing;
            curTarget = target[0];
        }

        menu.ChangeRing();
        StartCoroutine(SwitchRingCoroutine(duration, 0));
    }

    IEnumerator SwitchRingCoroutine (float duration, int FaceIndex)
    {
        faceIndex = 0;
        
        float tLerp = 0;

        while (tLerp <= duration)
        {
            transform.position = Vector3.Lerp(transform.position, currentRing[0].position, tLerp / duration);

           Vector3 currentAngle = new Vector3
                (Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentRing[0].rotation.eulerAngles.x, tLerp),
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentRing[0].eulerAngles.y, tLerp ),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentRing[0].rotation.eulerAngles.z, tLerp ));
            
            transform.eulerAngles = currentAngle;

            tLerp += Time.deltaTime;

            yield return null;
        }
    }


}
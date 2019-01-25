using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowCamUI : MonoBehaviour {
    public GroundSections[] CamRingUI;

    public Transform[] target;


    public float speed;
    public float speed2;


    public int FaceIndex = 0;
    private int sliceCount;

    public int i = 0;

    private int section;

    private float horizAxisInput;
    private bool axisInUse = false;

    private void Update(){

        sliceCount = CamRingUI[i].rings[0].sections.Count;
        Debug.Log("sliceCount " + sliceCount);
        horizAxisInput = Input.GetAxisRaw("Horizontal");

        if (!axisInUse && horizAxisInput != 0) {

            axisInUse = true;

            if (horizAxisInput < 0) {
                FaceIndex = ((FaceIndex - 1) + sliceCount) % sliceCount;
            }

            if (horizAxisInput > 0) {
                FaceIndex = ((FaceIndex + 1) + sliceCount) % sliceCount;
            }
        }

        if (horizAxisInput == 0) {

            axisInUse = false;
        }
        

        Vector3 DesiredPosition = CamRingUI[i].rings[0].sections[FaceIndex].tr.position;
        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, speed * Time.deltaTime);
        transform.position = SmoothedPosition;

        transform.LookAt(target[i]);

    }

    public void SwitchRing(float duration)
    {
        if (i == 0)
        {
            i = 1;
            FaceIndex = 0;
        }
        else
        {
            i = 0;
            FaceIndex = 1;
        }

        StartCoroutine(SwitchRingCoroutine(CamRingUI[i], duration, FaceIndex));
    }

    IEnumerator SwitchRingCoroutine (GroundSections CamRingUI, float duration, int FaceIndex)
    {
        float tLerp = 0;

        while (tLerp <= duration)
        {
            transform.position = Vector3.Lerp(transform.position, CamRingUI.rings[0].sections[FaceIndex].tr.position, tLerp / duration);

           Vector3 currentAngle = new Vector3
                (Mathf.LerpAngle(transform.rotation.eulerAngles.x, CamRingUI.rings[0].sections[FaceIndex].tr.rotation.eulerAngles.x, tLerp),
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, CamRingUI.rings[0].sections[FaceIndex].tr.rotation.eulerAngles.y, tLerp ),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, CamRingUI.rings[0].sections[FaceIndex].tr.rotation.eulerAngles.z, tLerp ));
            
            transform.eulerAngles = currentAngle;

            tLerp += Time.deltaTime;

            yield return null;
        }
    }


}
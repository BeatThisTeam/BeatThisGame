using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowCamUI : MonoBehaviour {
    public GroundSections[] CamRingUI;
    //public GroundSections CamRingMainMenu;
    //public GroundSections CamRingLS;

    //public GroundSections CamRingUI;

    //public CamToLevelSelect CamToLS;

    public Transform[] target;
    //public Transform targetMainMenu;
    //public Transform targetLS;

    public float speed;
    public float speed2;

    //public float SmoothSpeed;

    public int FaceIndex = 0;
    private int sliceCount;

    //public bool SwitchRing = false;
    //public float duration;

    public int i = 0;

    private int section;

    //public void Start()
    //{
    //    CamRingUI = new GroundSections[2];
    //    CamRingUI[0] = CamRingMainMenu;
    //    CamRingUI[1] = CamRingLS;

    //    target = new Transform[2];
    //    target[0] = targetMainMenu;
    //    target[1] = targetLS;

    //    i = 0;

    //}

    private void Update()
    {
        //if (CamToLS.SwitchRing == true)
        //{
        //    i = 1;
        //}

        //if (SwitchRing == true)
        //{
        //    StartCoroutine(Waiting(duration));
        //}

        sliceCount = CamRingUI[i].rings[0].sections.Count;
        Debug.Log("sliceCount " + sliceCount);

        if (Input.GetKeyDown(KeyCode.A))
        {
            FaceIndex = ((FaceIndex - 1) + sliceCount) % sliceCount;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            FaceIndex = ((FaceIndex + 1) + sliceCount) % sliceCount;
        }

        Debug.Log("FaceIndex " + FaceIndex);

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
            transform.position = Vector3.Lerp(transform.position, CamRingUI.rings[0].sections[FaceIndex].tr.position, speed2 * Time.deltaTime);
            //transform.rotation = Quaternion.Slerp(transform.rotation, CamRingUI.rings[0].sections[FaceIndex].tr.rotation, speed2 * Time.deltaTime);

            Vector3 currentAngle = new Vector3
                (Mathf.LerpAngle(transform.rotation.eulerAngles.x, CamRingUI.rings[0].sections[FaceIndex].tr.rotation.eulerAngles.x, tLerp),
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, CamRingUI.rings[0].sections[FaceIndex].tr.rotation.eulerAngles.y, tLerp ),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, CamRingUI.rings[0].sections[FaceIndex].tr.rotation.eulerAngles.z, tLerp ));

            //transform.eulerAngles = CamRingUI.rings[0].sections[0].tr.rotation.eulerAngles;
            transform.eulerAngles = currentAngle;

            tLerp += Time.deltaTime;

            yield return null;
        }
    }


}
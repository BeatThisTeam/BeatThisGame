using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCam : MonoBehaviour
{

    public Transform[] view;
    Transform EndView;

    public Transform monster;

    public float TransitionSpeed;
    //public float SmoothWASDSpeed;

    public bool i;

    public int set = 1;

    public GameObject MainCamera;
    public GameObject OverheadCamera;

    public float WaitingTime;
    public float ActiveWaitingTime;

    public PlayerController player;

    public GroundSections OvCamRing;

    public void FixedUpdate()
    {

        Camera MainCam = MainCamera.GetComponent<Camera>();
        Camera OvCam = OverheadCamera.GetComponent<Camera>();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            set = 0;
            OvCam.enabled = true;
            MainCam.enabled = false;

            EndView = view[0];
            StartCoroutine(UpDown(EndView));
            StartCoroutine(Active());
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            i = false;
            set = 1;
            EndView = view[1];
            StartCoroutine(UpDown(EndView));
            StartCoroutine(OvCameraDown(MainCam, OvCam, WaitingTime));
        }

        //if (i == true)
        //{
        //    StartCoroutine(MoveWASD());
        //}
       
    }

    IEnumerator UpDown(Transform EndView)
    {
        while(i == false)
        {
            transform.position = Vector3.Lerp(transform.position, EndView.position, TransitionSpeed * Time.deltaTime);
            //transform.LookAt(monster);

            Vector3 currentAngle = new Vector3
                (Mathf.LerpAngle(transform.rotation.eulerAngles.x, EndView.rotation.eulerAngles.x, TransitionSpeed * Time.deltaTime),
                  Mathf.LerpAngle(transform.rotation.eulerAngles.y, EndView.rotation.eulerAngles.y, TransitionSpeed * Time.deltaTime),
                  Mathf.LerpAngle(transform.rotation.eulerAngles.z, EndView.rotation.eulerAngles.z, TransitionSpeed * Time.deltaTime));

            transform.eulerAngles = currentAngle;

            yield return null;
        }

        
    }

    //IEnumerator MoveWASD()
    //{

    //    int FaceIndex = player.faceIndex;
    //    Vector3 DesiredPosition = OvCamRing.rings[0].sections[FaceIndex].tr.position;

    //    Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, SmoothWASDSpeed * Time.deltaTime);

    //    transform.position = SmoothedPosition;
    //    transform.LookAt(monster);

    //    yield return null;
    //}


    IEnumerator OvCameraDown(Camera MainCam, Camera OvCam, float WaitingTime)
    {
        yield return new WaitForSeconds(WaitingTime);
        i = true;
        set = 1;
        MainCam.enabled = true;
        OvCam.enabled = false;
    }

    IEnumerator Active()
    {
        yield return new WaitForSeconds(ActiveWaitingTime);
        i = true;
    }
}
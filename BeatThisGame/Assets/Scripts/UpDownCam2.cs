using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCam2 : MonoBehaviour {

   // public Transform[] view;
   // public float TransitionSpeed;
   // Transform EndView;

   // public GameObject MainCamera;
    //public GameObject OverheadCamera;

    //public float WaitingTime;

    public PlayerController player;

    public UpDownCam cam;

    public GroundSections[] OvCamRing;

    public float SmoothSpeed;

    public Transform monster;

    public Transform MainCam;
    public GameObject OvCamera;

    private void Start()
    {
        int FaceIndex = player.faceIndex;
        StartCoroutine(OvCamMove(OvCamRing[1], FaceIndex));
    }

    private void FixedUpdate()
    {
        //Camera OvCam = OvCamera.GetComponent<Camera>();

        //if (cam.i == false && OvCam.enabled == false)
        //{
        //    transform.position = MainCam.position;
        //}

        //if (cam.i == true) 
        //{
        //    StartCoroutine(OvCamMove());
        //}
        int FaceIndex = player.faceIndex;
        if (cam.set == 1)
        {
            StartCoroutine(OvCamMove(OvCamRing[1], FaceIndex));
        }

        if(cam.set == 0)
        {
            StartCoroutine(OvCamMove(OvCamRing[0], FaceIndex));
        }

    }

    private IEnumerator OvCamMove(GroundSections OvCamRing, int FaceIndex)
    {

        
        Vector3 DesiredPosition = OvCamRing.rings[0].sections[FaceIndex].tr.position;

        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, SmoothSpeed * Time.deltaTime);

        transform.position = SmoothedPosition;
        transform.LookAt(monster);

        yield return null;
    }

}
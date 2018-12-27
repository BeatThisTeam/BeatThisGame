using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam5 : MonoBehaviour {

    public GroundSections CamRing;

    public PlayerController player;

    public Transform monster;

    public float SmoothSpeed;

    private Vector3 DesiredPosition;

    private void Update()
    {

        int FaceIndex = player.faceIndex;
        DesiredPosition = CamRing.rings[0].sections[FaceIndex].tr.position;

        //Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, SmoothSpeed * Time.deltaTime);

        transform.position = DesiredPosition;
        transform.LookAt(monster);
    }


}

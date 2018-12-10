using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam5 : MonoBehaviour {

    public GroundSections CamRing;

    public CharacterController player;
    public GroundSections ground;

    public Transform target;
    public Transform monster;
    public Transform emptycam;

    public float SmoothSpeed;

    public Vector3 offset;

    private Vector3 DesiredPosition;

    public float Height;
    public float angle;
    public float radiusOffset;


    private void FixedUpdate()
    {

        //if (player.faceIndex == 8 || player.faceIndex == 0 || player.faceIndex == 1)
        //{DesiredPosition = CamRing.rings[0].faces[0].position;}

        //if (player.faceIndex == 2 || player.faceIndex == 3)
        //{DesiredPosition = CamRing.rings[0].faces[2].position;}

        //if (player.faceIndex == 4 || player.faceIndex == 5)
        //{DesiredPosition = CamRing.rings[0].faces[4].position;}

        //if (player.faceIndex == 6 || player.faceIndex == 7)
        //{ DesiredPosition = CamRing.rings[0].faces[6].position; }

        int FaceIndex = player.faceIndex;
        DesiredPosition = CamRing.rings[0].sections[FaceIndex].tr.position;

        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, SmoothSpeed * Time.deltaTime);

        transform.position = SmoothedPosition;
        transform.LookAt(monster);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam2 : MonoBehaviour
{

	
    public CharacterController player;
    public GroundSections ground;

    public Transform target;
    public Transform monster;

    public float SmoothSpeed = 10f;

    public Vector3 offset;
  
    private Vector3 DesiredPosition;


    private void FixedUpdate()
    {
        int FaceIndex = player.faceIndex;

        float dist = Vector3.Distance(ground.rings[2].faces[FaceIndex].position, offset);


        DesiredPosition.x = dist * Mathf.Sin(40);
        DesiredPosition.z = dist * Mathf.Cos(40);
        DesiredPosition.y = offset.y;

        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, SmoothSpeed * Time.deltaTime);

        transform.position = SmoothedPosition;

        transform.LookAt(monster);
    }

    }
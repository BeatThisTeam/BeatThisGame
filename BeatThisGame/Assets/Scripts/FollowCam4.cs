using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam4 : MonoBehaviour {

    public CharacterController player;
    public GroundSections ground;

    public Transform target;
    public Transform monster;
    public Transform emptycam;

    public float SmoothSpeed = 10f;

    public Vector3 offset;

    private Vector3 DesiredPosition;

    public float Height;
    public float angle;
    public float radiusOffset;


    private void FixedUpdate()
    {
        int FaceIndex = player.faceIndex;
        float radius = Vector3.Distance(monster.position, ground.rings[2].faces[FaceIndex].position);

        DesiredPosition.y = Height;

        if (player.faceIndex == 2 || player.faceIndex == 3 || player.faceIndex == 4)
        {
            DesiredPosition.x = target.position.x + ((radius + radiusOffset) * Mathf.Cos(Mathf.Deg2Rad * angle));
            DesiredPosition.z = -target.position.z + ((radius + radiusOffset) * Mathf.Sin(Mathf.Deg2Rad * angle));

        }

        if (player.faceIndex == 5 || player.faceIndex == 6 || player.faceIndex == 7)
        {
            DesiredPosition.x = -(target.position.x + ((radius + radiusOffset) * Mathf.Cos(Mathf.Deg2Rad * angle)));
            DesiredPosition.z = target.position.z + ((radius + radiusOffset) * Mathf.Sin(Mathf.Deg2Rad * angle));
        }

        else
        {
            DesiredPosition.x = target.position.x + ((radius + radiusOffset) * Mathf.Cos(Mathf.Deg2Rad * angle));
            DesiredPosition.z = target.position.z + ((radius + radiusOffset) * Mathf.Sin(Mathf.Deg2Rad * angle));

        }



        //Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, SmoothSpeed * Time.deltaTime);

        //transform.position = SmoothedPosition;

        transform.position = DesiredPosition;
            

        transform.LookAt(monster);

    }
}

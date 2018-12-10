
using UnityEngine;

public class FollowCam3 : MonoBehaviour


{
    public CharacterController player;
    public GroundSections ground;

    public Transform target;
    public Transform monster;

    public float SmoothSpeed = 10f;

    public Vector3 offset;
    public Vector3 offseta;

    public Transform Pos0;
    public Transform Pos3;
    public Transform Pos6;

    private Vector3 DesiredPosition;

    private float x;
    private float y;
    private float z;

    public Transform Face0;
    public Transform Face3;
    public Transform Face6;

    private void FixedUpdate()
    {
        int FaceIndex = player.faceIndex;

        // float dist = Vector3.Distance(monster.position, ground.rings[2].faces[0].position) + Mathf.Abs(offset.z);
        // Debug.Log("dst " + dist);

        // x = (dist * Mathf.Sin(40));
        // z = (dist * Mathf.Cos(40));

        // DesiredPosition.x = x;
        // Debug.Log("x " + DesiredPosition.x);
        // DesiredPosition.y = offset.y;
        // Debug.Log("y " + DesiredPosition.y);
        // DesiredPosition.z = z;
        // Debug.Log("z " + DesiredPosition.z);

        // Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, SmoothSpeed * Time.deltaTime);

        // transform.position = SmoothedPosition;

        //transform.LookAt(monster);

        // //if (player.faceIndex == 0 || player.faceIndex == 1 || player.faceIndex == 8)
        //{

        //    DesiredPosition = ground.rings[2].faces[0].position + offset;
        //    //DesiredPosition = Pos0.position;

        //    //  transform.LookAt(monster);
        //}

        //if (player.faceIndex == 2 || player.faceIndex == 3)
        //{
        //    x = (dist * Mathf.Sin(100)) + (Mathf.Abs(offset.x));
        //    z = (dist * Mathf.Cos(100)) + (Mathf.Abs(offset.z));

        //   // Debug.Log("x " + dist * Mathf.Sin(60));
        //   // Debug.Log("z " + dist * Mathf.Cos(60));

        //    DesiredPosition.x = x;
        //    DesiredPosition.y = offset.y;
        //    DesiredPosition.z = z;
        //    //DesiredPosition = Pos3.position;

        //    //  transform.LookAt(monster);
        //}

        //if (player.faceIndex == 4 || player.faceIndex == 5)
        //{
        //    DesiredPosition = ground.rings[2].faces[6].position + offset;
        //    // DesiredPosition = Pos6.position;

        //    //  transform.LookAt(monster);
        //}

        //if (player.faceIndex == 6 || player.faceIndex == 7)
        //{
        //    DesiredPosition = ground.rings[2].faces[6].position + offset;
        //    // DesiredPosition = Pos6.position;

        //    //  transform.LookAt(monster);
        //}

        // DesiredPosition = ground.rings[2].faces[FaceIndex].position + offset0;
        //transform.LookAt(monster);





        //  Vector3 DesiredPosition = ground.rings[2].faces[FaceIndex].position + offset;

        // ground.rings[2].faces[FaceIndex].position

        //   Vector3 DesiredPosition = target.position + offset;




        // transform.LookAt(target);
        //transform.LookAt(monster);


        //}

        //    if (player.faceIndex == 0 || player.faceIndex == 1 || player.faceIndex == 8)
        //    {

        //        DesiredPosition.x = 0;
        //        DesiredPosition.y = 17;
        //        DesiredPosition.z = -40;

        //    }

        //    if (player.faceIndex == 2 || player.faceIndex == 3 || player.faceIndex == 4)
        //    {

        //        DesiredPosition.x = 35;
        //        DesiredPosition.y = 17;
        //        DesiredPosition.z = 20;

        //    }

        //    if (player.faceIndex == 5 || player.faceIndex == 6 || player.faceIndex == 7)
        //    {

        //        DesiredPosition.x = -35;
        //        DesiredPosition.y = 17;
        //        DesiredPosition.z = 20;

        //    }


        //    Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, SmoothSpeed * Time.deltaTime);

        //    transform.position = SmoothedPosition;

        //    transform.LookAt(monster);
        //}



        if (player.faceIndex == 0 || player.faceIndex == 1 || player.faceIndex == 8)
        {
            float dist = Vector3.Distance(monster.position, ground.rings[2].faces[FaceIndex].position);
            DesiredPosition = ground.rings[2].faces[FaceIndex].position + offset;

        }

        if (player.faceIndex == 2 || player.faceIndex == 3)
        {
            //float dist = Vector3.Distance(monster.position, ground.rings[2].faces[FaceIndex].position);
            //DesiredPosition = ground.rings[2].faces[FaceIndex].position + offset;

            float dist = Vector3.Distance(monster.position, ground.rings[2].faces[FaceIndex].position);

            DesiredPosition.x = offset.x + Mathf.Abs(Mathf.Abs(dist) * (Mathf.Cos(60)));
            DesiredPosition.z = offset.z -Mathf.Abs(Mathf.Abs(dist) * (Mathf.Sin(60)));
            DesiredPosition.y = offset.y;
        }

        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, SmoothSpeed * Time.deltaTime);

        transform.position = SmoothedPosition;

        transform.LookAt(monster);





    }
}




using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public CharacterController player;
    public GroundSections ground;

    public Transform target;
    public Transform monster;
    
    public float SmoothSpeed = 10f;

    public Vector3 offset0;
    public Vector3 offset3;
    public Vector3 offset6;
    public Vector3 offset4;

    public Transform Pos0;
    public Transform Pos3;
    public Transform Pos6;

    private Vector3 DesiredPosition;

    public Transform Face0;
    public Transform Face3;
    public Transform Face6;

    private void FixedUpdate()
    {
        int FaceIndex = player.faceIndex;

        if (player.faceIndex == 0 || player.faceIndex == 1 || player.faceIndex == 8)
        {
            
            DesiredPosition = ground.rings[2].faces[FaceIndex].position + offset0;
            //DesiredPosition = Pos0.position;

           //  transform.LookAt(monster);
        }

        if (player.faceIndex == 2 || player.faceIndex == 3)
        {
            DesiredPosition = ground.rings[2].faces[FaceIndex].position + offset3;
            //DesiredPosition = Pos3.position;

          //  transform.LookAt(monster);
        }

        if (player.faceIndex == 4 || player.faceIndex == 5)
        {
            DesiredPosition = ground.rings[2].faces[FaceIndex].position + offset4;
            // DesiredPosition = Pos6.position;

            //  transform.LookAt(monster);
        }

        if (player.faceIndex == 6 || player.faceIndex == 7)
        {
            DesiredPosition = ground.rings[2].faces[FaceIndex].position + offset6;
            // DesiredPosition = Pos6.position;

           //  transform.LookAt(monster);
        }

        // DesiredPosition = ground.rings[2].faces[FaceIndex].position + offset0;
        //transform.LookAt(monster);





        //  Vector3 DesiredPosition = ground.rings[2].faces[FaceIndex].position + offset;

        // ground.rings[2].faces[FaceIndex].position

        //   Vector3 DesiredPosition = target.position + offset;
        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, SmoothSpeed*Time.deltaTime);

        transform.position = SmoothedPosition;

        transform.LookAt(monster);
        

        
       // transform.LookAt(target);
      //transform.LookAt(monster);
    }



}

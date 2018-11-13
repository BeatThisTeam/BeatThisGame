using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public Transform target;
    public Transform player;

    // Vector3 startPos = new Vector3(0f,30f,-70f);


    void Update()
    {

        if (Input.GetKeyDown("a"))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 40);
        }
        if (Input.GetKeyDown("d"))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, -40);
        }
        transform.LookAt(target);


    }

}

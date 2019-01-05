using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFollowingInCircle : MonoBehaviour {

    public float radius;
    public float duration;
    public float height;

    public void Awake(){

        GetComponent<Transform>();
    }

    public void CircleTrajectory(float radius, float height, float duration, Vector3 spawnpos, Vector3 endpos, int direction){

        StartCoroutine(CircleTrajectoryCoroutine(radius, height, duration, spawnpos, endpos, direction));
    }

   public  IEnumerator CircleTrajectoryCoroutine(float radius, float height, float duration, Vector3 spawnpos, Vector3 endpos, int direction){

        float TimeCounter = 0;
        float angle = 0;
        float speed = (Mathf.PI) / duration; //2*PI in degress is 360, PI is 180, so you get "duration" seconds to complete half a circle

        float x = spawnpos.x;
        float z = spawnpos.z;

        transform.position = spawnpos;

        while (TimeCounter <= duration ){

            if (direction == 0){
                angle -= speed * Time.deltaTime; //if you want to switch direction, use += instead of -=
            }
            
            if (direction == 1){
                angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
            }

            x = Mathf.Cos(angle) * radius;
            z = Mathf.Sin(angle) * radius;

            transform.position = new Vector3(x, height, z);

            TimeCounter += Time.deltaTime;

            yield return null;
        }

        if (TimeCounter > duration){
            Destroy(this.gameObject);
        }
   }
}

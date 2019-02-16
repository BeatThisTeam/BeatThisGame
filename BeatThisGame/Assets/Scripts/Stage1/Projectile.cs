using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour {

    public float damage;

    //The number of projectiles fired in the same attack
    public int numFired;

    Transform tr;
    public Transform player;

    public int playerRingPos;
    public int playerFacePos;

    public ProjectileAttack att;

    public bool rejectable;
    public bool rejected;
    public float rejectAccuracy;

    private UnityAction action;

    private static int conta;

    public float radius;
    public float duration;
    public float height;

    private void Awake() {
        tr = GetComponent<Transform>();
        action = new UnityAction(DestroyGameObject);
    }

    public void Move(Vector3 startPos, Vector3 endPos, float duration) {
       
        StartCoroutine( MoveCoroutine(startPos, endPos, duration));
    }

    IEnumerator MoveCoroutine(Vector3 startPos, Vector3 endPos, float duration) {
       
        float elapsedTime = 0;

        float distance = Vector3.Distance(startPos, endPos);
        float velocity = distance / duration;
        Vector3 dir = Vector3.Normalize(endPos - startPos);

        while (true) {
            elapsedTime += Time.deltaTime;
            Vector3 pos = tr.position;
            pos = pos + dir * velocity * Time.deltaTime;
            tr.position = pos;
            yield return null;

            if(elapsedTime >= duration + 0.5f) {
                DestroyGameObject();
            }
        }     
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            
            player.GetComponent<PlayerController>().Damage(damage);
            att.ResetTargetSections(playerFacePos, playerRingPos);
            DestroyGameObject();

            //if (rejectable) {
            //    conta++;
            //    Debug.Log("PROJECTILE n. " + conta);
            //}

        }
    }

    public void DestroyGameObject() {

        if (!rejectable && !rejected) {
            att.ResetTargetSections(playerFacePos, playerRingPos);
        }
        Destroy(this.gameObject);
    }



    public void CircleTrajectory(float radius, float height, float duration, Vector3 spawnpos, Vector3 endpos, int direction) {
        StartCoroutine(CircleTrajectoryCoroutine(radius, height, duration, spawnpos, endpos,
        direction));
    }

    public IEnumerator CircleTrajectoryCoroutine(float radius, float height, float duration, Vector3 spawnpos, Vector3 endpos, int direction) {
        float distance = 1f;
        transform.position = spawnpos;
        float x = spawnpos.x;
        float z = spawnpos.z;

        distance = Vector3.Angle(endpos, spawnpos);
        float distanceRad = distance * Mathf.Deg2Rad;
        float TimeCounter = 0f;
        float offsetAngle = 0f;
        float speed = distanceRad / duration;      
      
        while (true) {

            if (direction == 0) { 
                offsetAngle += speed * Time.deltaTime;
            }

            if (direction == 1){
                offsetAngle -= speed * Time.deltaTime;
            }

            x = (spawnpos.x * Mathf.Cos(offsetAngle)) + (spawnpos.z * Mathf.Sin(offsetAngle));
            z = (spawnpos.z * Mathf.Cos(offsetAngle)) - (spawnpos.x * Mathf.Sin(offsetAngle));
            Vector3 DesiredPosition = new Vector3(x, height, z);
            transform.position = DesiredPosition;

            TimeCounter += Time.deltaTime;

            yield return null;

            if (TimeCounter >= duration) {
                Debug.Log(Vector3.Distance(transform.position, endpos));
                //DestroyGameObject(); <--TO DO: farla funzionare
                Destroy(this.gameObject);
            }
        }

        

        ////if (TimeCounter > duration) {
        //Destroy(this.gameObject);
        ////}
    }
}

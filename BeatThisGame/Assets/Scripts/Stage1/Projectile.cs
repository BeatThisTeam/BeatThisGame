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

    public void CircleTrajectory(float radius, float height, float duration, Vector3 spawnpos, Vector3 endpos, int direction) {

        StartCoroutine(CircleTrajectoryCoroutine(radius, height, duration, spawnpos, endpos, direction));
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

    public IEnumerator CircleTrajectoryCoroutine(float radius, float height, float duration, Vector3 spawnpos, Vector3 endpos, int direction) {

        float TimeCounter = 0;
        float angle = 0;
        float speed = (Mathf.PI) / duration; //2*PI in degress is 360, PI is 180, so you get "duration" seconds to complete half a circle

        float x = spawnpos.x;
        float z = spawnpos.z;

        transform.position = spawnpos;
        Debug.Log(transform.position);

        while (TimeCounter <= duration) {

            if (direction == 0) {
                angle -= speed * Time.deltaTime; //if you want to switch direction, use += instead of -=
            }

            if (direction == 1) {
                angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
            }

            x = Mathf.Cos(angle) * radius;
            z = Mathf.Sin(angle) * radius;

            transform.position = new Vector3(x, height, z);

            TimeCounter += Time.deltaTime;

            yield return null;
        }

        if (TimeCounter > duration) {
            Destroy(this.gameObject);
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
}

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

            if (rejectable) {
                conta++;
                Debug.Log(conta);
            }
            
        }
    }

    public void DestroyGameObject() {

        if (!rejectable && !rejected) {
            att.ResetTargetSections(playerFacePos, playerRingPos);
        }
        Destroy(this.gameObject);
    }
}

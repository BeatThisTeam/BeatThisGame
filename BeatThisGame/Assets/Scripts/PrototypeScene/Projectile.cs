using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float damage;

    Transform tr;
    public Transform player;

    public int playerRingPos;
    public int playerFacePos;

    private void Awake() {
        tr = GetComponent<Transform>();
    }

    public void Move(Vector3 startPos, Vector3 endPos, float duration) {
        
        StartCoroutine( MoveCoroutine(startPos, endPos, duration));
    }

    IEnumerator MoveCoroutine(Vector3 startPos, Vector3 endPos, float duration) {

        float tLerp = 0;

        while (tLerp <= duration) {
            tr.position = Vector3.Lerp(startPos, endPos, tLerp / duration);
            tLerp += Time.deltaTime;
            yield return null;
        } 
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("colpito");
            player.GetComponent<CharacterController>().Damage(damage);
            Destroy(this.gameObject);
        }
    }
}

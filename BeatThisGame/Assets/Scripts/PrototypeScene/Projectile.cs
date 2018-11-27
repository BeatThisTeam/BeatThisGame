using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    Transform tr;

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

        //Debug.Log(SongManager.Instance.SongPositionInSeconds);
        Destroy(this.gameObject);
    }
}

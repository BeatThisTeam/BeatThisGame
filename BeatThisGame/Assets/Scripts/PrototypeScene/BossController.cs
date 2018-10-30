using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    Transform tr;

    public Projectile projectile;

    private void Awake() {

        tr = GetComponent<Transform>();
    }

    public void Attack(float duration) {
       
        StartCoroutine(AttackCoroutine(duration));
    }

    public void Attack2(Vector3 startPos, Vector3 endPos, float duration) {
        //Debug.Log(SongManager.Instance.SongPositionInSeconds);
        Projectile pr; 
        pr = Instantiate(projectile, startPos, Quaternion.identity);
        pr.Move(startPos, endPos, duration);
    }

    IEnumerator AttackCoroutine(float duration) {
        //Debug.Log(SongManager.Instance.SongPositionInSeconds);
        float tLerp = 0;
        float time = 0;
        Vector3 startScale = tr.localScale;
        Vector3 endScale = new Vector3(9, 9, 9);
        while (time <= duration) {
            tr.localScale = Vector3.Lerp(startScale, endScale, tLerp / (duration / 2));
            tLerp += Time.deltaTime;
            time += Time.deltaTime;

            if(tLerp >= duration / 2) {
                
                tLerp = 0;
                Vector3 temp = startScale;
                startScale = endScale;
                endScale = temp;
            }

            yield return null;
        }
    }
}

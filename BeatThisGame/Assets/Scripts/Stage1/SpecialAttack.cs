using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour {

    Transform tr;

    public Transform circle;

    public Transform player;

    public Transform playerOuterCircle;

    public Vector3 startScale;
    public Vector3 endScale;
    public float spawnHeight;
    public float enlargeVelocity;
    private float lastNotePlayedInSeconds;
    private IEnumerator cr;
    private bool enlarge = false;
    bool played = false;

    public void StartAttack(float duration) {
       
        Vector3 spawnPos = new Vector3(player.position.x, spawnHeight, player.position.z);
        if (!played) {
            played = true;
            tr = Instantiate(circle, spawnPos, Quaternion.identity);
            tr.GetComponent<SphereCollider>().enabled = false;
            tr.position = spawnPos;
            tr.localScale = startScale;
            tr.rotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
            cr = ShrinkCoroutine(duration);
            StartCoroutine(cr);
        }
    }

    private IEnumerator ShrinkCoroutine(float duration) {

        Vector3 startScale = tr.localScale;
        float tLerp = 0f;

        while (tLerp <= duration + 0.1) {
            Vector3 pos = new Vector3(player.position.x, spawnHeight, player.position.z);
            tr.position = pos;
            tr.localScale = Vector3.Lerp(startScale, endScale, tLerp / duration);
            tLerp += Time.deltaTime;

            if(tr.localScale.x <= playerOuterCircle.lossyScale.x) {
                playerOuterCircle.GetComponent<Circle>().ChangeRingColors();
            }

            if (Input.GetButtonDown("SpecialAttack")) {
                ScoreManager.Instance.HitSpecialAttack();
                enlarge = true;
                StartCoroutine(EnlargeCoroutine());
                StopCoroutine(cr);
            }

            if(tLerp >= duration + 0.1) {
                played = false;
                StopAllCoroutines();
                Destroy(tr.gameObject);
                ScoreManager.Instance.SpecialAttackMiss();
            }
            yield return null;
        }
    }

    public void BossHit() {
        enlarge = false;
        played = false;
        Destroy(tr.gameObject);
    }

    private IEnumerator EnlargeCoroutine() {

        tr.GetComponent<SphereCollider>().enabled = true;

        while (enlarge) {
            float scaleX = tr.localScale.x;
            float scaleY = tr.localScale.y;
            scaleX += enlargeVelocity * Time.deltaTime;
            scaleY += enlargeVelocity * Time.deltaTime;
            tr.localScale = new Vector3(scaleX, scaleY, tr.localScale.z);
            yield return null;
        }
    }
}

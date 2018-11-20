using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour {

    Transform tr;

    public Transform circle;
    public Transform player;

    public Vector3 startScale;
    public Vector3 endScale;
    public float spawnHeight;
    public float enlargeVelocity;
    private float lastNotePlayedInSeconds;
    private IEnumerator cr;
    private bool enlarge = false;

    public void StartAttack(float noteToPlayInSeconds) {

        Vector3 spawnPos = new Vector3(player.position.x, spawnHeight, player.position.z);
        float duration = ScenePrototypeManager.Instance.notesInSeconds[ScenePrototypeManager.Instance.notesInSecondsIndex + 1].notePosInSeconds - noteToPlayInSeconds;

        if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= duration && noteToPlayInSeconds != 0 && lastNotePlayedInSeconds != noteToPlayInSeconds) {
            lastNotePlayedInSeconds = noteToPlayInSeconds;
            Projectile pr;
            tr = Instantiate(circle, spawnPos, Quaternion.identity);
            tr.localScale = startScale;
            tr.rotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
            cr = ShrinkCoroutine(duration);
            StartCoroutine(cr);
        }
    }

    private IEnumerator ShrinkCoroutine(float duration) {

        Vector3 startScale = tr.localScale;
        float tLerp = 0f;

        while (tLerp <= duration) {
            tr.localScale = Vector3.Lerp(startScale, endScale, tLerp / duration);
            tLerp += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space)) {
                enlarge = true;
                StartCoroutine(EnlargeCoroutine());
                StopCoroutine(cr);
            }
            yield return null;
        }
    }

    public void BossHit() {
        enlarge = false;
    }

    private IEnumerator EnlargeCoroutine() {

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

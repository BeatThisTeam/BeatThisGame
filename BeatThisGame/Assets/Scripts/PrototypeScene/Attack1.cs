using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour {

    public Transform bossTr;

    public void StartAttack(float noteToPlayInSeconds, int notesInSecondsIndex) {

        Song song = SongManager.Instance.song;

        if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= 0 && noteToPlayInSeconds % 2 == 0) {

            float startAttack = noteToPlayInSeconds;
            float endAttack = ScenePrototypeManager.Instance.notesInSeconds[notesInSecondsIndex + 1].notePosInSeconds;

            StartCoroutine(AttackCoroutine(endAttack - startAttack));
            //ScenePrototypeManager.Instance.IncrementNoteToPlayInSeconds();
        }
    }

    IEnumerator AttackCoroutine(float duration) {
        //Debug.Log(SongManager.Instance.SongPositionInSeconds);
        float tLerp = 0;
        float time = 0;
        
        Vector3 startScale = bossTr.localScale;
        Vector3 endScale = new Vector3(9, 9, 9);
        while (time <= duration) {
            bossTr.localScale = Vector3.Lerp(startScale, endScale, tLerp / (duration / 2));
            tLerp += Time.deltaTime;
            time += Time.deltaTime;

            if (tLerp >= duration / 2) {

                tLerp = 0;
                Vector3 temp = startScale;
                startScale = endScale;
                endScale = temp;
            }

            yield return null;
        }
    }
}

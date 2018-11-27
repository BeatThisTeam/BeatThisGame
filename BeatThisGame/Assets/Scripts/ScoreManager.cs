using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private static ScoreManager instance;

    public static ScoreManager Instance { get { return instance; } }

    public float noteToHit;

    public float deltaAccuracy;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    public void HitNote() {
        //Debug.Log("delta: " + Mathf.Abs(noteToHit - SongManager.Instance.SongPositionInSeconds));
        //Debug.Log("songpos: " + SongManager.Instance.SongPositionInSeconds);
        //Debug.Log("targetpos: " + noteToHit);

        float diff = Mathf.Abs(noteToHit - SongManager.Instance.SongPositionInSeconds);

        if (diff < deltaAccuracy) {

            if(diff < deltaAccuracy && diff > deltaAccuracy / 2) {
                Debug.Log("ok");
            }else if (diff <= deltaAccuracy / 2 && diff > deltaAccuracy / 6) {
                Debug.Log("good");
            }else if (diff <= deltaAccuracy / 6) {
                Debug.Log("perfect");
            }

            if (SongManager.Instance.SongPositionInSeconds > noteToHit) {
                nextNoteToHit(ScenePrototypeManager.Instance.notesInSecondsIndex);
            } else {
                nextNoteToHit(ScenePrototypeManager.Instance.notesInSecondsIndex + 1);
            }
        }
    }

    public void nextNoteToHit(int index) {

        float prevNoteToHit = noteToHit;

        for (int i = index; i < ScenePrototypeManager.Instance.notesInSeconds.Count; i++) {
            if (ScenePrototypeManager.Instance.notesInSeconds[i].playerShouldPlay) {
                noteToHit = ScenePrototypeManager.Instance.notesInSeconds[i].notePosInSeconds;
                return;
            }
        }
    }

    public void UpdateNoteToHit() {
        if(noteToHit < SongManager.Instance.SongPositionInSeconds - deltaAccuracy) {
            Debug.Log("MISS");
            nextNoteToHit(ScenePrototypeManager.Instance.notesInSecondsIndex);
        }
    }
}

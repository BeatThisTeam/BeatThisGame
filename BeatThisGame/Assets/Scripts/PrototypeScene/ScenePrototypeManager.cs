using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePrototypeManager : MonoBehaviour {

    private static ScenePrototypeManager instance;

    public static ScenePrototypeManager Instance { get { return instance; } }

    public SongManager sm;

    public Song song;

    public BossController boss;

    public Transform player;

    public float noteToPlayInSeconds = 0;

    public int notesInSecondsIndex = 0;

    private bool playing = true;


    public float projectileSpeed = 10f;
    public float spawnHeight = 12f;

    public Attack1 att1;
    public Attack2 att2;
    public TilesAttack att3;
    //public SliceAttack att4;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }

        sm.SetSong(song);
    }

    private void FixedUpdate() {

        if (playing) {
            if (noteToPlayInSeconds == 0) {
                noteToPlayInSeconds = song.notesInSeconds[notesInSecondsIndex];
            }
            
            att1.StartAttack(noteToPlayInSeconds, notesInSecondsIndex);
            att2.StartAttack(noteToPlayInSeconds);
            att3.StartAttack(noteToPlayInSeconds);
            //att4.StartAttack(noteToPlayInSeconds);

            if(noteToPlayInSeconds <= SongManager.Instance.SongPositionInSeconds) {
                IncrementNoteToPlayInSeconds();
            }
        }
    }

    public void IncrementNoteToPlayInSeconds() {

        notesInSecondsIndex++;
        if (notesInSecondsIndex < song.notesInSeconds.Count) {
            noteToPlayInSeconds = song.notesInSeconds[notesInSecondsIndex];
        } else {
            SongManager.Instance.Stop();
            playing = false;
        }
    }
}

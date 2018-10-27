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
            
            Att1();
            Att2();
        }
    }

    private void Att1() {

        if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= 0 && noteToPlayInSeconds % 2 == 0) {

            float startAttack = noteToPlayInSeconds;
            float endAttack = song.notesInSeconds[notesInSecondsIndex + 1];

            boss.Attack(endAttack - startAttack);
            IncrementNoteToPlayInSeconds();
        }
    }

    private void Att2() {

        Vector3 spawnPos = new Vector3(player.position.x, spawnHeight, player.position.z);
        float projectileTime = Vector3.Distance(spawnPos, player.position) / projectileSpeed;

        if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= projectileTime && noteToPlayInSeconds % 2 != 0 && SongManager.Instance.SongPositionInSeconds >= noteToPlayInSeconds) {
            boss.Attack2(spawnPos, player.position, projectileTime);
            IncrementNoteToPlayInSeconds();
        }
    }

    private void IncrementNoteToPlayInSeconds() {

        notesInSecondsIndex++;
        if (notesInSecondsIndex < song.notesInSeconds.Count) {
            noteToPlayInSeconds = song.notesInSeconds[notesInSecondsIndex];
        } else {
            SongManager.Instance.Stop();
            playing = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class ScenePrototypeManager : MonoBehaviour {

    private static ScenePrototypeManager instance;

    public static ScenePrototypeManager Instance { get { return instance; } }

    public SongManager sm;

    public Song song;

    public BossController boss;

    public Transform player;

    public float noteToPlayInSeconds = 0;

    private bool playing = true;


    public float projectileSpeed = 10f;
    public float spawnHeight = 12f;

    public CircleMetronome metronome;

    public List<Note> notesInSeconds = new List<Note>();
    public int notesInSecondsIndex = 0;

    [System.Serializable]
    public class Note {

        public float notePosInSeconds;
        public UnityEvent noteFunction;

        public Note(float notePos) {
            notePosInSeconds = notePos;
        }
    }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }

        sm.SetSong(song);
        metronome.StartMetronome();
    }

    private void FixedUpdate() {

        sm.UpdateSongValues();

        if (playing) {
            if (noteToPlayInSeconds == 0) {
                noteToPlayInSeconds = song.notesInSeconds[notesInSecondsIndex];
            }

            if(noteToPlayInSeconds <= SongManager.Instance.SongPositionInSeconds) {
                notesInSeconds[notesInSecondsIndex].noteFunction.Invoke();
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

    /// <summary>
    /// Calculates the position of every beat in seconds and adds it to the list
    /// </summary>
    public void setNotesInSeconds() {

        float secondsPerBeat = 60 / song.bpm;
        float barStartTime = 0;

        notesInSeconds.Clear();

        for (int i = 0; i < song.bars.Count; i++) {

            if (i != 0) {
                barStartTime += song.bars[i - 1].durationInBeats * secondsPerBeat;
            }

            for (int j = 0; j < song.bars[i].notes.Count; j++) {
                notesInSeconds.Add(new Note(barStartTime + song.bars[i].notes[j] * secondsPerBeat));
            }

        }
    }

   
}
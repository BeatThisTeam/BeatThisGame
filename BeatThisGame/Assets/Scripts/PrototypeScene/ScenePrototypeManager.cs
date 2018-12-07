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

    public GroundSections ground;

    public Transform player;
    CharacterController playerCharContr;

    public float noteToPlayInSeconds = 0;

    private bool playing = true;


    public float projectileSpeed = 10f;
    public float spawnHeight = 12f;

    public CircleMetronome metronome;

    public List<Note> notesInSeconds = new List<Note>();
    public int notesInSecondsIndex = 0;

    private Animator bossAnim;

    [System.Serializable]
    public class Note {

        public float notePosInSeconds;
        public bool playerShouldPlay;
        public bool specialAttack;
        public UnityEvent noteFunction;

        [HideInInspector]
        public bool removable = true;

        public Note(float notePos) {
            notePosInSeconds = notePos;
            removable = false;
        }
    }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }

        bossAnim = boss.GetComponent<Animator>();
        playerCharContr = player.GetComponent<CharacterController>();
    }

    private void Start() {

        sm.SetSong(song);
        boss.StartIdle();
        ScoreManager.Instance.Setup();
        metronome.StartMetronome();
        StartCoroutine(CheckIfTileHurts(0.5f));
    }

    private void FixedUpdate() {

        sm.UpdateSongValues();
        ScoreManager.Instance.UpdateNoteToHit();

        if (playing) {
            if (noteToPlayInSeconds == 0) {
                noteToPlayInSeconds = notesInSeconds[notesInSecondsIndex].notePosInSeconds;
                ScoreManager.Instance.nextNoteToHit(notesInSecondsIndex);
            }

            if(noteToPlayInSeconds <= SongManager.Instance.SongPositionInSeconds) {
                notesInSeconds[notesInSecondsIndex].noteFunction.Invoke();
                IncrementNoteToPlayInSeconds();
            }
        }
    }

    IEnumerator CheckIfTileHurts(float interval) {

        while (playing) {

            for(int i = 0; i< ground.rings.Count; i++) {
                for (int j = 0; j < ground.rings[i].sections.Count; j++) {
                    if (ground.rings[i].sections[j].hurts) {
                        ground.Hurts(i, j);
                    }
                }
            }

            yield return new WaitForSeconds(interval);
        }
    }

    public void IncrementNoteToPlayInSeconds() {

        notesInSecondsIndex++;
        if (notesInSecondsIndex < notesInSeconds.Count) {
            noteToPlayInSeconds = notesInSeconds[notesInSecondsIndex].notePosInSeconds;
        } else {
            SongManager.Instance.Stop();
            playing = false;
        }
    }

    /// <summary>
    /// Calculates the position of every beat in seconds and updates the list
    /// </summary>
    public void setNotesInSeconds() {

        float secondsPerBeat = 60 / song.bpm;
        float barStartTime = 0;
        float noteInSeconds;

        //All the notes are flagged as removable at beginning
        for (int i = 0; i< notesInSeconds.Count; i++) {
            notesInSeconds[i].removable = true;
        }

        //Iterate on each note in the Song object and calculate its position in seconds within the song
        for (int i = 0; i < song.bars.Count; i++) {

            if (i != 0) {
                barStartTime += song.bars[i - 1].durationInBeats * secondsPerBeat;
            }

            for (int j = 0; j < song.bars[i].notes.Count; j++) {

                bool found = false;
                noteInSeconds = barStartTime + song.bars[i].notes[j] * secondsPerBeat;

                //If a note is already in the list it is flagged as not removable
                for(int k = 0; k< notesInSeconds.Count; k++) {
                    if(notesInSeconds[k].notePosInSeconds == noteInSeconds) {
                        notesInSeconds[k].removable = false;
                        found = true;
                    }
                }

                //If a not is not in the list it is added
                if (!found) {
                    notesInSeconds.Add(new Note(noteInSeconds));
                }
            }
        }

        //If a note is still flagged as removable it means that has been removed from the Song object so it is removed from the list
        for (int i = 0; i< notesInSeconds.Count; i++) {
            if (notesInSeconds[i].removable) {
                notesInSeconds.RemoveAt(i);
                i--;
            }
        }

        //Finally the list is sorted 
        notesInSeconds.Sort((n1, n2) => n1.notePosInSeconds.CompareTo(n2.notePosInSeconds));
    }

    //public void copia() {
    //    notesInSeconds.Clear();
    //    for (int i = 0; i < n.Count; i++) {
    //        Note note = new Note(n[i].notePosInSeconds);
    //        note.noteFunction = n[i].noteFunction;
    //        notesInSeconds.Add(note);
    //    }
    //}
}
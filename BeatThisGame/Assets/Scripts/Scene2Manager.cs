using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Manager : MonoBehaviour {

    private static Scene2Manager instance;

    public static Scene2Manager Instance { get { return instance; } }

    public GameObject noteGameObject;
    private Note note;
    public Transform noteSpawnPoint;

    public Song song;

    public float noteToPlayInSeconds = 0;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }

        note = noteGameObject.GetComponent<Note>();
    }

    void Start() {

        song = SongManager.Instance.song;      
    }

    void Update() {
        if(noteToPlayInSeconds == 0) {
            noteToPlayInSeconds = FindNextNote();
        }

        //This is the only different thinf from Scene1Manager:
        //Now to spawn a note I calculate the time it needs to reach its final position and i check if its position in time
        //minus the current position of the song is equal to that value
        if(noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= Vector3.Distance(note.spawnPos,note.removePos) / note.velocity) {
            Instantiate(noteGameObject, noteSpawnPoint.position, Quaternion.identity);
            noteToPlayInSeconds = FindNextNote();
        }
    }

    private float FindNextNote() {

        int barIndex = SongManager.Instance.SongPositionInBars;
        float nextNote = 0;

        while(nextNote == 0) {
            if (song.bars[barIndex].notes.Count > 0) {
                for(int i = 0; i < song.bars[barIndex].notes.Count; i++) {
                    float n = SongManager.Instance.BeatsPosToTimePos(barIndex, song.bars[barIndex].notes[i]);
                    if (n > noteToPlayInSeconds) {
                        nextNote = n;
                        return nextNote;
                    }
                }
                barIndex++;
            }else {
                barIndex++;
            }
        }
        return nextNote;
    }
}

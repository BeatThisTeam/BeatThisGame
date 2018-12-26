using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour {

    private static Scene1Manager instance;

    public static Scene1Manager Instance { get { return instance; } }

    //The note(s) we use in this scene
    public GameObject noteGameObject;
    private Cube cube;

    public Song song;

    //The position in seconds of the next note to play
    public float noteToPlayInSeconds = 0;

    private void Awake() {

        //Singleton pattern stuff
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }

        cube = noteGameObject.GetComponent<Cube>();
    }

    void Start() {

        //We get the song from the song manager, actually maybe it would be better the other way round :P
        song = SongManager.Instance.song;
    }

    void Update() {

        //If it is == 0 means there are no note selected to play yet (I didn't put this in start or awake 
        //because I couldn't know if songManager would be ready)
        if(noteToPlayInSeconds == 0) {
            noteToPlayInSeconds = FindNextNote();
        }

        //In this example I simply check if it is time to play the next note, if so I play it and I search the next note
        if(noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= 0) {
            cube.Expand();
            noteToPlayInSeconds = FindNextNote();
        }
    }


    //Finds the next note to play in the song
    private float FindNextNote() {

        //I start to search from the bar we are playing right now
        int barIndex = SongManager.Instance.SongPositionInBars;
        float nextNote = 0;

        //I loop until I find a note to play
        while (nextNote == 0) {

            //I check the bar only if it contains notes
            if (song.bars[barIndex].notes.Count > 0) {

                //For every note I check its position in time to pick the first one after the one I already have
                for (int i = 0; i < song.bars[barIndex].notes.Count; i++) {
                    float n = SongManager.Instance.BeatsPosToTimePos(barIndex, song.bars[barIndex].notes[i]);
                    if (n > noteToPlayInSeconds) {
                        nextNote = n;
                        return nextNote;
                    }
                }
                barIndex++;
            } else {
                barIndex++;
            }
        }
        return nextNote;
    }
}

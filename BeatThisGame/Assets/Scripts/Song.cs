using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable object that represents a song
[CreateAssetMenu]
public class Song : ScriptableObject {

    //It is wrote as a subclass so every bar we add to the song can be edited directly in the inspector window
    [System.Serializable]
    public class Bar{

        //position in beats of all the notes in the bar
        public List<float> notes;

        //How many beats lasts
        public float durationInBeats;
    }

    

    public AudioClip track;
    public float bpm;

    //Here we store all the bars of the song
    public List<Bar> bars = new List<Bar>();

    //List of every beat in seconds
    public List<float> notesInSeconds = new List<float>();

    public Bar currentBar;  

    /// <summary>
    /// Calculates the position of every beat in seconds and adds it to the list
    /// </summary>
    public void setNotesInSeconds() {

        float secondsPerBeat = 60 / bpm;
        float barStartTime = 0;

        notesInSeconds.Clear();

        for (int i = 0; i < bars.Count; i++) {

            if(i != 0) {
                barStartTime += bars[i - 1].durationInBeats * secondsPerBeat;
            }

            for(int j = 0; j < bars[i].notes.Count; j++) {
                notesInSeconds.Add(barStartTime + bars[i].notes[j] * secondsPerBeat);
            }
          
        }
    }

}

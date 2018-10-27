using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour {

    private static SongManager instance;

    public static SongManager Instance { get { return instance;  } }

    //Song played in this stage
    public Song song;

    //The audiosuorce attached to this gameobject
    private AudioSource audioSource;

    //Song informations
    private float bpm;                        //beats per minute of the song

    private float secondsPerBeat;             //how many seconds for each beat
    public float  SecondsPerBeat        { get { return secondsPerBeat; } }

    private float songPositionInSeconds;      //how many seconds since the song started
    public float  SongPositionInSeconds { get { return songPositionInSeconds; } }

    private float songPositionInBeats;        //how many beats since the song started
    public float  SongPositionInBeats   { get { return songPositionInBeats; } }

    private int   songPositionInBars;         //how many bars since the song started
    public int    SongPositionInBars    { get { return songPositionInBars; } }

    private float barPositionInSeconds;       //how many seconds since the last bar started
    public float BarPositionInSeconds   { get { return barPositionInSeconds; } }

    private float barPositionInBeats;         //how many beats since the last bar started
    public float  BarPositionInBeats    { get { return barPositionInBeats; } }

    private float dspSongTime;                //record the time when the song starts
    private float dspBarTime;                 //record the time when the last bar starts

    //The naming here is a bit messy, I used next but actually they are the indexes of the current bar and the 
    //current note played, the thing is that they're update before we actually get to that note or that bar so 
    //that's why I choose "next". This should be more clear in the update function.
    //This indexes are used for the lists in the Song object (might be better to move the there? Probably but I think
    //the code is a little bit cleaner having them here ahah)
    private int nextBarIndex;                 //index of the next bar to play 
    private int nextNoteIndex;                //index of the next note to play (in respect of the bar)
    private bool playing = true;

    private void Awake() {

        //Singleton patter stuff
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
        
        //Initialization of some variables
        
        nextBarIndex = 0;
        nextNoteIndex = 0;
        audioSource = GetComponent<AudioSource>();
        
    }

    void Start() {

        
    }

    //Not sure yet if it should be better to use FixedUpdate instead 
    void FixedUpdate() {

        if (playing) {
            //We update all our song position variables
            float newSongPositionInSeconds = (float)(AudioSettings.dspTime - dspSongTime);

            if(songPositionInSeconds != newSongPositionInSeconds) {
                songPositionInSeconds = newSongPositionInSeconds;
            } else {
                songPositionInSeconds += Time.unscaledDeltaTime;
            }
            songPositionInSeconds = (float)(AudioSettings.dspTime - dspSongTime);
            barPositionInSeconds = (float)(AudioSettings.dspTime - dspBarTime);
            songPositionInBeats = songPositionInSeconds / secondsPerBeat;
            barPositionInBeats = barPositionInSeconds / secondsPerBeat;

            //Shit gets serious! 
            //probably there is a simpler way than this but unfortunately this is the best solution I found  to keep 
            //track of everything (hopefully you'll be more clever than me to find a more elegant solution :P).
            //Let's go!

            //If the time passed since the current bar started is equal to the duration of the bar itself means 
            //that a new bar starts
            if (barPositionInSeconds >= song.bars[nextBarIndex].durationInBeats * secondsPerBeat) {
                songPositionInBars++;
                dspBarTime = (float)(AudioSettings.dspTime);
            }

            //If the current bar contains note to play (Count returns how many elements are in a list) we play them
            if (song.currentBar.notes.Count > 0) {

                //Here is the trick: since nextBarIndex is incremented when we play the last note (see below) then we must be sure
                //that the song has actually reached the next bar. For example: if the song is at 60 bpm (1 beat = 1 sec)
                //the last beat lasts 1 second before we can actually start to play the next bar.
                //So the first condition is to checks if the song actually reached the next bar to play, 
                //the second condition checks if we have reached a beat to play
                if (songPositionInBars == nextBarIndex && barPositionInBeats >= song.currentBar.notes[nextNoteIndex]) {

                    nextNoteIndex++;

                    //If we have played all the notes in bar we can increment the bar index then we'll wait the song
                    //to actually reach the next bar (see above)
                    if (nextNoteIndex == song.currentBar.notes.Count) {
                        nextBarIndex++;
                        song.currentBar = song.bars[nextBarIndex];
                        nextNoteIndex = 0;
                    }
                }
                //Otherwise we just wait the end of the bar
            } else if (barPositionInBeats >= song.currentBar.durationInBeats) {
                nextBarIndex++;
                song.currentBar = song.bars[nextBarIndex];
            }
            //Whoohoo done!!
        }
    }

    public void SetSong(Song song) {

        this.song = song;
        bpm = song.bpm;
        audioSource.clip = song.track;
        secondsPerBeat = 60f / bpm;

        //The timer of the audiotrack is more precise than the timer of the scene so we use this
        //to record the exact time when the audio track starts
        dspSongTime = (float)AudioSettings.dspTime;

        //The first bar obviously starts with the song
        dspBarTime = dspSongTime;
        audioSource.Play();

        //We keep track of the bar we are playing both in the variable songPositionInBars and in the 
        //song object (might be redundant, we'll see)
        song.currentBar = song.bars[nextBarIndex];
        songPositionInBars = 0;
        song.setNotesInSeconds();
    }

    //Just a simple function to get the exact time position of a note given its bar and its position in the bar
    public float BeatsPosToTimePos(int bar, float beat) {
        
        float time = 0;

        for(int i = 0; i < bar; i++) {
            time += song.bars[i].durationInBeats * secondsPerBeat;
        }

        time += beat * secondsPerBeat;

        return time;
    }

    public void Stop() {
        audioSource.Stop();
        playing = false;
    }
}


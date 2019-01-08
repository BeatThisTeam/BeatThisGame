using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLight : MonoBehaviour {

    public Song song;
    public float bpm;
    public Material mat;

    public int patternIndex = 0;
    public List<Pattern> patterns;

    private bool free = true;
    private bool playing = true;

    [ColorUsageAttribute(true,true)]
    public Color color1;
    [ColorUsageAttribute(true, true)]
    public Color color2;

    [System.Serializable]
    public class Pattern {

        public float startTime;
        public int num;
        public int noteIndex = 0;
        public float[] notes;
    }

    private void Start() {

        float secPBeat = 60 / bpm;

        patterns = new List<Pattern>() {
            new Pattern(){ startTime = 32.72727f, num = 3, noteIndex = 0, notes = new float[] {
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat*2,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat,
                (secPBeat/4)*3,
                secPBeat/4,
                (secPBeat/4)*2,
            } },
            new Pattern(){ startTime = 85.09091f, num = 7, noteIndex = 0, notes = new float[] {
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat*2,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat,
                (secPBeat/4)*3,
                secPBeat/4,
                (secPBeat/4)*2,
            } },
            new Pattern(){ startTime = 207.2729f, num = 3, noteIndex = 0, notes = new float[] {
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat*2,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat/4,
                secPBeat,
                (secPBeat/4)*3,
                secPBeat/4,
                (secPBeat/4)*2,
            } },
        };
        
        mat = GetComponent<Renderer>().material;
    }

    private void FixedUpdate() {

        if (playing) {
            if (patterns[patternIndex].startTime <= SongManager.Instance.SongPositionInSeconds) {
                free = false;
                int noteIndex = patterns[patternIndex].noteIndex;

                //Debug.Log(noteIndex);
                //Debug.Log(patternIndex);               
                StartCoroutine(Light(0.05f));
                //Light();
                patterns[patternIndex].startTime += patterns[patternIndex].notes[noteIndex];

                if (patterns[patternIndex].noteIndex < patterns[patternIndex].notes.Length - 1) {                   
                    patterns[patternIndex].noteIndex++;
                } else {
                    if(patterns[patternIndex].num > 0) {
                        patterns[patternIndex].num--;
                        patterns[patternIndex].noteIndex = 0;
                    }
                    else if (patternIndex < patterns.Count - 1) {
                        patternIndex++;
                    } else {
                        playing = false;
                    }
                }

                
            }
        }      
    }

    private IEnumerator Light(float duration) {

        mat.color = color2;
        yield return new WaitForSeconds(duration);
        mat.color = color1;
    }

    private void Light() {

        if(mat.color == color1) {
            mat.color = color2;
        } else {
            mat.color = color1;
        }
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLight2 : MonoBehaviour {

    public AudioVisualization audioPeer;
    public float maxIntensity;
    public float maxRange;
    public float mult;
    public bool useBuffer;
    public enum Param { AudioBands, Amplitude };
    public Param param;

    public int band;

    Light l;

    // Use this for initialization
    void Start() {
        l = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update() {

        if (param == Param.AudioBands) {
            if (l != null && audioPeer.audioBandBuffer[band] > 0) {
                if (useBuffer) {
                    l.intensity = audioPeer.audioBandBuffer[band] * maxIntensity * mult;
                    l.range = audioPeer.audioBandBuffer[band] * maxRange * mult;
                } else {
                    l.intensity = audioPeer.audioBand[band] * maxIntensity * mult;
                    l.range = audioPeer.audioBand[band] * maxRange * mult;
                }

            }

        }

        if (param == Param.Amplitude) {
            if (l != null && audioPeer.amplitudeBuffer > 0) {
                if (useBuffer) {
                    l.intensity = audioPeer.amplitudeBuffer * maxIntensity * mult;
                    l.range = audioPeer.amplitudeBuffer * maxRange * mult;
                } else {
                    l.intensity = audioPeer.amplitude * maxIntensity * mult;
                    l.range = audioPeer.amplitude * maxRange * mult;
                }

            }
        }
    }
}

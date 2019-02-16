using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLight : MonoBehaviour {

    public AudioVisualization audioPeer;
    public float maxIntensity;
    public float maxRange;
    public float mult;
    Light[] lights = new Light[8];
    public Light lightPrefab;
    public float lightsCircleRadius;
    public bool useBuffer;
    public enum Param {AudioBands, Amplitude};
    public Param param;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 8; i++) {
            Light light = Instantiate(lightPrefab);
            light.transform.position = this.transform.position;
            light.transform.parent = this.transform;
            light.name = "AudioLight" + i;
            this.transform.eulerAngles = new Vector3(0, -(360 / 8) * i, 0);
            light.transform.position = Vector3.forward * lightsCircleRadius + new Vector3(0, transform.position.y, 0);
            lights[i] = light;
        }
    }

    // Update is called once per frame
    void Update () {

        if (param == Param.AudioBands) {
            for (int i = 0; i < 8; i++) {
                if (lights != null && audioPeer.audioBandBuffer[i] > 0) {
                    if (useBuffer) {
                        lights[i].intensity = audioPeer.audioBandBuffer[i] * maxIntensity * mult;
                        lights[i].range = audioPeer.audioBandBuffer[i] * maxRange * mult;
                    } else {
                        lights[i].intensity = audioPeer.audioBand[i] * maxIntensity * mult;
                        lights[i].range = audioPeer.audioBand[i] * maxRange * mult;
                    }

                }
            }
        }

        if (param == Param.Amplitude) {
            for (int i = 0; i < 8; i++) {
                if (lights != null && audioPeer.amplitudeBuffer > 0) {
                    if (useBuffer) {
                        lights[i].intensity = audioPeer.amplitudeBuffer * maxIntensity * mult;
                    } else {
                        lights[i].intensity = audioPeer.amplitude * maxIntensity * mult;
                    }

                }
            }
        }
    }
}

using System.Collections;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioVisualization : MonoBehaviour {

    AudioSource audioSource;

    //20KHz divisi in 512
    private float[] samplesLeft = new float[512];
    private float[] samplesRight = new float[512];

    //valori non normalizzati
    private float[] frequencyBands = new float[8];
    private float[] bandBuffer = new float[8];
    private float[] bufferDecrease = new float[8];
    private float[] freqBandHighest = new float[8];

    //64
    private float[] frequencyBands64 = new float[64];
    private float[] bandBuffer64 = new float[64];
    private float[] bufferDecrease64 = new float[64];
    private float[] freqBandHighest64 = new float[64];

    //valori normalizzati
    public float[] audioBand;
    public float[] audioBandBuffer;

    public float[] audioBand64;
    public float[] audioBandBuffer64;

    public float amplitude;
    public float amplitudeBuffer;
    private float amplitudeHighest;

    public float audioProfile;

    public enum Channel {Stereo, Left, Right};
    public Channel channel = new Channel();
    
	void Start () {
        audioBand = new float[8];
        audioBandBuffer = new float[8];
        audioBand64 = new float[64];
        audioBandBuffer64 = new float[64];

        audioSource = GetComponent <AudioSource> ();
        AudioProfile();
        AudioProfile64();
	}
	
	void Update () {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        MakeFrequencyBands64();
        BandBuffer();
        BandBuffer64();
        CreateAudioBand();
        CreateAudioBand64();
        GetAmplitude();
    }

    void GetSpectrumAudioSource() {

        audioSource.GetSpectrumData(samplesLeft, 0, FFTWindow.Blackman);
        audioSource.GetSpectrumData(samplesRight, 1, FFTWindow.Blackman);
    }

    //Divide lo spettro in 8 zone
    void MakeFrequencyBands() {

        /*
         * 22050 / 512 = 43Hz per sample
         * 20 - 60 Hz
         * 60 - 250 Hz
         * 250 - 500 Hz
         * 500 - 2K Hz
         * 2K - 4K Hz
         * 4K - 6K Hz
         * 6K - 20K Hz
         * 
         * 0 - 2 = 86 Hz
         * 1 - 4 = 172 Hz  87-258
         * 2 - 8 = 344 Hz  259-602
         * 3 - 16 = 688 Hz  603-1290
         * 4 - 32 = 1376 Hz  1291-2666
         * 5 - 64 = 2752 Hz  2667-5418
         * 6 - 128 = 5504 Hz  5419-10922
         * 7 - 256 = 11008 Hz 10923-21930
         * 
         * total 510 samples
         */

        int count = 0;

        for (int i = 0; i < 8; i++) {

            float avg = 0;
            int sampleCount = (int)Mathf.Pow(2, i + 1);
            
            if(i == 7) {
                //include also last two spectrum samples
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++) {
                if(channel == Channel.Stereo) {
                    avg += (samplesLeft[count] + samplesRight[count]) * (count + 1);
                }else if(channel == Channel.Left) {
                    avg += (samplesLeft[count] * (count + 1));
                } else if (channel == Channel.Right) {
                    avg += (samplesRight[count] * (count + 1));
                }
                count++;
            }

            avg /= count;

            frequencyBands[i] = avg * 10;
        }
    }

    void MakeFrequencyBands64() {

        /*
         * 0 - 15 = 1 sample   =16
         * 16 - 31 = 2 sample  =32
         * 32 - 39 = 4 sample  =32
         * 40 - 47 = 6 sample  =48
         * 48 - 55 = 16 sample =128
         * 56 - 63 = 32 sample =256
         */

        int count = 0;
        int sampleCount = 1;
        int power = 0;

        for (int i = 0; i < 64; i++) {

            float avg = 0;

            if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56) {
                //include also last two spectrum samples
                power++;
                sampleCount = (int)Mathf.Pow(2, power);
                if(power == 3) {
                    sampleCount -= 2;
                }
            }

            for (int j = 0; j < sampleCount; j++) {
                if (channel == Channel.Stereo) {
                    avg += (samplesLeft[count] + samplesRight[count]) * (count + 1);
                } else if (channel == Channel.Left) {
                    avg += (samplesLeft[count] * (count + 1));
                } else if (channel == Channel.Right) {
                    avg += (samplesRight[count] * (count + 1));
                }
                count++;
            }

            avg /= count;

            frequencyBands64[i] = avg * 80;
        }
    }

    void BandBuffer() {

        for(int i = 0; i < 8; i++) {
            if(frequencyBands[i] > bandBuffer[i]) {
                bandBuffer[i] = frequencyBands[i];
                bufferDecrease[i] = 0.005f;
            }

            if (frequencyBands[i] < bandBuffer[i]) {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
    }

    void BandBuffer64() {

        for (int i = 0; i < 64; i++) {
            if (frequencyBands64[i] > bandBuffer64[i]) {
                bandBuffer64[i] = frequencyBands64[i];
                bufferDecrease64[i] = 0.005f;
            }

            if (frequencyBands64[i] < bandBuffer64[i]) {
                bandBuffer64[i] -= bufferDecrease64[i];
                bufferDecrease64[i] *= 1.2f;
            }
        }
    }

    void CreateAudioBand() {

        for(int i = 0; i < 8; i++) {

            if(frequencyBands[i] > freqBandHighest[i]) {
                freqBandHighest[i] = frequencyBands[i];
            }

            audioBand[i] = frequencyBands[i] / freqBandHighest[i];
            audioBandBuffer[i] = bandBuffer[i] / freqBandHighest[i];
        }
    }

    void CreateAudioBand64() {

        for (int i = 0; i < 64; i++) {

            if (frequencyBands64[i] > freqBandHighest64[i]) {
                freqBandHighest64[i] = frequencyBands64[i];
            }

            audioBand64[i] = frequencyBands64[i] / freqBandHighest64[i];
            audioBandBuffer64[i] = bandBuffer64[i] / freqBandHighest64[i];
        }
    }

    void GetAmplitude() {

        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;

        for(int i = 0; i < 8; i++) {
            currentAmplitude += audioBand[i];
            currentAmplitudeBuffer += audioBandBuffer[i];
        }

        if(currentAmplitude > amplitudeHighest) {
            amplitudeHighest = currentAmplitude;
        }

        amplitude = currentAmplitude / amplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
    }

    void AudioProfile() {

        for(int i = 0; i < 8; i++) {

            freqBandHighest[i] = audioProfile;
        }
    }
    
    void AudioProfile64() {

        for (int i = 0; i < 64; i++) {

            freqBandHighest64[i] = audioProfile;
        }
    }

}

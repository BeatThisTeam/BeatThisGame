using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    
    private static SoundManager instance;

    public static SoundManager Instance { get { return instance; } }

    public AudioClip moveSound;

    private AudioSource audioSource;

    void Awake() {

        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMoveSound() {

        if(audioSource != moveSound) {
            audioSource.clip = moveSound;
        }
        
        audioSource.Play();
    }
}

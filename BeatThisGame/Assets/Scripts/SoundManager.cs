using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    
    private static SoundManager instance;

    public static SoundManager Instance { get { return instance; } }

    public AudioClip moveSound;
    public AudioClip shieldSound;
    public List<AudioClip> characterDamage;
    public List<AudioClip> bossDamage;

    private AudioSource audioSource;

    void Awake() {

        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void PlayClip(AudioClip clip) {

        if (audioSource != clip) {
            audioSource.clip = clip;
        }

        audioSource.Play();
    }

    private void PlayRandomClip(List<AudioClip> clips) {

        int numClips = clips.Count;
        int clipIndex = Random.Range(0, numClips - 1);
        PlayClip(clips[clipIndex]);
    }

    public void PlayMoveSound() {

        PlayClip(moveSound);
    }

    public void PlayBossDamageSound() {

        PlayRandomClip(bossDamage);
    }

    public void PlayCharacterDamageSound() {

        PlayRandomClip(characterDamage);
    }

    public void PlayShieldSound() {

        PlayClip(shieldSound);
    }
}

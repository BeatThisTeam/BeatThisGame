﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private static ScoreManager instance;

    public static ScoreManager Instance { get { return instance; } }

    private float noteToHit;

    [Header("Delta Accuracy")]
    public float deltaAccuracy;

    [Header("Accuracy Percentages")]
    public float perfectAccuracy;
    public float goodAccuracy;
    public float okAccuracy;

    [Header("Special Attack Stats")]
    public float specialAttackTotalDamage;
    public float specialAttackMaxPower;
    public float specialAttackPower;
    public float specialAttackAccuracy;

    [Header("Boss Life")]
    public float maxBossHealth;
    public float currentBossHealth;

    [Space]
    public int numNotesInSection;
    public int numSpecialAttacks;
    public int numOtherAttacks;

    private int lastSpecialAttackIndex;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    public void Setup() {

        numNotesInSection = CalcNumNotesInSection(0);
        numSpecialAttacks = CalcNumSpecialAttacks();
        //CalcNumOtherAttacks();

        currentBossHealth = maxBossHealth;

        specialAttackMaxPower = maxBossHealth * specialAttackTotalDamage / numSpecialAttacks;

        specialAttackPower = 0;       
    }

    private int CalcNumNotesInSection(int index) {

        specialAttackPower = 0;
        int counter = 0;
        for (int i = index; i < ScenePrototypeManager.Instance.notesInSeconds.Count; i++) {
           
            if (ScenePrototypeManager.Instance.notesInSeconds[i].specialAttack && counter != 0) {
                lastSpecialAttackIndex = i;
                return counter;
            }
            if (ScenePrototypeManager.Instance.notesInSeconds[i].playerShouldPlay) {
                //Debug.Log(ScenePrototypeManager.Instance.notesInSeconds[i].notePosInSeconds);
                counter++;
            }
        }
        return -1;
    }

    private int CalcNumSpecialAttacks() {

        int counter = 0;
        for (int i = 0; i < ScenePrototypeManager.Instance.notesInSeconds.Count; i++) {
            if (ScenePrototypeManager.Instance.notesInSeconds[i].specialAttack) {
                counter++;
            }
        }
        return counter;
    }

    public void HitNote() {
        //Debug.Log("delta: " + Mathf.Abs(noteToHit - SongManager.Instance.SongPositionInSeconds));
        //Debug.Log("songpos: " + SongManager.Instance.SongPositionInSeconds);
        //Debug.Log("targetpos: " + noteToHit);

        float diff = Mathf.Abs(noteToHit - SongManager.Instance.SongPositionInSeconds);

        if (diff < deltaAccuracy) {

            if(diff < deltaAccuracy && diff > deltaAccuracy / 2) {
                //Debug.Log("ok");
                specialAttackPower += specialAttackMaxPower * okAccuracy / numNotesInSection;
            }else if (diff <= deltaAccuracy / 2 && diff > deltaAccuracy / 6) {
                //Debug.Log("good");
                specialAttackPower += specialAttackMaxPower * goodAccuracy / numNotesInSection;
            } else if (diff <= deltaAccuracy / 6) {
                //Debug.Log("perfect");
                specialAttackPower += specialAttackMaxPower * perfectAccuracy / numNotesInSection;
            }

            if (SongManager.Instance.SongPositionInSeconds > noteToHit) {
                nextNoteToHit(ScenePrototypeManager.Instance.notesInSecondsIndex);
            } else {
                nextNoteToHit(ScenePrototypeManager.Instance.notesInSecondsIndex + 1);
            }
        }
    }

    public void HitSpecialAttack() {

        float diff = Mathf.Abs(noteToHit - SongManager.Instance.SongPositionInSeconds);

        if (diff < deltaAccuracy) {

            if (diff < deltaAccuracy && diff > deltaAccuracy / 2) {
                //Debug.Log("ok");
                specialAttackAccuracy = okAccuracy;
            } else if (diff <= deltaAccuracy / 2 && diff > deltaAccuracy / 6) {
                //Debug.Log("good");
                specialAttackAccuracy = goodAccuracy;
            } else if (diff <= deltaAccuracy / 6) {
                //Debug.Log("perfect");
                specialAttackAccuracy = perfectAccuracy;
            }

            if (SongManager.Instance.SongPositionInSeconds > noteToHit) {
                nextNoteToHit(ScenePrototypeManager.Instance.notesInSecondsIndex);
            } else {
                nextNoteToHit(ScenePrototypeManager.Instance.notesInSecondsIndex + 1);
            }
        }

        UpdateBossHealth();

        numNotesInSection = CalcNumNotesInSection(lastSpecialAttackIndex);
    }

    public void SpecialAttackMiss() {

        numNotesInSection = CalcNumNotesInSection(lastSpecialAttackIndex);
    }

    public void nextNoteToHit(int index) {

        float prevNoteToHit = noteToHit;

        for (int i = index; i < ScenePrototypeManager.Instance.notesInSeconds.Count; i++) {
            if (ScenePrototypeManager.Instance.notesInSeconds[i].playerShouldPlay) {
                noteToHit = ScenePrototypeManager.Instance.notesInSeconds[i].notePosInSeconds;
                return;
            }
        }
    }

    public void UpdateNoteToHit() {
        if(noteToHit < SongManager.Instance.SongPositionInSeconds - deltaAccuracy) {
            //Debug.Log("MISS");
            nextNoteToHit(ScenePrototypeManager.Instance.notesInSecondsIndex);
        }
    }

    public void UpdateBossHealth() {

        float damage = specialAttackPower * specialAttackAccuracy;
        Debug.Log(damage);
        currentBossHealth -= damage;
    }
}

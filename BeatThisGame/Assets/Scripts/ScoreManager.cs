using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private static ScoreManager instance;

    public static ScoreManager Instance { get { return instance; } }

    public float noteToHit;

    [Header("Delta Accuracy")]
    public float deltaAccuracy;

    [Header("Accuracy Percentages")]
    public float perfectAccuracy;
    public float goodAccuracy;
    public float okAccuracy;

    public float currentAccuracy = 0f;

    [Header("Special Attack Stats")]
    public float specialAttackTotalDamage;
    public float specialAttackMaxPower;
    public float specialAttackPower;
    public float specialAttackAccuracy;

    [Header("Boss Life")]
    public float maxBossHealth;
    public float currentBossHealth;

    [Space]
    public GroundSections ground;
    public int numNotesInSection;
    public int numSpecialAttacks;
    public int numOtherAttacks;

    public PowerAttack specialAttackUI;
    public BossHealth bossHealthBarUI;
    
    public ChangeText changeText;

    private int lastSpecialAttackIndex;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    public void Setup() {

        numSpecialAttacks = CalcNumSpecialAttacks();

        currentBossHealth = maxBossHealth;

        specialAttackMaxPower = maxBossHealth * specialAttackTotalDamage / numSpecialAttacks;

        specialAttackPower = 0;

        specialAttackUI.Setup(specialAttackMaxPower);
        bossHealthBarUI.Setup(maxBossHealth);

        numNotesInSection = CalcNumNotesInSection(0);
        
        //CalcNumOtherAttacks();
    }

    private int CalcNumNotesInSection(int index) {

        specialAttackPower = 0;
        specialAttackUI.UpdateBar(specialAttackPower);
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

    public void HitNote(int facePos, int ringPos) {
        //Debug.Log("delta: " + Mathf.Abs(noteToHit - SongManager.Instance.SongPositionInSeconds));
        //Debug.Log("songpos: " + SongManager.Instance.SongPositionInSeconds);
        //Debug.Log("targetpos: " + noteToHit);
        //Debug.Log(facePos +" "+ ringPos);
        float diff = Mathf.Abs(noteToHit - SongManager.Instance.SongPositionInSeconds);
        //if(diff > deltaAccuracy) {
        //    Debug.Log(noteToHit);
        //    Debug.Log(SongManager.Instance.SongPositionInSeconds);
        //    Debug.Log(diff);
        //}
        //Debug.Assert(ground.rings[ringPos].sections[facePos].isTarget);
        if (diff < deltaAccuracy && ground.rings[ringPos].sections[facePos].isTarget) {

            if(diff < deltaAccuracy && diff > deltaAccuracy / 2) {
                Debug.Log("ok");
                specialAttackPower += specialAttackMaxPower * okAccuracy / numNotesInSection;
                currentAccuracy = 1f; //okAccuracy;
                changeText.UpdateText(1f);
            }
            else if (diff <= deltaAccuracy / 2 && diff > deltaAccuracy / 6) {
                Debug.Log("good");
                specialAttackPower += specialAttackMaxPower * goodAccuracy / numNotesInSection;
                currentAccuracy = 2f; // goodAccuracy;
                changeText.UpdateText(2f);

            } else if (diff <= deltaAccuracy / 6) {
                Debug.Log("perfect");
                specialAttackPower += specialAttackMaxPower * perfectAccuracy / numNotesInSection;
                currentAccuracy = 3f; // perfectAccuracy;
                changeText.UpdateText(3f);
            }

            //EventManager.TriggerEvent("note");
            specialAttackUI.UpdateBar(specialAttackPower);

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
        bossHealthBarUI.UpdateBar(currentBossHealth);
    }

    public float getAccuracy()
    {
        return currentAccuracy;
    }
    
}


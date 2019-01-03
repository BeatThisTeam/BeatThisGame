using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private static ScoreManager instance;

    public static ScoreManager Instance { get { return instance; } }

    public float noteToHit;

    [Header("Accuracy")]
    public float deltaAccuracy;
    public float accuracy;

    [Header("Accuracy Percentages")]
    public float perfectAccuracy;
    public float goodAccuracy;
    public float okAccuracy;

<<<<<<< HEAD
    [Header("Attack Stats")]
=======
    public float currentAccuracy = 0f;

    [Header("Special Attack Stats")]
>>>>>>> Alejandra
    public float specialAttackTotalDamage;
    public float specialAttackMaxPower;
    public float normalAttackMaxPower;
    public float specialAttackPower;
    public float specialAttackAccuracy;

    [Header("Boss Life")]
    public float maxBossHealth;
    public float currentBossHealth;

    [Space]
    public GroundSections ground;
    public int numNotesInSection;
    public int numSpecialAttacks;
    public int numNormalAttacks;

    public PowerAttack specialAttackUI;
    public BossHealth bossHealthBarUI;
    
    //public GameObject okText;
    //public GameObject goodText;
    //public GameObject perfectText;

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

        normalAttackMaxPower = maxBossHealth * (1 - specialAttackTotalDamage) / numNormalAttacks;

        specialAttackPower = 0;

        specialAttackUI.Setup(specialAttackMaxPower);
        bossHealthBarUI.Setup(maxBossHealth);

        numNotesInSection = CalcNumNotesInSection(0);
    }

    private int CalcNumNotesInSection(int index) {

        
        int counter = 0;
        for (int i = index + 2; i < ScenePrototypeManager.Instance.notesInSeconds.Count; i++) {

            if (ScenePrototypeManager.Instance.notesInSeconds[i].playerShouldPlay) {
                //Debug.Log(ScenePrototypeManager.Instance.notesInSeconds[i].notePosInSeconds);
                counter++;
            }

            if (ScenePrototypeManager.Instance.notesInSeconds[i].specialAttack && counter != 0) {
                lastSpecialAttackIndex = i;
                //Debug.Log("num attack in section" + counter);
                //Debug.Log("");
                return counter;
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
        //Debug.Log(counter);
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
<<<<<<< HEAD
                //Debug.Log("ok");
                GiveFeedback(1f);
                accuracy = okAccuracy;
                specialAttackPower += specialAttackMaxPower * okAccuracy / numNotesInSection;
            }else if (diff <= deltaAccuracy / 2 && diff > deltaAccuracy / 6) {
                //Debug.Log("good");
                GiveFeedback(2f);
                accuracy = goodAccuracy;
=======
                Debug.Log("ok");
                specialAttackPower += specialAttackMaxPower * okAccuracy / numNotesInSection;
                currentAccuracy = 1f; //okAccuracy;
                changeText.UpdateText(1f);
            }
            else if (diff <= deltaAccuracy / 2 && diff > deltaAccuracy / 6) {
                Debug.Log("good");
>>>>>>> Alejandra
                specialAttackPower += specialAttackMaxPower * goodAccuracy / numNotesInSection;
                currentAccuracy = 2f; // goodAccuracy;
                changeText.UpdateText(2f);

            } else if (diff <= deltaAccuracy / 6) {
<<<<<<< HEAD
                //Debug.Log("perfect");
                GiveFeedback(3f);
                accuracy = perfectAccuracy;
=======
                Debug.Log("perfect");
>>>>>>> Alejandra
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
        specialAttackPower = 0;
        specialAttackUI.UpdateBar(specialAttackPower);
    }

    public void UpdateBossHealth(float accuracy) {

        float damage = normalAttackMaxPower * accuracy;
        Debug.Log(damage);
        currentBossHealth -= damage;
        bossHealthBarUI.UpdateBar(currentBossHealth);
    }

    public float getAccuracy()
    {
        return currentAccuracy;
    }
    
}


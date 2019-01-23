using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialProgress : MonoBehaviour {

    public TutorialText tutorialText;
    public UnityEvent tileAttack;
    public UnityEvent clearTiles;
    public UnityEvent fadeTile;

    public bool displayTutorialTexts = true;
    public bool tileTutorial = false;
    public bool projectileTutorial = false;
    public bool specialAttackTutorial = false;

    public float timeToCheck = 1000;
    public bool checkTile = false;
    
    public UnityEvent changeCamera;
    public UnityEvent projectile;

    public UnityEvent special;

    public int numAtkCleared = 0;

    private bool check = false;

    private void Start() {

        tutorialText.DisplayText();
    }

    private void Update() {

        if (Input.GetButtonDown("SpecialAttack") && displayTutorialTexts) {

            if(tutorialText.textIndex < 9) {
                tutorialText.DisplayText();
            }
            
            if(tutorialText.textIndex == 5) {

                tutorialText.gameObject.SetActive(false);
                displayTutorialTexts = false;
                tileTutorial = true;
                TileTutorial();
            }

            if (tutorialText.textIndex == 8) {

                tutorialText.gameObject.SetActive(false);
                displayTutorialTexts = false;
                projectileTutorial = true;
                ScenePrototypeManager.Instance.notesInSeconds[(int)ScenePrototypeManager.Instance.noteToPlayInSeconds + 2].noteFunction = changeCamera;
                ProjectileTutorial();
            }

            if(tutorialText.textIndex == 9) {

                tutorialText.gameObject.SetActive(false);
                displayTutorialTexts = false;
                specialAttackTutorial = true;              
                SpecialAttackTutorial();
            }
        }
    }

    private void TileTutorial() {

        int currentTime = (int)ScenePrototypeManager.Instance.noteToPlayInSeconds;

        //ScenePrototypeManager.Instance.notesInSeconds[currentTime + 2] = new ScenePrototypeManager.Note(currentTime + 2);        
        ScenePrototypeManager.Instance.notesInSeconds[currentTime + 2].noteFunction = tileAttack;

        ScenePrototypeManager.Instance.notesInSeconds[currentTime + 4].playerShouldPlay = true;
        ScenePrototypeManager.Instance.notesInSeconds[currentTime + 5].noteFunction = clearTiles;
        ScoreManager.Instance.noteToHit = currentTime + 4;
        Debug.Assert(ScoreManager.Instance.noteToHit == currentTime + 4);
        timeToCheck = currentTime + 5;
        check = false;
    }

    private void ProjectileTutorial() {

        int currentTime = (int)ScenePrototypeManager.Instance.noteToPlayInSeconds;

        ScenePrototypeManager.Instance.notesInSeconds[currentTime + 1].noteFunction = projectile;
        ScenePrototypeManager.Instance.notesInSeconds[currentTime + 3].playerShouldPlay = true;
        ScoreManager.Instance.noteToHit = currentTime + 3;
        timeToCheck = currentTime + 4;
        check = false;
    }

    private void SpecialAttackTutorial() {

        int currentTime = (int)ScenePrototypeManager.Instance.noteToPlayInSeconds;

        ScoreManager.Instance.specialAttackPower = ScoreManager.Instance.specialAttackMaxPower;
        ScenePrototypeManager.Instance.notesInSeconds[currentTime + 1].noteFunction = special;
        ScenePrototypeManager.Instance.notesInSeconds[currentTime + 1].specialAttack = true;
        ScenePrototypeManager.Instance.notesInSeconds[currentTime + 3].playerShouldPlay = true;

        timeToCheck = currentTime + 4;
        check = false;
    }

    private void FixedUpdate() {

        if (ScenePrototypeManager.Instance.playing) {
            if (SongManager.Instance.SongPositionInSeconds >= timeToCheck && !check) {

                check = true;

                if (ScoreManager.Instance.accuracy < 1) {

                    if (tileTutorial) {
                        TileTutorial();
                    }

                    if (projectileTutorial) {
                        ProjectileTutorial();
                    }

                    if (specialAttackTutorial) {
                        SpecialAttackTutorial();
                    }

                } else {

                    if (specialAttackTutorial) {
                        specialAttackTutorial = false;
                        ScenePrototypeManager.Instance.playing = false;
                    } else {
                        tileTutorial = false;
                        projectileTutorial = false;
                        displayTutorialTexts = true;
                        tutorialText.gameObject.SetActive(true);
                        tutorialText.DisplayText();
                    }                    
                }
            }
        }
    }
}
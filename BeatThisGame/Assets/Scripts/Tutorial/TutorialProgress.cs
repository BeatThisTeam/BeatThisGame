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
            tutorialText.DisplayText();

            if(tutorialText.textIndex == 5) {

                tutorialText.gameObject.SetActive(false);
                displayTutorialTexts = false;
                tileTutorial = true;
                TileTutorial();
            }
        }
    }

    private void TileTutorial() {

        int currentTime = (int)ScenePrototypeManager.Instance.noteToPlayInSeconds;

        //ScenePrototypeManager.Instance.notesInSeconds[currentTime + 2] = new ScenePrototypeManager.Note(currentTime + 2);
        ScenePrototypeManager.Instance.notesInSeconds[currentTime + 1].noteFunction = clearTiles;
        ScenePrototypeManager.Instance.notesInSeconds[currentTime + 2].noteFunction = tileAttack;

        ScenePrototypeManager.Instance.notesInSeconds[currentTime + 4].playerShouldPlay = true;
        ScoreManager.Instance.noteToHit = currentTime + 4;
        Debug.Assert(ScoreManager.Instance.noteToHit == currentTime + 4);
        timeToCheck = currentTime + 5;
        check = false;
        
    }

    private void FixedUpdate() {

        if(SongManager.Instance.SongPositionInSeconds >= timeToCheck && !check) {

            check = true;

            if(ScoreManager.Instance.accuracy < 1) {

                if (tileTutorial) {
                    TileTutorial();
                }               

            } else {
                tileTutorial = false;
                displayTutorialTexts = true;
                tutorialText.gameObject.SetActive(true);
                tutorialText.DisplayText();
            }
        }
    }
}
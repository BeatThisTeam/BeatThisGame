using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScorePanel : MonoBehaviour {

    public GameObject background;
    public GameObject roundBackground;
    public GameObject stageClearedPanel;
    public GameObject stageFailedPanel;
    public GameObject deathPanel;

    public TextMeshProUGUI accuracyTxt;
    public TextMeshProUGUI gradeTxt;

    public Buttons activePanel;
    private bool axisInUse = false;
    private float vertAxisInput;

    private void Update() {       

        if (activePanel != null) {

            vertAxisInput = Input.GetAxisRaw("Vertical");

            if (vertAxisInput != 0 && !axisInUse) {

                axisInUse = true;

                if (vertAxisInput > 0) {                    
                    activePanel.Up();
                }else if(vertAxisInput < 0) {
                    activePanel.Down();
                }
            }else if(vertAxisInput == 0) {
                axisInUse = false;
            }

            if (Input.GetButtonDown("Submit")) {
                Debug.Log("submint");
                activePanel.ActivateButton();
            }
        }
    }

    public void DisplayScore() {

        background.SetActive(true);
        roundBackground.SetActive(true);

        if (ScoreManager.Instance.stageCleared) {
            stageClearedPanel.SetActive(true);
            activePanel = stageClearedPanel.GetComponent<Buttons>();
            if(accuracyTxt) {
                accuracyTxt.text = ScoreManager.Instance.avgAccuracy.ToString("F2");
            }
            if (gradeTxt) {
                gradeTxt.text = ScoreManager.Instance.grade;
            }
            
        } else {
            stageFailedPanel.SetActive(true);
            activePanel = stageFailedPanel.GetComponent<Buttons>();
            accuracyTxt.text = "--";
            gradeTxt.text = "--";
        }
    }

    public void DisplayDeathScore() {

        background.SetActive(true);
        roundBackground.SetActive(true);
        deathPanel.SetActive(true);
        activePanel = deathPanel.GetComponent<Buttons>();
    }

    public void NextButton() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RetryButton() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void MenuButton() {
        SceneManager.LoadScene(0);
    }
}

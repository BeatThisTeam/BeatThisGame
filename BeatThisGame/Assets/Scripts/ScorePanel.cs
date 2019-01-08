using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScorePanel : MonoBehaviour {

    public GameObject stageClearedPanel;
    public GameObject stageFailedPanel;
    public GameObject deathPanel;

    public TextMeshProUGUI accuracyTxt;
    public TextMeshProUGUI gradeTxt;

    public void DisplayScore() {

        if (ScoreManager.Instance.stageCleared) {
            stageClearedPanel.SetActive(true);
            if(accuracyTxt) {
                accuracyTxt.text = ScoreManager.Instance.avgAccuracy.ToString("F2");
            }
            if (gradeTxt) {
                gradeTxt.text = ScoreManager.Instance.grade;
            }
            
        } else {
            stageFailedPanel.SetActive(true);
            accuracyTxt.text = "--";
            gradeTxt.text = "--";
        }
    }

    public void DisplayDeathScore() {

        deathPanel.SetActive(true);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

    public GameObject[] Button;
    public GameObject[] boss;
    public GameObject[] bossButtons;

    public GameObject PlayButton;
    public GameObject LevelButton;
    public GameObject creditsButton;
    public GameObject QuitButton;
    public GameObject title;
    public GameObject backButton;
    public GameObject Tutorial;
    public GameObject LV1blob;
    public GameObject LV2parents;
    public GameObject credits;
    public FollowCamUI Cam;

    private bool[] active;
    private bool[] activeLS;

    public int[] index = new int[2];

    int ring = 0;

    private void Awake()
    {
        Button = new GameObject[4];
        Button[0] = PlayButton;
        Button[1] = LevelButton;
        Button[2] = creditsButton;
        Button[3] = QuitButton;

        //    //active = new bool[4];
        //    //active[0] = false;
        //    //active[1] = false;
        //    //active[2] = false;
        //    //active[3] = false;

        //    //activeLS = new bool[4];
        //    //activeLS[0] = false;
        //    //activeLS[1] = false;
        //    //activeLS[2] = false;
        //    //activeLS[3] = false;

        boss = new GameObject[3];
        boss[0] = Tutorial;
        boss[1] = LV1blob;
        boss[2] = LV2parents;

        
    }

    public void ChangeRing() {

        ring = (ring + 1) % 2;
    }

    public void changeIndex(int dir) {

        index[0] = (index[0] + Button.Length + dir) % Button.Length;
        index[1] = (index[1] + boss.Length + dir) % boss.Length;
    }

    void Update () {
        int FaceIndex = index[ring];
        Debug.Log("FaceIndex = " + FaceIndex + " Ring n.= " + ring);

        //activeLS[FaceIndex] = Boss[FaceIndex].activeInHierarchy; 

        if (ring == 0 && Button[FaceIndex].activeInHierarchy == false)
        {
            Debug.Log("FaceIndex = " + FaceIndex);

            StartCoroutine(ChangeButton(FaceIndex));
        }

        if (ring == 1 && boss[FaceIndex].activeInHierarchy == false)
        {
            Debug.Log("FaceIndex = " + FaceIndex);

            StartCoroutine(ChangeLevel(FaceIndex));
        }

        if (Input.GetButtonDown("Submit")) {

            if (PlayButton.activeInHierarchy) {
                SceneManager.LoadScene(1);
            }

            if (creditsButton.activeInHierarchy) {
                credits.SetActive(true);
            }

            if (QuitButton.activeInHierarchy) {
                Application.Quit();
            }

            if (LevelButton.activeInHierarchy) {
                index[0] = 0;
                index[1] = 0;
                Cam.SwitchRing(0.01f);
                title.SetActive(false);
                backButton.SetActive(true);
            }

            //if (Tutorial.activeInHierarchy) {
            //    SceneManager.LoadScene(1);
            //}

            //if (LV1blob.activeInHierarchy) {
            //    SceneManager.LoadScene(2);
            //}
            
            //if (LV2parents.activeInHierarchy) {
            //    SceneManager.LoadScene(3);
            //}            
        }

        if ((Input.GetButtonDown("Shield") || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)) && ring == 1) {
            BackToMainMenu();
            index[0] = 0;
            index[1] = 0;
        }

        if ((Input.GetButtonDown("Shield") || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)) && credits.activeInHierarchy) {
            credits.SetActive(false);
        }
    }

    public void BackToMainMenu() {
        Cam.SwitchRing(0.01f);
        title.SetActive(true);
        Tutorial.SetActive(false);
        LV1blob.SetActive(false);
        LV2parents.SetActive(false);
        backButton.SetActive(false);
    }

    public IEnumerator ChangeButton(int FaceIndex)
    {
        if (Button[((FaceIndex - 1) + 4) % 4].activeInHierarchy == true){
            Button[((FaceIndex - 1) + 4) % 4].SetActive(false);
        }

        if (Button[((FaceIndex + 1) + 4) % 4].activeInHierarchy == true){
            Button[((FaceIndex + 1) + 4) % 4].SetActive(false);
        }
        
        
        Button[FaceIndex].SetActive(true);
        
        yield return null;
    }

    public IEnumerator ChangeLevel(int FaceIndex){

        if (boss[((FaceIndex - 1) + 3) % 3].activeInHierarchy == true){
            boss[((FaceIndex - 1) + 3) % 3].SetActive(false);
            bossButtons[((FaceIndex - 1) + 3) % 3].SetActive(false);
        }

        if (boss[((FaceIndex + 1) + 3) % 3].activeInHierarchy == true){
            boss[((FaceIndex + 1) + 3) % 3].SetActive(false);
            bossButtons[((FaceIndex + 1) + 3) % 3].SetActive(false);
        }

        boss[FaceIndex].SetActive(true);
        bossButtons[FaceIndex].SetActive(true);
        boss[FaceIndex].transform.LookAt(bossButtons[FaceIndex].transform);
        //boss[FaceIndex].transform.eulerAngles(boss[FaceIndex].transform.eulerAngles, boss[FaceIndex].transform.eulerAngles, boss[FaceIndex].transform.eulerAngles)
        yield return null;
    }

}

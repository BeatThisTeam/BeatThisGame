using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

    public GameObject[] Button;
    public GameObject PlayButton;
    public GameObject LevelButton;
    public GameObject OptionsButton;
    public GameObject QuitButton;

    public GameObject[] Boss;
    public GameObject Tutorial;
    public GameObject LV1blob;
    public GameObject LV2parents;
    public GameObject LV3boss;

    public FollowCamUI Cam;

    private bool[] active;
    private bool[] activeLS;

    private GameObject[] boss;

    //int i;
    //int n;

    private void Awake()
    {
        Button = new GameObject[4];
        Button[0] = PlayButton;
        Button[1] = LevelButton;
        Button[2] = OptionsButton;
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

        Boss = new GameObject[4];
        Boss[0] = Tutorial;
        Boss[1] = LV1blob;
        Boss[2] = LV2parents;
        Boss[3] = LV3boss;


    }

    void Update () {
        int FaceIndex = Cam.FaceIndex;
        int Ring = Cam.i;
        Debug.Log("FaceIndex = " + FaceIndex + " Ring n.= " + Ring);

        //activeLS[FaceIndex] = Boss[FaceIndex].activeInHierarchy; 

        if (Ring == 0 && Button[FaceIndex].activeInHierarchy == false)
        {
            Debug.Log("FaceIndex = " + FaceIndex);

            StartCoroutine(ChangeButton(FaceIndex));
        }

        if (Ring == 1 && Boss[FaceIndex].activeInHierarchy == false)
        {
            Debug.Log("FaceIndex = " + FaceIndex);

            StartCoroutine(ChangeLevel(FaceIndex));
        }

        if (Input.GetButtonDown("Submit")) {

            if (PlayButton.activeInHierarchy) {
                SceneManager.LoadScene(1);
            }

            if (QuitButton.activeInHierarchy) {
                Application.Quit();
            }

            if (LevelButton.activeInHierarchy) {
                Cam.SwitchRing(0.01f);
            }

            if (Tutorial.activeInHierarchy) {
                SceneManager.LoadScene(1);
            }

            if (LV1blob.activeInHierarchy) {
                SceneManager.LoadScene(2);
            }
            
            if (LV2parents.activeInHierarchy) {
                SceneManager.LoadScene(3);
            }            
        }

        if (Input.GetButtonDown("Shield") && Ring == 1) {
            Cam.SwitchRing(0.01f);
            Tutorial.SetActive(false);
            LV1blob.SetActive(false);
            LV2parents.SetActive(false);
            LV3boss.SetActive(false);
        }
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

        if (Boss[((FaceIndex - 1) + 4) % 4].activeInHierarchy == true){
            Boss[((FaceIndex - 1) + 4) % 4].SetActive(false);
        }

        if (Boss[((FaceIndex + 1) + 4) % 4].activeInHierarchy == true){
            Boss[((FaceIndex + 1) + 4) % 4].SetActive(false);
        }

        Boss[FaceIndex].SetActive(true);

        yield return null;
    }

}

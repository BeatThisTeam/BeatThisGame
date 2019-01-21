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

    //private void Start()
    //{
    //    StartCoroutine(ChangeButton(0));
    //}

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

    }

    public IEnumerator ChangeButton(int FaceIndex)
    {
        if (Button[((FaceIndex - 1) + 4) % 4].activeInHierarchy == true)
        {
            //Destroy(bt[((FaceIndex - 1) + 4) % 4]);
            Button[((FaceIndex - 1) + 4) % 4].SetActive(false);
            //active[((FaceIndex - 1) + 4) % 4] = false;
        }

        if (Button[((FaceIndex + 1) + 4) % 4].activeInHierarchy == true)
        {
            //Destroy(bt[((FaceIndex + 1) + 4) % 4]);
            Button[((FaceIndex + 1) + 4) % 4].SetActive(false);
           // active[((FaceIndex + 1) + 4) % 4] = false;
        }

        //bt[FaceIndex] = Instantiate(Button[FaceIndex]);

        Button[FaceIndex].SetActive(true);

        //active[FaceIndex] = true;

        yield return null;
    }

    public IEnumerator ChangeLevel(int FaceIndex)
    {
        
        //if (activeLS[((FaceIndex - 1) + 4) % 4] == true)
        //{
        //    //Destroy(boss[((FaceIndex - 1) + 4) % 4]);
        //    Boss[((FaceIndex - 1) + 4) % 4].SetActive(false);
        //    //active[((FaceIndex - 1) + 4) % 4] = false;
        //}

        //if (active[((FaceIndex + 1) + 4) % 4] == true)
        //{
        //    //Destroy(boss[((FaceIndex + 1) + 4) % 4]);
        //    Boss[((FaceIndex + 1) + 4) % 4].SetActive(false);
        //    active[((FaceIndex + 1) + 4) % 4] = false;
        //}

        ////boss[FaceIndex] = Instantiate(Boss[FaceIndex]);

        //Boss[FaceIndex].SetActive(true);

        //active[FaceIndex] = true;

        if (Boss[((FaceIndex - 1) + 4) % 4].activeInHierarchy == true)
        {
            Boss[((FaceIndex - 1) + 4) % 4].SetActive(false);
        }

        if (Boss[((FaceIndex + 1) + 4) % 4].activeInHierarchy == true)
        {
            Boss[((FaceIndex + 1) + 4) % 4].SetActive(false);
        }

        Boss[FaceIndex].SetActive(true);

        yield return null;
    }

}

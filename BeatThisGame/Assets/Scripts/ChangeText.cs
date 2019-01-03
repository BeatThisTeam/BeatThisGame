using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour {

    [SerializeField]
    private Text feedbackText = null;


    string noText = " ";
    string okText = "OK!";
    string goodText = "GOOD!";
    string perfectText = "PERFECT!";
    string text;

    void Start ()
    {
        feedbackText.text = noText;
	}

    public void Update()
    {
        feedbackText.text = text;
    }

    public void UpdateText(float acc)
    {
        
        if (acc == 1f)
        {
            text = okText;
            StartCoroutine("WaitForSec");
        }
        else if (acc == 2f)
        {
            text = goodText;
            StartCoroutine("WaitForSec");
        }
        else if (acc == 3f)
        {
            text = perfectText;
            StartCoroutine("WaitForSec");
        }
    } 

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(0.6f);
        text = noText;
    }
}
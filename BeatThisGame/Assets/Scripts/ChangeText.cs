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

    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
    IEnumerator WaitForSec()
    {
        StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Text>()));
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Text>()));
        text = noText;
    }

}
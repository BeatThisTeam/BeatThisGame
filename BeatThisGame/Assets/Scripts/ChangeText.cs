using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{

    [SerializeField]
    public Text FineText;
    [SerializeField]
    public Text OkText;
    [SerializeField]
    public Text GoodText;
    [SerializeField]
    public Text PerfectText;

    string noText = " ";
    string fineText = "FINE!";
    string okText = "OK!";
    string goodText = "GOOD!";
    string perfectText = "PERFECT!";

    void Start()
    {
        FineText.text = noText;
        OkText.text = noText;
        GoodText.text = noText;
        PerfectText.text = noText;
    }

    public void UpdateText(float acc)
    {

        if (acc == 1f)
        {
            OkText.text = okText;
            //StartCoroutine(FadeTextToFullAlpha(1f, OkText.GetComponent<Text>()));
            StartCoroutine(FadeTextToZeroAlpha(1f, OkText.GetComponent<Text>()));
            Debug.Log("ok fade out");
        }
        else if (acc == 2f)
        {
            GoodText.text = goodText;
            //StartCoroutine(FadeTextToFullAlpha(1f, GoodText.GetComponent<Text>()));
            StartCoroutine(FadeTextToZeroAlpha(1f, GoodText.GetComponent<Text>()));
            Debug.Log("good fade out");
        }
        else if (acc == 3f)
        {
            PerfectText.text = perfectText;
            //StartCoroutine(FadeTextToFullAlpha(1f, PerfectText.GetComponent<Text>()));
            StartCoroutine(FadeTextToZeroAlpha(1f, PerfectText.GetComponent<Text>()));
            Debug.Log("perfect fade out");
        }
        else
        {
            FineText.text = fineText;
            //StartCoroutine(FadeTextToFullAlpha(1f, FineText.GetComponent<Text>()));
            StartCoroutine(FadeTextToZeroAlpha(1f, FineText.GetComponent<Text>()));
            Debug.Log("fine fade out");
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
    

}
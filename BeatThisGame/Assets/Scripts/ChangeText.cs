using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    
    [SerializeField]
    public GameObject okText;
    [SerializeField]
    public GameObject goodText;
    [SerializeField]
    public GameObject perfectText;

    void Start()
    {
        //OkText.SetActive(false);
        //GoodText.SetActive(false);
        //PerfectText.SetActive(false);
    }

    public void UpdateText(float acc)
    {

        if (acc == 1f)
        {
            //OkText.SetActive(true);
            GameObject txt = Instantiate(okText);
            txt.transform.position = transform.position;
            txt.transform.parent = transform.parent;
            txt.GetComponent<Text>().GetComponent<Animator>().SetTrigger("TextAnimation");
            Debug.Log("ok fade out");
        }
        else if (acc == 2f)
        {
            //GoodText.SetActive(true);
            GameObject txt = Instantiate(goodText);
            txt.transform.position = transform.position;
            txt.transform.parent = transform.parent;            
            txt.GetComponent<Text>().GetComponent<Animator>().SetTrigger("TextAnimation");
            Debug.Log("good fade out");
        }
        else if (acc == 3f)
        {
            //PerfectText.SetActive(true);
            GameObject txt = Instantiate(perfectText);
            txt.transform.position = transform.position;
            txt.transform.parent = transform.parent;
            txt.GetComponent<Text>().GetComponent<Animator>().SetTrigger("TextAnimation");
            Debug.Log("perfect fade out");
        }
    }



    //public IEnumerator FadeTextToFullAlpha(float t, Text i)
    //{
    //    i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
    //    while (i.color.a < 1.0f)
    //    {
    //        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
    //        yield return null;
    //    }
    //}

    //public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    //{
    //    i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
    //    i.GetComponent<Animator>().SetTrigger("TextAnimation");
    //    while (i.color.a > 0.0f)
    //    {
    //        i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));           
    //        yield return null;
    //    }
    //}
    

}
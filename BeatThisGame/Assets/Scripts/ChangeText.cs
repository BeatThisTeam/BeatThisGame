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
    [SerializeField]
    public GameObject missText;

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
        }
        else if (acc == 2f)
        {
            //GoodText.SetActive(true);
            GameObject txt = Instantiate(goodText);
            txt.transform.position = transform.position;
            txt.transform.parent = transform.parent;            
            txt.GetComponent<Text>().GetComponent<Animator>().SetTrigger("TextAnimation");
        }
        else if (acc == 3f)
        {
            //PerfectText.SetActive(true);
            GameObject txt = Instantiate(perfectText);
            txt.transform.position = transform.position;
            txt.transform.parent = transform.parent;
            txt.GetComponent<Text>().GetComponent<Animator>().SetTrigger("TextAnimation");
        } else if (acc == 4f) {
            //PerfectText.SetActive(true);
            GameObject txt = Instantiate(missText);
            txt.transform.position = transform.position;
            txt.transform.parent = transform.parent;
            txt.GetComponent<Text>().GetComponent<Animator>().SetTrigger("TextAnimation");
        }
    }
}
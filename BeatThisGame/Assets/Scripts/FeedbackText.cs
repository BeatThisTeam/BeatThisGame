using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackText : MonoBehaviour {

    public Text fineText;
    public Text okText;
    public Text goodText;
    public Text perfectText;

    void Update () {
        Vector3 textPos = Camera.main.WorldToScreenPoint(this.transform.position);
        fineText.transform.position = textPos;
        okText.transform.position = textPos;
        goodText.transform.position = textPos;
        perfectText.transform.position = textPos;
    }
}

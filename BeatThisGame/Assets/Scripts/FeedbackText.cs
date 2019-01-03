using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackText : MonoBehaviour {

    public Text feedbackText;

	void Update () {
        Vector3 textPos = Camera.main.WorldToScreenPoint(this.transform.position);
        feedbackText.transform.position = textPos;
	}
}

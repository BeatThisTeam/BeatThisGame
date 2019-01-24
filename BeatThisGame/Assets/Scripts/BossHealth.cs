using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {

    float minValue = 0;
    float maxValue;
    //variationAmount = 2;
    Slider sliderRef;                                               // True when the boss gets damaged.

    public Color gradeColor;
    private Color gradeDefaultColor;
    public TextMeshProUGUI D;
    public TextMeshProUGUI C;
    public TextMeshProUGUI B;
    public TextMeshProUGUI A;
    public TextMeshProUGUI S;
    public TextMeshProUGUI SS;

    public void Setup(float value) {

        sliderRef = this.gameObject.GetComponent<Slider>();
        sliderRef.minValue = minValue;
        sliderRef.maxValue = maxValue;
        maxValue = value;
        sliderRef.maxValue = maxValue;
        sliderRef.value = maxValue;
        gradeDefaultColor = D.color;
    }

    public void UpdateBar(float value) {

        sliderRef.value = value;

        if(value / maxValue <= 0.5) {
            if(value / maxValue <= 0.4) {
                if(value / maxValue <= 0.3) {
                    if(value / maxValue <= 0.2) {
                        if(value / maxValue <= 0.1) {
                            if (value == 0) {
                                SS.color = gradeColor;
                                S.color = gradeDefaultColor;
                            } else {
                                S.color = gradeColor;
                                A.color = gradeDefaultColor;
                            }
                        } else {
                            A.color = gradeColor;
                            B.color = gradeDefaultColor;
                        }
                    } else {
                        B.color = gradeColor;
                        C.color = gradeDefaultColor;
                    }
                } else {
                    C.color = gradeColor;
                    D.color = gradeDefaultColor;
                }
            } else {
                D.color = gradeColor;
            }
        } 
    }
}

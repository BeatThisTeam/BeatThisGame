using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {

    float minValue = 0;
    float maxValue;
    //variationAmount = 2;
    Slider sliderRef;                                               // True when the boss gets damaged.

    public void Setup(float value) {

        sliderRef = this.gameObject.GetComponent<Slider>();
        sliderRef.minValue = minValue;
        sliderRef.maxValue = maxValue;
        maxValue = value;
        sliderRef.maxValue = maxValue;
        sliderRef.value = maxValue;
    }

    public void UpdateBar(float value) {

        sliderRef.value = value;
    }
}

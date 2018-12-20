using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerAttack : MonoBehaviour 
{
    float minValue = 0f;
    float maxValue;
    float variationAmount = 2;
    Slider sliderRef;                                               // True when the player gets damaged.

    public void Setup(float value) {

        sliderRef = this.gameObject.GetComponent<Slider>();
        sliderRef.minValue = minValue;
        sliderRef.value = minValue;
        maxValue = value;
        sliderRef.maxValue = maxValue;
    }

    public void UpdateBar(float value) {

        Debug.Log(value);
        sliderRef.value = value;
    }
}
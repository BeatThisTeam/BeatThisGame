using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    float minValue = 0;
    float maxValue;
    //variationAmount = 2;
    Slider sliderRef;                                               // True when the player gets damaged.

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

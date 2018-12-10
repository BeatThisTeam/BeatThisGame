using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {

    int minValue = 0, maxValue = 100, variationAmount = 2;
    Slider sliderRef;                                               // True when the boss gets damaged.

    void Start () {
        sliderRef = this.gameObject.GetComponent<Slider>();
        sliderRef.minValue = minValue;
        sliderRef.maxValue = maxValue;
    }
	
	void Update () {
        if (Input.GetKeyDown("a"))
        {

            sliderRef.value -= variationAmount;
        }
        else if (Input.GetKeyDown("d"))
        {
            sliderRef.value += variationAmount;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerAttack : MonoBehaviour 
{
    int minValue = 0, maxValue = 100, variationAmount = 2;
    Slider sliderRef;                                               // True when the player gets damaged.


    void Start()
    {
        sliderRef = this.gameObject.GetComponent<Slider>();
        sliderRef.minValue = minValue;
        sliderRef.maxValue = maxValue;
        sliderRef.value = minValue;
    }


    void Update()
    {
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
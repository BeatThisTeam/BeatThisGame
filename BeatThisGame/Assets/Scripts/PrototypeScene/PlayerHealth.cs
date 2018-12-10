using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    int minValue = 0, maxValue = 10, variationAmount = 2;
    Slider sliderRef;                                               // True when the player gets damaged.


    void Start()
    {
        sliderRef = this.gameObject.GetComponent<Slider>();
        sliderRef.minValue = minValue;
        sliderRef.maxValue = maxValue;
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

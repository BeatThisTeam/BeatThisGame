using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buttons : MonoBehaviour {

    public List<GameObject> buttons;
    public int activeIndex = 0;
    public List<UnityEvent> buttonActions;

    private void Start() {
        
         buttons[activeIndex].transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Up() {

        buttons[activeIndex].transform.GetChild(1).gameObject.SetActive(false);

        activeIndex--;
        if(activeIndex < 0) {
            activeIndex = buttons.Count - 1;
        }

        buttons[activeIndex].transform.GetChild(1).gameObject.SetActive(true);
    }

    public void Down() {

        buttons[activeIndex].transform.GetChild(1).gameObject.SetActive(false);
        activeIndex = (activeIndex + 1) % buttons.Count;
        buttons[activeIndex].transform.GetChild(1).gameObject.SetActive(true);
    }

    public void ActivateButton() {

        buttonActions[activeIndex].Invoke();
    }

    public void MouseOver(int index) {

        buttons[activeIndex].transform.GetChild(1).gameObject.SetActive(false);
        activeIndex = index;
        buttons[activeIndex].transform.GetChild(1).gameObject.SetActive(true);
    }
}

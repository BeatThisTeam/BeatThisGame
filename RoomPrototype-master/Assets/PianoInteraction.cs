using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PianoInteraction : MonoBehaviour {

	private GameObject piano;
	private BoxCollider pianoCollider;
	private bool Interacting;
	private bool enterScenelbl;
	


	// Use this for initialization
	void Start () {
		piano = GameObject.Find("piano");
		pianoCollider = piano.GetComponent<BoxCollider>();
		Interacting = true;
		enterScenelbl = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (enterScenelbl == true && Input.GetMouseButtonDown(0))
		{
			SceneManager.LoadScene("");
			
			
		}
	}
	
	void OnTriggerStay(Collider other) {
		if (other == pianoCollider && Interacting == true)
		{
			if (enterScenelbl == false)
			{
				enterScenelbl = true;
			}
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other == pianoCollider && enterScenelbl == true)
		{
			enterScenelbl = false;
		}
    }
	
	void OnGUI() {
		if (enterScenelbl == true)
		{
			GUI.Label(new Rect(550, 400, 150, 20), "Press LMB to enter level");
		}
		
    }
}

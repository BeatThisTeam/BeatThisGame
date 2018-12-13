using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class WardrobeInteraction : MonoBehaviour {

	private GameObject wardrobe;
	private BoxCollider wardrobeCollider;
	private bool Interacting;
	private bool enterScenelbl;
	


	// Use this for initialization
	void Start () {
		wardrobe = GameObject.Find("wardrobe2");
		wardrobeCollider = wardrobe.GetComponent<BoxCollider>();
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
		if (other == wardrobeCollider && Interacting == true)
		{
			if (enterScenelbl == false)
			{
				enterScenelbl = true;
			}
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other == wardrobeCollider && enterScenelbl == true)
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

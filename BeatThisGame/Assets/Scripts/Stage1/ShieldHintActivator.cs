using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHintActivator : MonoBehaviour
{
	private float currentTime;
	
	// Use this for initialization
	void Start () {
		
		gameObject.SetActive(true);
		//Game Object's Starting transform position has to be set up for (0, -1200, 0)
		//for this to work properly, or slightly higher/lower for different positioning

		
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentTime += Time.deltaTime;
		
		if (currentTime > 45f && currentTime < 45.015f)
		{
			gameObject.transform.position += Vector3.up * 1000f;
		}
		if (currentTime > 48f && currentTime < 48.2f)
		{
			gameObject.transform.position += Vector3.up * 1000f;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHintActivator : MonoBehaviour
{
	private float currentTime;
	
	// Use this for initialization
	void Start () {
		
		gameObject.SetActive(true);
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentTime += Time.deltaTime;
		
		if (currentTime > 3f && currentTime < 3.02f)
		{
			gameObject.transform.position += Vector3.up * 1000f;
		}
		if (currentTime > 6f && currentTime < 6.02f)
		{
			gameObject.transform.position += Vector3.up * 1000f;
		}
	}
}

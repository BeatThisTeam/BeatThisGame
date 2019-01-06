using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
	private float currentTime;

	// Start is called before the first frame update
	void Start()
	{
		gameObject.SetActive(true);

	}

	// Update is called once per frame
	// Runs the method based on which object should be active at a time
	// Timings can be adjusted to match the pace of the game
	void Update()
	{
        
		if (gameObject.name == "MoveHint")
		{
            
			Initialize(3f);

		}
		if (gameObject.name == "RedZoneHint")
		{
       
			Initialize(8f);

		}
		if (gameObject.name == "AttackHint")
		{
            
			Initialize(22f);

		}
		if (gameObject.name == "ShieldHint")
		{
            
			Initialize(45f);

		}
        
	}

	// Method used to make the cue appear and disappear at a right time
	public void Initialize(float GoTime)
	{
		currentTime += Time.deltaTime;
		float AdjustedGoTime = GoTime + 0.015f;
		float KillTime = GoTime + 3f;
		float AdjustedKillTime = KillTime + 0.15f;
		
		if (currentTime > GoTime && currentTime < AdjustedGoTime)
		{
			gameObject.transform.position += Vector3.up * 1000f;
		}
		if (currentTime > KillTime && currentTime < AdjustedKillTime)
		{
			gameObject.transform.position += Vector3.up * 1000f;
		}
	}
    
    
}

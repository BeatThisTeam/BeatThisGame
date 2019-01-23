using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
	private float currentTime;
    private bool initialized = false;
    public Text currentText;

    [TextArea]
    public List<string> texts = new List<string>();

    public int textIndex = 0;

    public void DisplayText() {

        //gameObject.transform.position += Vector3.up * 1000f;
        currentText.text = texts[textIndex];
        if (textIndex < texts.Count) {
            textIndex++;
        }        
        
    }

	// Start is called before the first frame update
	//void Start()
	//{
	//	gameObject.SetActive(true);
	//}

	// Update is called once per frame
	// Runs the method based on which object should be active at a time
	// Timings can be adjusted to match the pace of the game
	//void Update()
	//{
        
	//	if (gameObject.name == "TutorialIntro")
	//	{
            
	//		Initialize(1f, 10f);

	//	}
 //       if (gameObject.name == "MoveHint") {

 //           Initialize(13f, 10f);

 //       }
 //       if (gameObject.name == "RedZoneHint") {

 //           Initialize(25f, 10f);

 //       }
 //       if (gameObject.name == "ShieldHint") {

 //           Initialize(50f, 10f);

 //       }
 //       if (gameObject.name == "AttackHint") {

 //           Initialize(69f, 15f);

 //       }


 //   }

    // Method used to make the cue appear and disappear at a right time
 //   public void Initialize(float GoTime, float duration)
	//{
	//	//currentTime += Time.deltaTime;
	//	float AdjustedGoTime = GoTime + 0.015f;
	//	float KillTime = GoTime + duration;
	//	float AdjustedKillTime = KillTime + 0.15f;
		
	//	if (SongManager.Instance.SongPositionInSeconds > GoTime && !initialized)
	//	{
 //           initialized = true;
	//		gameObject.transform.position += Vector3.up * 1000f;
	//	}
	//	if (SongManager.Instance.SongPositionInSeconds > KillTime && initialized)
	//	{
 //           initialized = false;
	//		gameObject.transform.position += Vector3.up * 1000f;
	//	}
	//}
    
    
}

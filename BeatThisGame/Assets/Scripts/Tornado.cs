using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour {

    public float degrees;
    public AudioVisualization audioPeer;
    public int audioBand;
	
	// Update is called once per frame
	void Update () {

        transform.RotateAround(gameObject.transform.position, Vector3.forward, audioPeer.audioBandBuffer[audioBand] * degrees * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour {

    public float degrees = 20;
	
	// Update is called once per frame
	void Update () {

        transform.RotateAround(gameObject.transform.position, Vector3.forward, degrees * Time.deltaTime);
    }
}

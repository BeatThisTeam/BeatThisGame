using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	public void Expand() {
        transform.localScale = new Vector3(2,2,0);
        Invoke("Shrink", 0.1f);
    }

    public void Shrink() {
        transform.localScale = new Vector3(1, 1, 0);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            
            Expand();
        }
    }
}

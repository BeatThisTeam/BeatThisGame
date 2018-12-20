using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

    //[ColorUsage(true, true)]
    public Color col;
    public SpriteRenderer circle1;
    public SpriteRenderer circle2;

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Projectile")) {
            circle1.color = col;
            circle2.color = col;
        }
    }

}

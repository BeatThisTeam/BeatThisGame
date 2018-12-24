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
            ChangeRingColors();
        }
    }

    public void ChangeRingColors() {
        circle1.color = col;
        circle2.color = col;
        StartCoroutine(ColorTransition(0.5f));
    }

    IEnumerator ColorTransition(float duration) {

        float tLerp = 0;

        while(tLerp <= duration) {

            circle1.color = Color.Lerp(col, Color.white, tLerp / duration);
            circle2.color = Color.Lerp(col, Color.white, tLerp / duration);
            tLerp += Time.deltaTime;
            yield return null;
        }
    }
}

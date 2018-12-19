using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSection : MonoBehaviour {

    public Image blackScreen;
    public Material mat1;
    public Material mat2;
    public GroundColorChanger gcc;

	public void StartFadeIn(float duration) {

        StartCoroutine(FadeInCoroutine(duration));
    }

    public IEnumerator FadeInCoroutine(float duration) {

        float tLerp = 0;
        
        while (tLerp <= duration) {
            var tempColor = blackScreen.color;
            tempColor.a = Mathf.Lerp(1f, 0f, tLerp / duration);
            blackScreen.color = tempColor;
            tLerp += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }

    public void ObscureRing() {

        gcc.ChangeColor(0, mat1);
        //gcc.ChangeColor(1, mat1);
        //gcc.ChangeColor(2, mat1);
    }

    public void LightUpRing(int ringIndex) {
        gcc.ChangeColor(ringIndex, mat2);
    }
}

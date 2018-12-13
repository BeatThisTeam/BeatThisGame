using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingAttack : MonoBehaviour {

    public GroundColorChanger groundControl;
    public GroundSections ground;
    public PlayerController player;
    public float percentage;
    public Material mat;

    private bool firstAtt = true;
    private int ringPos;

    public void StartAttack(float duration) {

        if (firstAtt) {

            ringPos = player.ringIndex + ground.rings.Count;
            Debug.Log(ringPos);
            //int playerSectPosCorrected = player.faceIndex + ground.rings[0].sections.Count;            
            groundControl.ChangeColor((ringPos - 1) % ground.rings.Count, duration);
            groundControl.ChangeColor((ringPos - 2) % ground.rings.Count, duration);

            firstAtt = false;

            ringPos--;
            
        } else {
            Debug.Log(ringPos);
            groundControl.ChangeColor(ringPos % ground.rings.Count, duration);
            groundControl.ChangeColor((ringPos + 1) % ground.rings.Count, duration);
            ringPos--;
            
        }
    }

    public void ClearSections(float duration) {

        for (int i = 0; i < ground.rings.Count; i++) {
            for (int j = 0; j < ground.rings[i].sections.Count; j++) {

                groundControl.ResetGround(i, j, duration);

            }
        }
        firstAtt = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesAttack : MonoBehaviour {

    public GroundColorChanger groundControl;
    public GroundSections ground;
    public CharacterController player;
    public float percentage;
    public Material mat;

    private bool firstAtt = true;

	public void StartAttack(float noteToPlayInSeconds) {
            
        if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= 0) {
            if (firstAtt) {

                int playerRingPosCorrected = player.ringIndex + ground.rings.Count;
                int playerSectPosCorrected = player.faceIndex + ground.rings[0].sections.Count;

                for (int i = 0; i < ground.rings.Count; i++) {
                    for (int j = 0; j < ground.rings[i].sections.Count; j++) {

                        if ((i != player.ringIndex || j != player.faceIndex) && Random.Range(0f, 1f) >= percentage) {
                            groundControl.ChangeColor(i, j);
                            ground.rings[i].sections[j].hurts = true;
                        }
                    }
                }

                firstAtt = false;
                
            } else {
                for(int i = 0; i < ground.rings.Count; i++) {
                    for (int j = 0; j < ground.rings[i].sections.Count; j++) {

                        ground.rings[i].sections[j].hurts = !ground.rings[i].sections[j].hurts;
                        groundControl.ChangeColor(i, j);
                    }
                }
            }
        }
    }

    public void ClearSections() {

        for (int i = 0; i < ground.rings.Count; i++) {
            for (int j = 0; j < ground.rings[i].sections.Count; j++) {
                ground.rings[i].sections[j].hurts = false;
                groundControl.ChangeColor(i, j, mat);
            }
        }
        firstAtt = true;
    }
}

using System.Collections;
using UnityEngine;

public class TilesAttack : MonoBehaviour {

    public GroundColorChanger playerGroundControl;
    public GroundSections playerGround;
    public GroundSections attackRingPrefab;
    public PlayerController player;
    public float percentage;
    public Material defaultMat;
    public Material damageMat;

    public bool[,] hurtingFaces;

    private bool firstAtt = true;

	public void StartAttack(float duration) {

        GroundSections attackRing = Instantiate(attackRingPrefab, Vector3.zero, Quaternion.identity);
        GroundColorChanger attackRingControl = attackRing.GetComponent<GroundColorChanger>();

        if (firstAtt) {

            int numFaces = playerGround.rings[0].sections.Count;
            int numRings = playerGround.rings.Count;
            int playerRingPosCorrected = player.ringIndex + numRings;
            int playerSectPosCorrected = player.faceIndex + numFaces;
            hurtingFaces = new bool[numRings,numFaces];

            for (int i = 0; i < playerGround.rings.Count; i++) {
                for (int j = 0; j < playerGround.rings[i].sections.Count; j++) {
                    if ((j + numFaces + 1)%numFaces == player.faceIndex || (j + numFaces - 1) % numFaces == player.faceIndex) {
                        hurtingFaces[i, j] = true;
                        attackRing.rings[i].sections[j].gameObject.SetActive(true);
                        attackRingControl.ChangeColor(i, j, damageMat);
                        playerGroundControl.ChangeColorDelayed(i, j, damageMat, duration);
                        playerGround.SwitchFaceDelayed(i, j, duration);
                    } else {
                        hurtingFaces[i, j] = false;
                        playerGround.rings[i].sections[j].isTarget = true;
                    }
                    StartCoroutine(AttackCoroutine(duration, attackRing));
                }
            }

            firstAtt = false;
                
        } else {
            for(int i = 0; i < playerGround.rings.Count; i++) {
                for (int j = 0; j < playerGround.rings[i].sections.Count; j++) {

                    hurtingFaces[i, j] = !hurtingFaces[i, j];
                    Debug.Log(hurtingFaces[i, j]);
                    if (hurtingFaces[i, j]) {
                        attackRing.rings[i].sections[j].gameObject.SetActive(true);
                        attackRingControl.ChangeColor(i, j, damageMat);
                        playerGroundControl.ChangeColorDelayed(i, j, damageMat, duration);
                        playerGround.SwitchFaceDelayed(i, j, true, duration);
                    } else {
                        playerGroundControl.ChangeColorDelayed(i, j, defaultMat, duration);
                        attackRing.rings[i].sections[j].gameObject.SetActive(false);
                        playerGround.SwitchFaceDelayed(i, j, false, duration);
                    }
                    
                    StartCoroutine(AttackCoroutine(duration, attackRing));                    
                }
            }
        }
    }

    private IEnumerator AttackCoroutine(float duration, GroundSections attackRing) {

        Transform tr = attackRing.GetComponent<Transform>();
        float tLerp = 0;
        while(tLerp <= duration) {
            tr.localScale = Vector3.Lerp(new Vector3(4,4,4), new Vector3(1.5f,1.5f,1.5f), tLerp / duration);
            tLerp += Time.deltaTime;
            yield return null;
        }

        Destroy(tr.gameObject);
    }

    public void ClearSections(float duration) {

        for (int i = 0; i < playerGround.rings.Count; i++) {
            for (int j = 0; j < playerGround.rings[i].sections.Count; j++) {
                
                playerGroundControl.ResetGround(i, j, duration);
                
            }
        }
        firstAtt = true;
    }
}

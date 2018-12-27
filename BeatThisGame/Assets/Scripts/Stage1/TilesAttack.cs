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
                        hurtingFaces[i, j] = false;
                        playerGround.rings[i].sections[j].isTarget = false;
                    } else {
                        hurtingFaces[i, j] = true;                                              
                        attackRing.rings[i].sections[j].gameObject.SetActive(true);
                        attackRingControl.ChangeColor(i, j, damageMat);
                        playerGroundControl.ChangeColorDelayed(i, j, damageMat, duration);
                        playerGround.SwitchFaceDelayed(i, j, duration);
                    }                    
                }
            }
            StartCoroutine(FindTargetFaces(hurtingFaces, duration - 0.15f));
            StartCoroutine(AttackCoroutine(duration, attackRing));
            firstAtt = false;
                
        } else {
            for(int i = 0; i < playerGround.rings.Count; i++) {
                for (int j = 0; j < playerGround.rings[i].sections.Count; j++) {

                    hurtingFaces[i, j] = !hurtingFaces[i, j];
                    if (hurtingFaces[i, j]) {
                        attackRing.rings[i].sections[j].gameObject.SetActive(true);
                        attackRingControl.ChangeColor(i, j, damageMat);
                        playerGroundControl.ChangeColorDelayed(i, j, damageMat, duration);                        
                        playerGround.SwitchFaceDelayed(i, j, true, duration);
                    } else {
                        attackRing.rings[i].sections[j].gameObject.SetActive(false);
                        playerGround.SwitchFaceDelayed(i, j, false, duration);
                    }                                        
                }
            }
            StartCoroutine(FindTargetFaces(hurtingFaces, duration - 0.15f));
            StartCoroutine(AttackCoroutine(duration, attackRing));
        }
    }

    public void FadeTiles(float duration) {

        for (int i = 0; i < playerGround.rings.Count; i++) {
            for (int j = 0; j < playerGround.rings[i].sections.Count; j++) {
                if (playerGround.rings[i].sections[j].hurts) {
                    playerGroundControl.ChangeColor(i, j, false, duration);
                }
            }
        }
    }

    public void FadeTiles(float duration, int ringIndex, int faceIndex) {

         playerGroundControl.ChangeColor(ringIndex, faceIndex, false, duration);
                
    }

    public void AttackOnFace(float duration) {

        AttackOnFace(duration, player.faceIndex);
    }

    public void AttackOnFace(float duration, int face) {

        GroundSections attackRing = Instantiate(attackRingPrefab, Vector3.zero, Quaternion.identity);
        GroundColorChanger attackRingControl = attackRing.GetComponent<GroundColorChanger>();
        int numFaces = playerGround.rings[0].sections.Count;
        int numRings = playerGround.rings.Count;
        int playerRingPosCorrected = player.ringIndex + numRings;
        int playerSectPosCorrected = player.faceIndex + numFaces;
        hurtingFaces = new bool[numRings, numFaces];

        hurtingFaces[player.ringIndex, face] = true;
        attackRing.rings[player.ringIndex].sections[face].gameObject.SetActive(true);
        attackRingControl.ChangeColor(player.ringIndex, face, damageMat);
        playerGroundControl.ChangeColorDelayed(player.ringIndex, face, damageMat, duration);
        playerGround.SwitchFaceDelayed(player.ringIndex, face, duration);
        StartCoroutine(AttackCoroutine(duration, attackRing));
    }

    private IEnumerator AttackCoroutine(float duration, GroundSections attackRing) {

        Transform tr = attackRing.GetComponent<Transform>();
        float tLerp = 0;
        while(tLerp <= duration) {
            tr.localScale = Vector3.Lerp(new Vector3(4f,4f,4f), new Vector3(1.5f,1.5f,1.5f), tLerp / duration);
            tLerp += Time.deltaTime;
            yield return null;
        }
        Destroy(tr.gameObject);
    }

    private IEnumerator FindTargetFaces(bool[,] hurtingFaces, float delay) {

        //TODO: also the case of front and back tiles (if needed)
        int numRings = playerGround.rings.Count;
        int numFaces = playerGround.rings[0].sections.Count;

        bool[,] boh = new bool[numRings, numFaces];

        for (int i = 0; i < playerGround.rings.Count; i++) {
            for (int j = 0; j < playerGround.rings[i].sections.Count; j++) {
                boh[i,j] = hurtingFaces[i, j];
            }
        }

        yield return new WaitForSeconds(delay);

        for (int i = 0; i < playerGround.rings.Count; i++) {
            for (int j = 0; j < playerGround.rings[i].sections.Count; j++) {

                //A face is tagged as target if at least on near face is safe
                if(boh[i,j] == true && (boh[i, (j+numFaces+1)%numFaces] == false || boh[i, (j + numFaces - 1) % numFaces] == false)) {
                    playerGround.rings[i].sections[j].isTarget = true;
                } else {
                    playerGround.rings[i].sections[j].isTarget = false;
                }
            }
        }
    }

    public void ClearSections(float duration) {

        for (int i = 0; i < playerGround.rings.Count; i++) {
            for (int j = 0; j < playerGround.rings[i].sections.Count; j++) {

                playerGroundControl.ChangeColor(i, j, false);
                playerGround.rings[i].sections[j].isTarget = false;
                playerGround.rings[i].sections[j].hurts = false;
            }
        }
        firstAtt = true;
    }
}

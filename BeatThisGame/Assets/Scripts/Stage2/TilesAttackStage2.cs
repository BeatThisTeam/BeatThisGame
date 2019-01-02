using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesAttackStage2 : Attack {

    private bool firstAtt = true;
    public GroundSections attackRingPrefab;
    public Material defaultMat;
    public Material damageMat;
    public bool[,] hurtingFaces = new bool [2,9];

    public override void StartAttack(float duration) {

        GroundSections attackRing1 = Instantiate(attackRingPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity);
        GroundSections attackRing2 = Instantiate(attackRingPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity);
        GroundColorChanger attackRingControl1 = attackRing1.GetComponent<GroundColorChanger>();
        GroundColorChanger attackRingControl2 = attackRing2.GetComponent<GroundColorChanger>();

        if (firstAtt) {

            int numFaces = ground.rings[0].sections.Count;
            int numRings = ground.rings.Count;
            Debug.Assert(numRings == 2);

            int playerRingPosCorrected = playerCtrl.ringIndex + numRings;
            int playerSectPosCorrected = playerCtrl.faceIndex + numFaces;

            int ringIndex = playerCtrl.ringIndex;

            for (int i = 0; i < numFaces; i++) {
                if ((i + numFaces + 1) % numFaces == playerCtrl.faceIndex || (i + numFaces - 1) % numFaces == playerCtrl.faceIndex) {

                    ground.rings[ringIndex].sections[i].isTarget = false;
                    hurtingFaces[ringIndex, i] = false;

                    hurtingFaces[(ringIndex + 1) % numRings, i] = true;
                    attackRing2.rings[(ringIndex + 1) % numRings].sections[i].gameObject.SetActive(true);
                    attackRingControl2.ChangeColor((ringIndex + 1) % numRings, i, damageMat);
                    gcc.ChangeColorDelayed((ringIndex + 1) % numRings, i, damageMat, duration);
                    ground.SwitchFaceDelayed((ringIndex + 1) % numRings, i, duration);
                } else {
                    hurtingFaces[ringIndex, i] = true;
                    hurtingFaces[(ringIndex + 1) % numRings, i] = false;

                    attackRing1.rings[ringIndex].sections[i].gameObject.SetActive(true);

                    attackRingControl1.ChangeColor(ringIndex, i, damageMat);
                    gcc.ChangeColorDelayed(ringIndex, i, damageMat, duration);
                    ground.SwitchFaceDelayed(ringIndex, i, duration);
                }
            }

            //StartCoroutine(FindTargetFaces(hurtingFaces, duration - 0.15f));
            if(ringIndex == 0) {
                StartCoroutine(AttackCoroutine(duration, attackRing1, new Vector3(0, 2, 0), new Vector3(2, 2, 2)));
                StartCoroutine(AttackCoroutine(duration, attackRing2, new Vector3(4, 2, 4), new Vector3(2, 2, 2)));
            } else {
                StartCoroutine(AttackCoroutine(duration, attackRing1, new Vector3(4, 2, 4), new Vector3(2, 2, 2)));
                StartCoroutine(AttackCoroutine(duration, attackRing2, new Vector3(0, 2, 0), new Vector3(2, 2, 2)));               
            }
            firstAtt = false;
        }
        //} else {
        //    for (int i = 0; i < playerGround.rings.Count; i++) {
        //        for (int j = 0; j < playerGround.rings[i].sections.Count; j++) {

        //            hurtingFaces[i, j] = !hurtingFaces[i, j];
        //            if (hurtingFaces[i, j]) {
        //                attackRing.rings[i].sections[j].gameObject.SetActive(true);
        //                attackRingControl.ChangeColor(i, j, damageMat);
        //                playerGroundControl.ChangeColorDelayed(i, j, damageMat, duration);
        //                playerGround.SwitchFaceDelayed(i, j, true, duration);
        //            } else {
        //                attackRing.rings[i].sections[j].gameObject.SetActive(false);
        //                playerGround.SwitchFaceDelayed(i, j, false, duration);
        //            }
        //        }
        //    }
        //    StartCoroutine(FindTargetFaces(hurtingFaces, duration - 0.15f));
        //    StartCoroutine(AttackCoroutine(duration, attackRing));
        //}
    }

    private IEnumerator AttackCoroutine(float duration, GroundSections attackRing, Vector3 startScale, Vector3 endScale) {

        Transform tr = attackRing.GetComponent<Transform>();
        float tLerp = 0;

        while (tLerp <= duration) {
            tr.localScale = Vector3.Lerp(startScale, endScale, tLerp / duration);
            tLerp += Time.deltaTime;
            yield return null;
        }
        Destroy(tr.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesAttackStage3 : Attack {

    private bool firstAtt = true;
    public GroundSections attackRingPrefab;
    public Material defaultMat;
    public Material damageMat;
    public bool[,] hurtingFaces = new bool[2, 9];

    private int numRings;
    private int numFaces;
    private int targetModifier = 0;
    private int nAttacks = 0;
    private int initialTargetSection;

    public override void StartAttack(float duration) {

        if (firstAtt) {
            GroundSections attackRing1 = Instantiate(attackRingPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity);
            GroundSections attackRing2 = Instantiate(attackRingPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity);
            GroundColorChanger attackRingControl1 = attackRing1.GetComponent<GroundColorChanger>();
            GroundColorChanger attackRingControl2 = attackRing2.GetComponent<GroundColorChanger>();
            numFaces = ground.rings[0].sections.Count;
            numRings = ground.rings.Count;
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


            if (ringIndex == 0) {
                StartCoroutine(AttackCoroutine(duration, attackRing1, new Vector3(0, 2, 0), new Vector3(2, 2, 2)));
                StartCoroutine(AttackCoroutine(duration, attackRing2, new Vector3(4, 2, 4), new Vector3(2, 2, 2)));
            } else {
                StartCoroutine(AttackCoroutine(duration, attackRing1, new Vector3(4, 2, 4), new Vector3(2, 2, 2)));
                StartCoroutine(AttackCoroutine(duration, attackRing2, new Vector3(0, 2, 0), new Vector3(2, 2, 2)));
            }
            StartCoroutine(FindTargetFaces(hurtingFaces, duration - 0.15f));
            firstAtt = false;
        } else {

            for (int i = 0; i < numRings; i++) {

                GroundSections attackRing = Instantiate(attackRingPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity);
                GroundColorChanger attackRingControl = attackRing.GetComponent<GroundColorChanger>();

                for (int j = 0; j < numFaces; j++) {

                    hurtingFaces[i, j] = !hurtingFaces[i, j];
                    if (hurtingFaces[i, j]) {
                        attackRing.rings[i].sections[j].gameObject.SetActive(true);
                        attackRingControl.ChangeColor(i, j, damageMat);
                        gcc.ChangeColorDelayed(i, j, damageMat, duration);
                        ground.SwitchFaceDelayed(i, j, true, duration);
                    } else {
                        attackRing.rings[i].sections[j].gameObject.SetActive(false);
                        ground.SwitchFaceDelayed(i, j, false, duration);
                    }
                }
                if (i == 0) {
                    StartCoroutine(AttackCoroutine(duration, attackRing, new Vector3(0, 2, 0), new Vector3(2, 2, 2)));
                } else {
                    StartCoroutine(AttackCoroutine(duration, attackRing, new Vector3(4, 2, 4), new Vector3(2, 2, 2)));
                }
            }
            StartCoroutine(FindTargetFaces(hurtingFaces, duration - 0.15f));
        }
    }

    public void AttackOnFace(float duration) {

        numFaces = ground.rings[0].sections.Count;
        numRings = ground.rings.Count;
        targetModifier = 0;
        nAttacks = 0;
        initialTargetSection = playerCtrl.faceIndex;
        AttackOnFace(duration, initialTargetSection);
    }

    public void ContinueAttackOnFace(float duration) {

        nAttacks++;
        targetModifier = -nAttacks;

        for (int i = 0; i < nAttacks + 1; i++) {
            int targetSection = (initialTargetSection + numFaces + targetModifier) % numFaces;
            AttackOnFace(duration, targetSection);
            targetModifier += 2;
        }
    }

    public void AttackOnFace(float duration, int face) {


        int numFaces = ground.rings[0].sections.Count;
        int numRings = ground.rings.Count;
        int playerRingPosCorrected = playerCtrl.ringIndex + numRings;
        int playerSectPosCorrected = playerCtrl.faceIndex + numFaces;
        hurtingFaces = new bool[numRings, numFaces];

        for (int i = 0; i < numRings; i++) {
            GroundSections attackRing = Instantiate(attackRingPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity);
            GroundColorChanger attackRingControl = attackRing.GetComponent<GroundColorChanger>();
            hurtingFaces[i, face] = true;
            attackRing.rings[i].sections[face].gameObject.SetActive(true);
            attackRingControl.ChangeColor(i, face, damageMat);
            gcc.ChangeColorDelayed(i, face, damageMat, duration);
            ground.SwitchFaceDelayed(i, face, duration);
            ground.rings[i].sections[face].isTarget = true;
            if (i == 0) {
                StartCoroutine(AttackCoroutine(duration, attackRing, new Vector3(0, 2, 0), new Vector3(2, 2, 2)));
            } else {
                StartCoroutine(AttackCoroutine(duration, attackRing, new Vector3(4, 2, 4), new Vector3(2, 2, 2)));
            }
        }
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

    private IEnumerator FindTargetFaces(bool[,] hurtingFaces, float delay) {

        //TODO: also the case of front and back tiles (if needed)
        int numRings = ground.rings.Count;
        int numFaces = ground.rings[0].sections.Count;

        bool[,] boh = new bool[numRings, numFaces];

        for (int i = 0; i < ground.rings.Count; i++) {
            for (int j = 0; j < ground.rings[i].sections.Count; j++) {
                boh[i, j] = hurtingFaces[i, j];
            }
        }

        yield return new WaitForSeconds(delay);

        for (int i = 0; i < ground.rings.Count; i++) {
            for (int j = 0; j < ground.rings[i].sections.Count; j++) {

                //A face is tagged as target if at least on near face is safe
                if (boh[i, j] == true && (boh[i, (j + numFaces + 1) % numFaces] == false || boh[i, (j + numFaces - 1) % numFaces] == false)) {
                    ground.rings[i].sections[j].isTarget = true;
                } else {
                    ground.rings[i].sections[j].isTarget = false;
                }
            }
        }
    }

    public void FadeTiles(float duration) {

        for (int i = 0; i < ground.rings.Count; i++) {
            for (int j = 0; j < ground.rings[i].sections.Count; j++) {
                if (ground.rings[i].sections[j].hurts) {
                    gcc.ChangeColor(i, j, false, duration);
                }
            }
        }
    }

    public void ClearSections(float duration) {

        for (int i = 0; i < ground.rings.Count; i++) {
            for (int j = 0; j < ground.rings[i].sections.Count; j++) {

                gcc.ChangeColor(i, j, false);
                ground.rings[i].sections[j].isTarget = false;
                ground.rings[i].sections[j].hurts = false;
            }
        }
        firstAtt = true;
    }
}

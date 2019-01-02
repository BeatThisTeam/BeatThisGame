using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingAttack : Attack {

    private bool firstAtt = true;
    private int ringPos;
    public GroundSections attackRingPrefab;
    public Material defaultMat;
    public Material damageMat;

    private int targetRing;

    public override void StartAttack(float duration) {

        GroundSections attackRing = Instantiate(attackRingPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity);
        GroundColorChanger attackRingControl = attackRing.GetComponent<GroundColorChanger>();
        if (firstAtt) {
            targetRing = playerCtrl.ringIndex;
            for (int i = 0; i < ground.rings[targetRing].sections.Count; i++) {
                attackRing.rings[targetRing].sections[i].gameObject.SetActive(true);
                attackRingControl.ChangeColor(targetRing, i, damageMat);
                gcc.ChangeColorDelayed(targetRing, i, damageMat, duration);
                ground.SwitchFaceDelayed(targetRing, i, duration);
                ground.rings[targetRing].sections[i].isTarget = true;
            }

            StartCoroutine(FindTargetFaces(targetRing, duration - 0.15f));
            StartCoroutine(AttackCoroutine(duration, attackRing, targetRing));
            firstAtt = false;

        } else {

            targetRing = (targetRing + 1) % 2;

            for (int i = 0; i < ground.rings[targetRing].sections.Count; i++) {

                attackRing.rings[targetRing].sections[i].gameObject.SetActive(true);
                attackRingControl.ChangeColor(targetRing, i, damageMat);
                gcc.ChangeColorDelayed(targetRing, i, damageMat, duration);
                ground.SwitchFaceDelayed(targetRing, i, duration);
                ground.rings[targetRing].sections[i].isTarget = true;

                attackRing.rings[(targetRing + 1) % 2].sections[i].gameObject.SetActive(false);
                ground.SwitchFaceDelayed((targetRing + 1) % 2, i, false, duration);                                      
            }

            StartCoroutine(FindTargetFaces(targetRing, duration - 0.15f));
            StartCoroutine(AttackCoroutine(duration, attackRing, targetRing));
        }
    }

    private IEnumerator AttackCoroutine(float duration, GroundSections attackRing, int targetRing) {

        Transform tr = attackRing.GetComponent<Transform>();
        Vector3 finalScale = new Vector3(2, 2, 2);
        Vector3 startScale;
        if(targetRing == 0) {
            startScale = new Vector3(0, 2, 0);
        } else {
            startScale = new Vector3(4, 2, 4);
        }

        float tLerp = 0;

        while (tLerp <= duration) {
            tr.localScale = Vector3.Lerp(startScale, finalScale, tLerp / duration);
            tLerp += Time.deltaTime;
            yield return null;
        }
        Destroy(tr.gameObject);
    }

    private IEnumerator FindTargetFaces(int targetRing, float delay) {

        yield return new WaitForSeconds(delay);

        for (int i = 0; i < ground.rings[targetRing].sections.Count; i++) {

            ground.rings[targetRing].sections[i].isTarget = true;
            ground.rings[(targetRing+1)%2].sections[i].isTarget = false;
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

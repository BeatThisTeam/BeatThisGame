using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GroundSections : MonoBehaviour {

    [System.Serializable]
    public class Ring {

        //public List<Transform> faces;
        public List<Section> sections;
    }

    public List<Ring> rings = new List<Ring>();

    IEnumerator SetHurt(int ringIndex, int faceIndex, float delay) {

        yield return new WaitForSeconds(delay);
        rings[ringIndex].sections[faceIndex].hurts = true;
        Hurts(ringIndex, faceIndex);
    }

    public void ResetHurt(int ringIndex, int faceIndex) {

        rings[ringIndex].sections[faceIndex].hurts = false;
    }

    public void SwitchFace(int ringIndex, int faceIndex) {

        if(rings[ringIndex].sections[faceIndex].hurts == true) {
            ResetHurt(ringIndex, faceIndex);
            rings[ringIndex].sections[faceIndex].isTarget = true;
        } else {
            StartCoroutine(SetHurt(ringIndex, faceIndex, 0.1f));
        }
    }

    public void SwitchFace(int ringIndex, int faceIndex, bool val) {

        if (val == false) {
            ResetHurt(ringIndex, faceIndex);
            rings[ringIndex].sections[faceIndex].isTarget = true;
        } else {
            StartCoroutine(SetHurt(ringIndex, faceIndex, 0.1f));
        }
    }

    public void SwitchFaceDelayed(int ringIndex, int faceIndex, float delay) {

        StartCoroutine(SwitchFaceDelayedCoroutine(ringIndex, faceIndex, delay));
    }

    public void SwitchFaceDelayed(int ringIndex, int faceIndex, bool val, float delay) {

        StartCoroutine(SwitchFaceDelayedCoroutine(ringIndex, faceIndex, delay));
    }

    public IEnumerator SwitchFaceDelayedCoroutine(int ringIndex, int faceIndex, float delay) {

        yield return new WaitForSeconds(delay);
        SwitchFace(ringIndex, faceIndex);
    }

    public IEnumerator SwitchFaceDelayedCoroutine(int ringIndex, int faceIndex, bool val, float delay) {

        yield return new WaitForSeconds(delay);
        SwitchFace(ringIndex, faceIndex, val);
    }

    public void Hurts(int ringIndex, int faceIndex) {

        PlayerController player = ScenePrototypeManager.Instance.player.GetComponent<PlayerController>();

        if(rings[ringIndex].sections[faceIndex].hurts && player.ringIndex == ringIndex && player.faceIndex == faceIndex) {
            player.Damage(10);
        }
    }
}

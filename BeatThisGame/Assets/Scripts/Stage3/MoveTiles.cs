using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTiles : MonoBehaviour {

    public GroundSections ground;
    public float height1;
    public float height2;

    public float[] heights = new float[3];

	void Start () {

        heights[0] = ground.rings[0].sections[0].GetComponent<Transform>().position.y;
	}
	
	void Update () {
        //Test1
        if (Input.GetKeyDown(KeyCode.A)) {
            int[] t = new int[3];
            t[0] = 0;
            t[1] = 2;
            t[2] = 4;
            ChangeTilesHeight(t, 1, 2f);
        }

        //Test2
        if (Input.GetKeyDown(KeyCode.S)) {
            SwapRings(0, 1, 2f);
        }
    }

    public void ChangeTilesHeight(int[] tiles, int heightIndex, float duration) {
        Debug.Log("bao1");
    }

    public void SwapRings(int ringIndex1, int ringIndex2, float duration) {
        Debug.Log("bao2");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTiles : MonoBehaviour {

    public GroundSections ground;
    public float height1;
    public float height2;

    public float[] heights = new float[4];

    public GameObject[] facce = new GameObject[9];
    public GameObject[] anelli = new GameObject[3];


	void Start () {

        heights[0] = ground.rings[0].sections[0].GetComponent<Transform>().position.y;
        heights[1] = height1;
        heights[2] = height2;
        heights[3] = 0;

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
        //Test1.5
        if (Input.GetKeyDown(KeyCode.W))
        {
            int[] t = new int[3];
            t[0] = 3;
            t[1] = 6;
            t[2] = 8;
            ChangeTilesHeight(t, 2, 2f);
        }
        //Test1.8
        if (Input.GetKeyDown(KeyCode.D))
        {
            int[] t = new int[3];
            t[0] = 0;
            t[1] = 2;
            t[2] = 4;
            ChangeTilesHeight(t, 0, 2f);
        }

        //Test2
        if (Input.GetKeyDown(KeyCode.S)) {
            SwapRings(0, 1, 2f);
        }
    }


    public void ChangeTilesHeight(int[] tiles, int heightIndex, float duration) {
        StartCoroutine(HeightCoroutine(tiles, heightIndex, duration));
    }

    public void SwapRings(int ringIndex1, int ringIndex2, float duration)
    {
        StartCoroutine(SwapRingsCoroutine(ringIndex1, ringIndex2, duration));
    }


    IEnumerator HeightCoroutine(int[] tiles, int heightIndex, float duration)
    {
        Debug.Log("bao1");

        float TimeCounter = 0;

        float speed = Mathf.Abs((Mathf.Abs(heights[heightIndex]) - Mathf.Abs(facce[tiles[0]].transform.position.y))) / duration;

        while (TimeCounter <= duration)
        {

            for (int i = 0; i <= 2; i++)
            {
                float StartHeight = facce[tiles[i]].transform.position.y;
                float EndHeight = heights[heightIndex];
                float Smoothed = Mathf.Lerp(StartHeight, EndHeight, speed * Time.deltaTime);

                facce[tiles[i]].transform.position = new Vector3(facce[tiles[i]].transform.position.x, Smoothed, facce[tiles[i]].transform.position.z);
            }

            TimeCounter += Time.deltaTime;

            yield return null;
        }
    }



    IEnumerator SwapRingsCoroutine(int ringIndex1, int ringIndex2, float duration)
    {
        MoveUpDown(ringIndex1, ringIndex2, 1, 2, duration);

        yield return null;

    }


    public void MoveUpDown(int ringIndex1, int ringIndex2, int heightIndex1, int heightIndex2, float duration)
    {
        float TimeCounter = 0;
        float speed1 = Mathf.Abs(heights[1]) / duration;
        float speed2 = Mathf.Abs(heights[2]) / duration;

        while (TimeCounter <= duration)
        {
            float StartHeight1 = anelli[ringIndex1].transform.position.y;
            float EndHeight1 = heights[heightIndex1];
            float Smoothed1 = Mathf.Lerp(StartHeight1, EndHeight1, speed1 * Time.deltaTime);

            anelli[ringIndex1].transform.position = new Vector3(anelli[ringIndex1].transform.position.x, Smoothed1, anelli[ringIndex1].transform.position.z);

            float StartHeight2 = anelli[ringIndex2].transform.position.y;
            float EndHeight2 = heights[heightIndex2];
            float Smoothed2 = Mathf.Lerp(StartHeight2, EndHeight2, speed2 * Time.deltaTime);

            anelli[ringIndex2].transform.position = new Vector3(anelli[ringIndex2].transform.position.x, Smoothed2, anelli[ringIndex2].transform.position.z);

            TimeCounter += Time.deltaTime;

        }

      
        Shrink(ringIndex1, ringIndex2, duration);

    }

    public void Shrink(int ringIndex1, int ringIndex2, float duration)
    {
        float TimeCounter = 0;

        float[] ringDim = new float[3];
        ringDim[0] = 48;
        ringDim[1] = 64;
        ringDim[2] = 80;

        float Coeff1 = ringDim[ringIndex2] / ringDim[ringIndex1];
        float Coeff2 = ringDim[ringIndex1] / ringDim[ringIndex2];

        float speed1 = 3;
        float speed2 = 3;

        while (TimeCounter <= duration)
        {

            Vector3 LocalScale1 = anelli[ringIndex1].transform.localScale;
            Vector3 DesiredScale1 = new Vector3(Coeff1, anelli[ringIndex1].transform.localScale.y, Coeff1);
            Vector3 Smoothed1 = Vector3.Lerp(LocalScale1, DesiredScale1, speed1 * Time.deltaTime);

            anelli[ringIndex1].transform.localScale = Smoothed1;


            Vector3 LocalScale2 = anelli[ringIndex2].transform.localScale;
            Vector3 DesiredScale2 = new Vector3(Coeff2, anelli[ringIndex2].transform.localScale.y, Coeff2);
            Vector3 Smoothed2 = Vector3.Lerp(LocalScale2, DesiredScale2, speed2 * Time.deltaTime);

            anelli[ringIndex2].transform.localScale = Smoothed2;


            TimeCounter += Time.deltaTime;

        }

        MoveUpDown(ringIndex1, ringIndex2, 3, 3, duration);
        
    }
}

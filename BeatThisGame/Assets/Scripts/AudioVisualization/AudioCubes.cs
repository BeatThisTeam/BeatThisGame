using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCubes : MonoBehaviour {

    public AudioVisualization audioVis;
    public GameObject cubePrefab;
    GameObject[] cubes = new GameObject[64];
    public float maxScale;
    public float circleRadius;

    private UpDownCam cam;
    private bool vertical = true;

	// Use this for initialization
	void Start () {
		
        for(int i = 0; i < cubes.Length; i++) {

            GameObject cube = Instantiate(cubePrefab);
            cube.transform.position = this.transform.position;
            cube.transform.parent = this.transform;
            cube.name = "AudioCube" + i;
            this.transform.eulerAngles = new Vector3(0, -5.625f * i, 0);
            cube.transform.position = Vector3.forward * circleRadius + new Vector3(0, transform.position.y, 0);
            cubes[i] = cube;
        }

        cam = GameObject.FindObjectOfType<UpDownCam>();
	}
	
	// Update is called once per frame
	void Update () {      
        for (int i = 0; i < cubes.Length; i++) {
            if (cubes[i] != null && audioVis.audioBandBuffer64[i] > 0) {
                cubes[i].transform.localScale = new Vector3(1, audioVis.audioBandBuffer64[i] * maxScale, 1);
            }
        }      
	}
}

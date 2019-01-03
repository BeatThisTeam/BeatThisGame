using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimation : MonoBehaviour {

    private Animator anim;
    public Material[] materials;
    private float i;

    public GameObject LOD;
    public float transitionSpeed;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        materials = LOD.GetComponent<Renderer>().materials;

    }
	
	// Update is called once per frame
	void Update () {
        if(anim.GetBool("isDead") && i < 1.5)
        {
            //Debug.Log("C is dead");
            foreach (Material material in materials)
            {
                i = Mathf.Lerp(i, 1, Time.deltaTime * transitionSpeed);
                material.SetFloat("Vector1_D4B3BD5A", i);
                material.SetFloat("Vector1_DC68DE65", i);
                Debug.Log("i is " + i);
            }
        }
	}
}

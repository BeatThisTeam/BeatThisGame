using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public float ReflectiveDuration;
    public Renderer rend;

    public GameObject shield;

    private bool active;

    public CharacterController player;
    public Transform boss;

    private Projectile projectile;
    private Attack2 ProjectileAttack;

    public float WaitingTime;

    public Transform ReflectivePoint;

    private void Start()
    {
        shield.SetActive(false);
        active = false;
    }

    void FixedUpdate () {
        /*rend = GetComponent<Renderer>()*/;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ShieldActive(WaitingTime));
            
            
        }
        //else
        //{
        //    shield.SetActive(false);
        //    active = false;
        //}
        
        
	}


    private IEnumerator ShieldActive(float WaitingTime)
    {
        shield.SetActive(true);
        active = true;

        yield return new WaitForSeconds(WaitingTime);

        shield.SetActive(false);
        active = false;

        yield return null;
     }
       
    

    //public void OnTriggerEnter(Collider collider)
    //{
    //    if (active)
    //        if (collider.gameObject.tag == "Projectile")
    //        {
    //            Debug.Log("HIT");
    //            Vector3 StartPos = ReflectivePoint.position;

    //            Vector3 EndPos = boss.position;

    //            projectile.Move(StartPos, EndPos, ReflectiveDuration);

    //        }
    
        
    //}
}

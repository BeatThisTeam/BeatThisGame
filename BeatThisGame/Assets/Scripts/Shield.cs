using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public float ReflectiveDuration;
    public Renderer rend;

    public GameObject shield;

    private bool active;

    public PlayerController player;
    public Transform boss;

    private Projectile projectile;
    private Attack2 ProjectileAttack;

    public float WaitingTime;

    public Transform ReflectivePoint;

    private void Start() {
        shield.SetActive(false);
        active = false;
    }

    public void ActivateShield() {

        StartCoroutine(ShieldActive(WaitingTime));
    }


    private IEnumerator ShieldActive(float WaitingTime){

        shield.SetActive(true);
        active = true;

        yield return new WaitForSeconds(WaitingTime);

        shield.SetActive(false);
        active = false;

        yield return null;
     }
}

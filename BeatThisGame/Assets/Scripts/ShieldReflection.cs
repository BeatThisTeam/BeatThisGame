using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldReflection : MonoBehaviour {

    private Shield shield;
    public Projectile projectile;

    public Transform ReflectivePoint;
    public Transform boss;

    public float ReflectiveDuration;


    private void OnTriggerEnter(Collider collider)
    {
            if (collider.gameObject.tag == "Projectile")
            {
                Destroy(collider.gameObject);
            
                Debug.Log("HIT");

                Vector3 StartPos = ReflectivePoint.position;
                Vector3 EndPos = boss.position;

                Projectile proj;
                proj = Instantiate(projectile, StartPos, Quaternion.identity);
                proj.Move(StartPos, EndPos, ReflectiveDuration);

            }

    }
}

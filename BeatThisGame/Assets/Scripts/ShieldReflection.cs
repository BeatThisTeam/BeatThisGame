using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldReflection : MonoBehaviour {

    private Shield shield;
    public Projectile projectile;

    public Transform ReflectivePoint;
    public Transform boss;

    public float ReflectiveDuration;


    private void OnTriggerEnter(Collider collider) {

        if (collider.gameObject.tag == "Projectile") {

            if (collider.GetComponent<Projectile>().rejectable) {

                Destroy(collider.gameObject);
                Debug.Log("HIT");

                Vector3 StartPos = collider.transform.position;
                Vector3 EndPos = boss.position;

                Projectile proj;
                proj = Instantiate(projectile, StartPos, Quaternion.identity);
                proj.rejectable = false;
                proj.rejected = true;
                proj.rejectAccuracy = ScoreManager.Instance.accuracy;
                proj.Move(StartPos, EndPos, ReflectiveDuration);

            }
        }
    }
}

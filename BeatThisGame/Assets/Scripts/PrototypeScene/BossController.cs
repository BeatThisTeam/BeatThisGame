using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    Transform tr;

    public Projectile projectile;

    private void Awake() {

        tr = GetComponent<Transform>();
    }

    
}

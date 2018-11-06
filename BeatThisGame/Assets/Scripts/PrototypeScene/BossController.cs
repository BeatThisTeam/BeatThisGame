using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    Transform tr;

    public Projectile projectile;

    private void Awake() {

        tr = GetComponent<Transform>();
    }

    public void Attack2(Vector3 startPos, Vector3 endPos, float duration) {
        //Debug.Log(SongManager.Instance.SongPositionInSeconds);
        Projectile pr; 
        pr = Instantiate(projectile, startPos, Quaternion.identity);
        pr.Move(startPos, endPos, duration);
    }
}

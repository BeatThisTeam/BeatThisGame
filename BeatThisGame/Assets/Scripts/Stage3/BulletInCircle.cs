using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInCircle : Attack {
    private Transform[] target;
    public Transform targetDx;
    public Transform targetSx;
    public Projectile projectile;
    private Vector3 spawnpos;
    private Vector3 endpos;
    public float damage;
    private float randomNumber;
    private int i;
    private int PlayerFacePosNew;
    int sliceCount;

    public override void StartAttack(float duration) {
        target = new Transform[2];
        target[0] = targetDx;
        target[1] = targetSx;
        sliceCount = ground.rings[0].sections.Count;
        int PlayerFacePos = playerCtrl.faceIndex;
        int PlayerRingPos = playerCtrl.ringIndex;
        randomNumber = Random.Range(0.0f, 1.0f);
        if (randomNumber < 0.5f){
            i = 0;
            PlayerFacePosNew = ((PlayerFacePos + 4) + sliceCount) % sliceCount;
        }
        if (randomNumber > 0.5f){
            i = 1;
            PlayerFacePosNew = ((PlayerFacePos + 5) + sliceCount) % sliceCount;
        }
        float height = target[i].position.y;
        spawnpos = new Vector3(ground.rings[PlayerRingPos].sections[PlayerFacePosNew].transform.position.x, height, ground.rings[PlayerRingPos].sections[PlayerFacePosNew].transform.position.z);
        Debug.Log(spawnpos);
        endpos = target[i].position;
        //float radius = Mathf.Abs(ground.rings[PlayerRingPos].sections[0].transform.position.z);
        float radius = Mathf.Sqrt(Mathf.Pow(target[i].position.x, 2) + Mathf.Pow(target[i].position.z, 2));
        Projectile pr;

        pr = Instantiate(projectile, spawnpos, Quaternion.identity);
        pr.CircleTrajectory(radius, height, duration, spawnpos, endpos, i);
    }
}

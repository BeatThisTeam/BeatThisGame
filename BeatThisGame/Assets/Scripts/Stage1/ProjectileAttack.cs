using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : Attack {

    public float damage;

    public Projectile projectile;
    public Projectile rejectableProjectile;

    public float spawnHeight = 12f;

    private int initialTargetSection;
    private int nAttacks = 0;

    private Vector3 spawnPos;
    public Transform target;

    public override void StartAttack(float duration) {

        initialTargetSection = playerCtrl.faceIndex;
        spawnPos = new Vector3(boss.position.x, target.position.y, boss.position.z);
        Vector3 targetPos = target.position;
        nAttacks = 0;       
        ground.rings[playerCtrl.ringIndex].sections[playerCtrl.faceIndex].isTarget = true;
        FireProjectile(spawnPos, targetPos, duration, false);
    }

    public void StartAttackRejectable(float duration) {

        initialTargetSection = playerCtrl.faceIndex;
        spawnPos = new Vector3(boss.position.x, target.position.y, boss.position.z);
        Vector3 targetPos = target.position;
        nAttacks = 0;       
        ground.rings[playerCtrl.ringIndex].sections[playerCtrl.faceIndex].isTarget = true;
        FireProjectile(spawnPos, targetPos, duration, true);
    }

    public void ContinueAttack(float duration) {

        int ringIndex = playerCtrl.ringIndex;
        int numberOfSections = ground.rings[0].sections.Count;
        nAttacks++;
        int targetModifier = -(nAttacks);
        Debug.Log(targetModifier);
        Projectile pr;
        for (int i = 0; i < nAttacks + 1; i++) {

            int targetSection = (initialTargetSection + numberOfSections + targetModifier) % numberOfSections;
            Vector3 targetPos = new Vector3(ground.rings[ringIndex].sections[targetSection].tr.position.x, spawnHeight, ground.rings[ringIndex].sections[targetSection].tr.position.z);
            ground.rings[playerCtrl.ringIndex].sections[targetSection].isTarget = true;
            pr = Instantiate(projectile, spawnPos, Quaternion.identity);
            pr.player = player;
            pr.att = this;
            pr.damage = damage;
            pr.Move(spawnPos, targetPos, duration);
            targetModifier += 2;
        }

    }

    private void FireProjectile(Vector3 startPos, Vector3 endPos, float duration, bool rejectable) {

        Projectile pr;
        if (rejectable) {
            pr = Instantiate(rejectableProjectile, spawnPos, Quaternion.identity);
        } else {
            pr = Instantiate(projectile, spawnPos, Quaternion.identity);
        }       
        pr.player = player;
        pr.att = this;
        pr.playerFacePos = playerCtrl.faceIndex;
        pr.playerRingPos = playerCtrl.ringIndex;
        pr.damage = damage;
        pr.Move(startPos, endPos, duration);
    }

    public void ResetTargetSections(int faceIndex, int ringIndex) {

        ground.rings[ringIndex].sections[faceIndex].isTarget = false;
    }

}

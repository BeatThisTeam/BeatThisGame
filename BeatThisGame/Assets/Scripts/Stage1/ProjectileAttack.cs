using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileAttack : Attack {

    public float damage;

    public Projectile projectile;
    public Projectile rejectableProjectile;

    public float spawnHeight = 12f;

    private int initialTargetSection;
    private int nAttacks = 0;

    private Vector3 spawnPos;
    public Transform target;
    private int targetModifier = 0;

    public List<Projectile> firedProjectiles = new List<Projectile>();

    private UnityAction action;

    public TilesAttack tilesAttack;

    bool attackContinued = false;

    public override void StartAttack(float duration) {
        
        //EventManager.StartListening("note", action);
        initialTargetSection = playerCtrl.faceIndex;
        spawnPos = new Vector3(boss.position.x, target.position.y, boss.position.z);
        Vector3 targetPos = target.position;
        nAttacks = 0;       
        ground.rings[playerCtrl.ringIndex].sections[playerCtrl.faceIndex].isTarget = true;
        FireProjectile(spawnPos, targetPos, duration, false, playerCtrl.ringIndex, playerCtrl.faceIndex);
    }

    public void StartAttackRejectable(float duration) {
        
        EventManager.StartListening("note", action);
        initialTargetSection = playerCtrl.faceIndex;
        spawnPos = new Vector3(boss.position.x, target.position.y, boss.position.z);
        Vector3 targetPos = target.position;
        nAttacks = 0;       
        ground.rings[playerCtrl.ringIndex].sections[playerCtrl.faceIndex].isTarget = true;
        FireProjectile(spawnPos, targetPos, duration, true, playerCtrl.ringIndex, playerCtrl.faceIndex);
    }

    public void StartAttackWithTile(float duration) {

        initialTargetSection = playerCtrl.faceIndex;
        spawnPos = new Vector3(boss.position.x, target.position.y, boss.position.z);
        Vector3 targetPos = target.position;
        nAttacks = 0;
        ground.rings[playerCtrl.ringIndex].sections[playerCtrl.faceIndex].isTarget = true;
        FireProjectile(spawnPos, targetPos, duration, false, playerCtrl.ringIndex, playerCtrl.faceIndex);
        tilesAttack.AttackOnFace(duration, playerCtrl.faceIndex);


        if (Random.value > 0.5) {
            targetModifier++;
        } else {
            targetModifier--;
        }

        int numberOfSections = ground.rings[0].sections.Count;
        int targetSection = (initialTargetSection + numberOfSections + targetModifier) % numberOfSections;
        gcc.ChangeColor(playerCtrl.ringIndex, targetSection);
        ground.SwitchFace(playerCtrl.ringIndex, targetSection);
    }

    public void ContinueAttackWithTile(float duration) {

        if (!attackContinued) {

            if (targetModifier == -1) {
                targetModifier = 1;
                attackContinued = true;
            } else if (targetModifier == 1) {
                targetModifier = -1;
                attackContinued = true;
            }
        } else {
            if (targetModifier > 0 ) {
                targetModifier++;
            } else{
                targetModifier--;
            }
        }

        int numberOfSections = ground.rings[0].sections.Count;
        int targetSection = (initialTargetSection + numberOfSections + targetModifier) % numberOfSections;

        spawnPos = new Vector3(boss.position.x, target.position.y, boss.position.z);
        Vector3 targetPos = ground.rings[playerCtrl.ringIndex].sections[targetSection].sectionTarget.position;
        nAttacks = 0;
        ground.rings[playerCtrl.ringIndex].sections[targetSection].isTarget = true;
        FireProjectile(spawnPos, targetPos, duration, false, playerCtrl.ringIndex, targetSection);
        tilesAttack.AttackOnFace(duration, targetSection);
    }

    //public void ContinueAttack(float duration) {

    //    int ringIndex = playerCtrl.ringIndex;
    //    int numberOfSections = ground.rings[0].sections.Count;
    //    nAttacks++;
    //    int targetModifier = -(nAttacks);
    //    Debug.Log(targetModifier);
    //    Projectile pr;
    //    for (int i = 0; i < nAttacks + 1; i++) {

    //        int targetSection = (initialTargetSection + numberOfSections + targetModifier) % numberOfSections;
    //        Vector3 targetPos = ground.rings[ringIndex].sections[targetSection].sectionTarget.position;

    //        //float targetPosX = ground.rings[ringIndex].sections[targetSection].tr.position.x + target.localPosition.x;
    //        //float targetPosZ = ground.rings[ringIndex].sections[targetSection].tr.position.z + target.localPosition.z;
    //        //Vector3 targetPos = new Vector3(targetPosX, target.position.y, targetPosZ);
    //        ground.rings[playerCtrl.ringIndex].sections[targetSection].isTarget = true;
    //        FireProjectile(spawnPos, targetPos, duration, false, playerCtrl.ringIndex, targetSection);
    //        targetModifier += 2;
    //    }
    //}

    

    private void FireProjectile(Vector3 startPos, Vector3 endPos, float duration, bool rejectable, int targetRing, int targetFace) {

        Projectile pr;
        if (rejectable) {
            pr = Instantiate(rejectableProjectile, spawnPos, Quaternion.identity);
        } else {
            pr = Instantiate(projectile, spawnPos, Quaternion.identity);
        }
        pr.numFired = nAttacks + 1;
        pr.player = player;
        pr.att = this;
        pr.playerFacePos = targetFace;
        pr.playerRingPos = targetRing;
        pr.damage = damage;
        firedProjectiles.Add(pr);
        pr.Move(startPos, endPos, duration);
    }

    //private void DestroyGameObject() {

    //    if (firedProjectiles.Count > 0) {

    //        int projectilesToDestroy = firedProjectiles[0].numFired;
    //        Debug.Log("da distruggere " + projectilesToDestroy);

    //        for (int i = 0; i < projectilesToDestroy; i++) {
    //            Projectile pr = firedProjectiles[i];
    //            firedProjectiles.RemoveAt(i);
    //            pr.DestroyGameObject();
    //        }
    //    }
    //}

    public void ResetTargetSections(int faceIndex, int ringIndex) {

        ground.rings[ringIndex].sections[faceIndex].isTarget = false;
    }

}

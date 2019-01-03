using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : Attack {

    public float damage;

    public Projectile projectile;
    public Projectile rejectableProjectile;

    public float[] ringDistances = new float[3];

    public float spawnHeight = 12f;

    private int initialTargetSection;
    private int nAttacks = 0;

    private Vector3 spawnPos;
    public Transform target;

    private int targetRing;
    private int targetModifier = 0;

    public TilesAttack tilesAttack;

    bool attackContinued = false;


    public override void StartAttack(float duration) {

        Vector3 lookAtPos = player.position - boss.transform.position;
        lookAtPos.y = 0;
        boss.transform.rotation = Quaternion.LookRotation(lookAtPos);
        boss.transform.Rotate(0, 90, 0);

        initialTargetSection = playerCtrl.faceIndex;
        spawnPos = new Vector3(boss.position.x, target.position.y, boss.position.z);
        Vector3 targetPos = target.position;
        nAttacks = 0;       
        ground.rings[playerCtrl.ringIndex].sections[playerCtrl.faceIndex].isTarget = true;
        FireProjectile(spawnPos, targetPos, duration, false, playerCtrl.ringIndex, playerCtrl.faceIndex);
    }

    public void StartAttackRejectable(float duration) {

        spawnPos = new Vector3(boss.position.x, target.position.y, boss.position.z);
        Vector3 targetPos = target.position;
        targetRing = playerCtrl.ringIndex;
        initialTargetSection = playerCtrl.faceIndex;
        targetModifier = 0;

        if (ringDistances[targetRing] == 0) {
            ringDistances[targetRing] = Vector3.Distance(spawnPos, targetPos);
        }
        
        Vector3 lookAtPos = player.position - boss.transform.position;
        lookAtPos.y = 0;
        boss.transform.rotation = Quaternion.LookRotation(lookAtPos);
        boss.transform.Rotate(0, 90, 0);

        initialTargetSection = playerCtrl.faceIndex;      
        nAttacks = 0;       
        ground.rings[playerCtrl.ringIndex].sections[playerCtrl.faceIndex].isTarget = true;
        FireProjectile(spawnPos, targetPos, duration, true, playerCtrl.ringIndex, playerCtrl.faceIndex);
    }

    public void ContinueAttackRejectable(float duration) {

        if(targetModifier == 0) {
            if(Random.Range(0f,1f) > 0.5) {
                targetModifier++;
            } else {
                targetModifier--;
            }
        }else if(targetModifier > 0) {
            targetModifier++;
        } else {
            targetModifier--;
        }

        int numberOfSections = ground.rings[0].sections.Count;
        int targetSection = (initialTargetSection + numberOfSections + targetModifier) % numberOfSections;

        Vector3 direction = new Vector3(ground.rings[targetRing].sections[targetSection].tr.position.x, target.position.y, ground.rings[targetRing].sections[targetSection].tr.position.z) - spawnPos;       
        direction = Vector3.Normalize(direction);
        Vector3 targetPos = direction * ringDistances[targetRing];
        targetPos.y = target.position.y;
        Debug.Log(targetPos);
        Debug.Log(target.position);
        FireProjectile(spawnPos, targetPos, duration, true, targetRing, targetSection);
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
    //        Vector3 targetPos = new Vector3(ground.rings[ringIndex].sections[targetSection].tr.position.x, spawnHeight, ground.rings[ringIndex].sections[targetSection].tr.position.z);
    //        ground.rings[playerCtrl.ringIndex].sections[targetSection].isTarget = true;
    //        pr = Instantiate(projectile, spawnPos, Quaternion.identity);
    //        pr.player = player;
    //        pr.att = this;
    //        pr.damage = damage;
    //        pr.Move(spawnPos, targetPos, duration);
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
        pr.player = player;
        pr.att = this;
        pr.playerFacePos = targetFace;
        pr.playerRingPos = targetRing;
        pr.damage = damage;
        pr.Move(startPos, endPos, duration);
    }

    public void ResetTargetSections(int faceIndex, int ringIndex) {

        ground.rings[ringIndex].sections[faceIndex].isTarget = false;
    }

    public void StartAttackWithTile(float duration) {

        Vector3 lookAtPos = player.position - boss.transform.position;
        lookAtPos.y = 0;
        boss.transform.rotation = Quaternion.LookRotation(lookAtPos);
        boss.transform.Rotate(0, 90, 0);

        targetModifier = 0;
        attackContinued = false;
        initialTargetSection = playerCtrl.faceIndex;
        int numberOfSections = ground.rings[0].sections.Count;
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

        int targetSection = (initialTargetSection + numberOfSections + targetModifier) % numberOfSections;
        gcc.ChangeColor(playerCtrl.ringIndex, targetSection);
        ground.SwitchFace(playerCtrl.ringIndex, targetSection);
    }

    public void ContinueAttackWithTile(float duration) {

        Vector3 lookAtPos = player.position - boss.transform.position;
        lookAtPos.y = 0;
        boss.transform.rotation = Quaternion.LookRotation(lookAtPos);
        boss.transform.Rotate(0, 90, 0);

        if (!attackContinued) {

            if (targetModifier == -1) {
                targetModifier = 1;
                attackContinued = true;
            } else if (targetModifier == 1) {
                targetModifier = -1;
                attackContinued = true;
            }
        } else {
            if (targetModifier > 0) {
                targetModifier++;
            } else {
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

}

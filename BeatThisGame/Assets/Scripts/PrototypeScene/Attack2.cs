using UnityEngine;

public class Attack2 : MonoBehaviour {

    Transform tr;

    public float damage;

    public Projectile projectile;
    public Projectile rejectableProjectile;

    public Transform player;
    public Transform boss;
    public GroundSections groundSections;

    public float spawnHeight = 12f;
    private float lastNotePlayedInSeconds;

    private int initialTargetSection;
    private int nAttacks = 0;

    private Vector3 spawnPos;
    private PlayerController playerContr;

    public void StartAttack(float duration){
        
        playerContr = player.GetComponent<PlayerController>();
        initialTargetSection = playerContr.faceIndex;
        spawnPos = new Vector3(boss.position.x, spawnHeight, boss.position.z);
        Vector3 targetPos = new Vector3(player.position.x, spawnHeight, player.position.z);
        nAttacks = 0;
        Projectile pr;
        groundSections.rings[playerContr.ringIndex].sections[playerContr.faceIndex].isTarget = true;
        pr = Instantiate(projectile, spawnPos, Quaternion.identity);
        pr.player = player;
        pr.att = this;
        pr.playerFacePos = playerContr.faceIndex;
        pr.playerRingPos = playerContr.ringIndex;
        pr.damage = damage;
        pr.Move(spawnPos, targetPos, duration);
    }

    public void StartAttackRejectable(float duration) {

        playerContr = player.GetComponent<PlayerController>();
        initialTargetSection = playerContr.faceIndex;
        spawnPos = new Vector3(boss.position.x, spawnHeight, boss.position.z);
        Vector3 targetPos = new Vector3(player.position.x, spawnHeight, player.position.z);
        nAttacks = 0;
        Projectile pr;
        groundSections.rings[playerContr.ringIndex].sections[playerContr.faceIndex].isTarget = true;
        pr = Instantiate(rejectableProjectile, spawnPos, Quaternion.identity);
        pr.player = player;
        pr.rejectable = true;
        pr.att = this;
        pr.playerFacePos = playerContr.faceIndex;
        pr.playerRingPos = playerContr.ringIndex;
        pr.damage = damage;
        pr.Move(spawnPos, targetPos, duration);
    }

    public void ContinueAttack(float duration) {
      
        int ringIndex = playerContr.ringIndex;
        int numberOfSections = groundSections.rings[0].sections.Count;
        nAttacks++;
        int targetModifier = -(nAttacks);
        Debug.Log(targetModifier);
        Projectile pr;
        for(int i = 0; i< nAttacks + 1; i++) {

            int targetSection = (initialTargetSection + numberOfSections + targetModifier) % numberOfSections;
            Vector3 targetPos = new Vector3(groundSections.rings[ringIndex].sections[targetSection].tr.position.x, spawnHeight, groundSections.rings[ringIndex].sections[targetSection].tr.position.z);
            groundSections.rings[playerContr.ringIndex].sections[targetSection].isTarget = true;
            pr = Instantiate(projectile, spawnPos, Quaternion.identity);
            pr.player = player;
            pr.att = this;
            pr.damage = damage;
            pr.Move(spawnPos, targetPos, duration);
            targetModifier += 2;
        }
    }

    public void ResetTargetSections(int faceIndex, int ringIndex) {

        groundSections.rings[ringIndex].sections[faceIndex].isTarget = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : MonoBehaviour {

    Transform tr;

    public Projectile projectile;

    public Transform player;
    public Transform boss;
    public GroundSections groundSections;

    public float spawnHeight = 12f;
    private float lastNotePlayedInSeconds;

    private int initialTargetSection;
    private int nAttacks = 0;

    private Vector3 spawnPos;
    private CharacterController playerContr;

    public void StartAttack(float noteToPlayInSeconds){

        
        playerContr = player.GetComponent<CharacterController>();
        initialTargetSection = playerContr.faceIndex;
        spawnPos = new Vector3(boss.position.x, spawnHeight, boss.position.z);
        Vector3 targetPos = new Vector3(player.position.x, spawnHeight, player.position.z);
        //float projectileTime = Vector3.Distance(spawnPos, new Vector3(player.position.x, spawnHeight, player.position.z)) / projectileSpeed;
        //float projectileTime = ScenePrototypeManager.Instance.notesInSeconds[ScenePrototypeManager.Instance.notesInSecondsIndex + 1].notePosInSeconds - noteToPlayInSeconds;

        if (noteToPlayInSeconds != 0 && lastNotePlayedInSeconds != noteToPlayInSeconds){

            float projectileTime = noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds;
            if(projectileTime <= 0.1) {
                projectileTime = ScenePrototypeManager.Instance.notesInSeconds[ScenePrototypeManager.Instance.notesInSecondsIndex + 1].notePosInSeconds - noteToPlayInSeconds;
            }
            //Debug.Log(SongManager.Instance.SongPositionInSeconds);
            nAttacks++;
            lastNotePlayedInSeconds = noteToPlayInSeconds;
            Projectile pr;
            pr = Instantiate(projectile, spawnPos, Quaternion.identity);
            pr.Move(spawnPos, targetPos, projectileTime);
            //ScenePrototypeManager.Instance.IncrementNoteToPlayInSeconds();
        }
    }

    public void ContinueAttack(float noteToPlayInSeconds) {

        
        int ringIndex = playerContr.ringIndex;
        int numberOfSections = groundSections.rings[0].sections.Count;
        
        if (noteToPlayInSeconds != 0 && lastNotePlayedInSeconds != noteToPlayInSeconds) {
            //Debug.Log(SongManager.Instance.SongPositionInSeconds);
            float projectileTime = noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds;
            lastNotePlayedInSeconds = noteToPlayInSeconds;
            //if (nAttacks == 1) {
                nAttacks++;
                //Debug.Log("att2");
                Projectile pr1;
                Projectile pr2;

                int targetSection = (initialTargetSection + numberOfSections + 1) % numberOfSections;
                Vector3 targetPos = new Vector3(groundSections.rings[ringIndex].sections[targetSection].tr.position.x, spawnHeight, groundSections.rings[ringIndex].sections[targetSection].tr.position.z);
                pr1 = Instantiate(projectile, spawnPos, Quaternion.identity);
                pr1.Move(spawnPos, targetPos, projectileTime);
                targetSection = (initialTargetSection + numberOfSections - 1) % numberOfSections;
                targetPos = new Vector3(groundSections.rings[ringIndex].sections[targetSection].tr.position.x, spawnHeight, groundSections.rings[ringIndex].sections[targetSection].tr.position.z);
                pr2 = Instantiate(projectile, spawnPos, Quaternion.identity);
                pr2.Move(spawnPos, targetPos, projectileTime);

            //}
        }
    }
}

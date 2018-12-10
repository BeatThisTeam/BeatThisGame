using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : MonoBehaviour {

    Transform tr;

    public Projectile projectile;

    public Transform player;
    public Transform boss;

    public float spawnHeight = 12f;
    //public float projectileSpeed = 20f;
    private float lastNotePlayedInSeconds;

    public void StartAttack(float noteToPlayInSeconds )
    {

        Vector3 spawnPos = new Vector3(boss.position.x, spawnHeight, boss.position.z);
        Vector3 targetPos = new Vector3(player.position.x, spawnHeight, player.position.z);
        //float projectileTime = Vector3.Distance(spawnPos, new Vector3(player.position.x, spawnHeight, player.position.z)) / projectileSpeed;
        float projectileTime = ScenePrototypeManager.Instance.notesInSeconds[ScenePrototypeManager.Instance.notesInSecondsIndex + 1].notePosInSeconds - noteToPlayInSeconds;

        if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= projectileTime && noteToPlayInSeconds != 0 && lastNotePlayedInSeconds != noteToPlayInSeconds)
        {
            lastNotePlayedInSeconds = noteToPlayInSeconds;
            Projectile pr;
            pr = Instantiate(projectile, spawnPos, Quaternion.identity);
            pr.Move(spawnPos, targetPos, projectileTime);
            //ScenePrototypeManager.Instance.IncrementNoteToPlayInSeconds();
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : MonoBehaviour {

    Transform tr;

    public Projectile projectile;

    public Transform player;

 

    public float spawnHeight = 12f;
    public float projectileSpeed = 10f;




    public void StartAttack(float noteToPlayInSeconds )
    {

        Vector3 spawnPos = new Vector3(player.position.x, spawnHeight, player.position.z);
        float projectileTime = Vector3.Distance(spawnPos, player.position) / projectileSpeed;

        if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= projectileTime && noteToPlayInSeconds % 2 != 0 && SongManager.Instance.SongPositionInSeconds >= noteToPlayInSeconds)
        {
            Projectile pr;
            pr = Instantiate(projectile, spawnPos, Quaternion.identity);
            pr.Move(spawnPos, player.position, projectileTime);
            ScenePrototypeManager.Instance.IncrementNoteToPlayInSeconds();
        }
    }
}

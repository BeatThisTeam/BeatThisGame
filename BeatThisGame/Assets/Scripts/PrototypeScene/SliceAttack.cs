using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceAttack : MonoBehaviour {

    int ringIndex;
    int faceIndex;

    public GroundColorChanger ground;
    public CharacterController player;
    //public GroundSections groundSections;


    public void StartAttack(float noteToPlayInSeconds) {
        ringIndex = player.ringIndex;
        faceIndex = player.faceIndex +1;

        ground.ChangeColorSlice(faceIndex);

          
        }


        //public void StartAttack(float noteToPlayInSeconds)
        //{

        //    Vector3 spawnPos = new Vector3(player.position.x, spawnHeight, player.position.z);
        //    float projectileTime = Vector3.Distance(spawnPos, player.position) / projectileSpeed;

        //    if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= 0 && noteToPlayInSeconds % 2 == 0)

        //    //if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= projectileTime && noteToPlayInSeconds % 2 != 0 && SongManager.Instance.SongPositionInSeconds >= noteToPlayInSeconds)
        //{
        //    Projectile pr;
        //    pr = Instantiate(projectile, spawnPos, Quaternion.identity);
        //    pr.Move(spawnPos, player.position, projectileTime);
        //    ScenePrototypeManager.Instance.IncrementNoteToPlayInSeconds();
    }
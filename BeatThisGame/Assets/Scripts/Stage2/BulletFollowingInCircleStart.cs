using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFollowingInCircleStart : Attack {

    private Transform[] target;
    public Transform targetDx;
    public Transform targetSx;

    //public PlayerController Player;
    // public int PlayerFacePos;
    //  public int PlayerRingPos;
   // public GroundSections Ground;

    //public float speed;

    public BulletFollowingInCircle projectile;
    private Vector3 spawnpos;
    private Vector3 endpos;

    private float randomNumber;
    private int i;
    private int PlayerFacePosNew;
    int sliceCount;
   

   // public float radius;
    


    public override void StartAttack(float duration)
    {
        target = new Transform[2];
        target[0] = targetDx;
        target[1] = targetSx;

        sliceCount = ground.rings[0].sections.Count;

        int PlayerFacePos = playerCtrl.faceIndex;
        int PlayerRingPos = playerCtrl.ringIndex;

        float height = targetDx.position.y;

        randomNumber = Random.Range(0.0f, 1.0f);

        if (randomNumber < 0.5f)
        {
            i = 0;
            PlayerFacePosNew = ((PlayerFacePos + 4) + sliceCount) % sliceCount;
            
        }
        if (randomNumber >= 0.5f)
        {
            i = 1;
            PlayerFacePosNew = ((PlayerFacePos - 4) + sliceCount) % sliceCount;
        }

        Debug.Log("random " + i);
        Debug.Log("spawn pos " + PlayerFacePosNew);

        spawnpos = new Vector3(ground.rings[PlayerRingPos].sections[PlayerFacePosNew].transform.position.x, target[i].position.y, ground.rings[PlayerRingPos].sections[PlayerFacePosNew].transform.position.z);

        endpos = target[i].position;

        float radius = Mathf.Abs(ground.rings[PlayerRingPos].sections[0].transform.position.z);

        BulletFollowingInCircle pr;
        pr = Instantiate(projectile, spawnpos, Quaternion.identity);

        pr.radius = radius;
        pr.duration = duration;
        pr.height = height;
        pr.CircleTrajectory(radius, height, duration, spawnpos, endpos, i);



    }

}

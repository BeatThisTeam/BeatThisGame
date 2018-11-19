using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceAttack : MonoBehaviour
{
    public Transform _player;
    public Transform _ground;
    public Transform _wall;
    public Transform _centralring;

    public int LimitTime;

    int RingIndex;
    int FaceIndex;
    int newFaceIndex = 0;
    int storeFaceIndex;
    int allredFaceIndex;

    int i = 0;
    int sx = 0;
    int dx = 0;
    int y = 0;


    public float endHeight = 7f;
    public float spawnHeight = 7f;
    public float WallSpeed = 10f;
    public float WaitingTime;


    public Wall wall;

    public GroundColorChanger ground;
    public CharacterController player;
    public GroundSections groundSections;


    public void StartAttack(float noteToPlayInSeconds)
    {
        int SliceCount = groundSections.rings[0].faces.Count;


        if (SongManager.Instance.SongPositionInBeats >= 4 && SongManager.Instance.SongPositionInBeats < 5)
        {
            if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= 0)
            {

                FaceIndex = player.faceIndex;
                storeFaceIndex = player.faceIndex + SliceCount;
                allredFaceIndex = player.faceIndex + SliceCount;

                Vector3 spawnPos = new Vector3(groundSections.rings[2].faces[FaceIndex].position.x, spawnHeight,
                    groundSections.rings[2].faces[FaceIndex].position.z);
                Vector3 endPos = new Vector3(groundSections.rings[2].faces[FaceIndex].position.x,
                    endHeight, groundSections.rings[2].faces[FaceIndex].position.z);

                float WallTime = Vector3.Distance(spawnPos, endPos) / WallSpeed;

                Wall wl;
                wl = Instantiate(wall, spawnPos, Quaternion.identity);
                wl.MoveUpDown(spawnPos, endPos, WallTime, WaitingTime);
                ground.AllRed((storeFaceIndex + 2) % SliceCount);
                ground.AllRed((storeFaceIndex - 2) % SliceCount);

                //AttackPhase2(storeFaceIndex, SliceCount, noteToPlayInSeconds);

                //for (int i = 0; i < 5; i++)
                //{
                //    ground.AllRed(0, allredFaceIndex +2 + i);
                //    ground.AllRed(1, allredFaceIndex + 2 + i);
                //    ground.AllRed(2, allredFaceIndex + 2 + i);
                //}

                //ground.ChangeColorSlice((FaceIndex + 1) % SliceCount);
                //ground.ChangeColorSlice((FaceIndex -1) % SliceCount);

                FaceIndex += SliceCount;
            }

        }


        AttackPhase2(storeFaceIndex, SliceCount, noteToPlayInSeconds);

    }



    public void AttackPhase2(int storeFaceIndex, int SliceCount, float noteToPlayInSeconds)
    {


        if (SongManager.Instance.SongPositionInBeats >= 4)// && SongManager.Instance.SongPositionInBeats < 16)
        {
            if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= 0)
            {
                FaceIndex = (player.faceIndex + SliceCount + y);
                //storeFaceIndex = storeFaceIndex % SliceCount;


                Debug.Log("face" + (FaceIndex % SliceCount));
                Debug.Log("store" + (storeFaceIndex % SliceCount));


                if ((storeFaceIndex % SliceCount) == 8 && (FaceIndex % SliceCount) == 0)
                {
                    Debug.Log("caso speciale uno");
                    dx = 1;
                    SliceAttackRight(storeFaceIndex, SliceCount);
                    y++;
                }

                if ((storeFaceIndex % SliceCount) == 0 && (FaceIndex % SliceCount) == 8)
                {
                    Debug.Log("caso speciale due");
                    sx = 1;
                    SliceAttackLeft(storeFaceIndex, SliceCount);
                    y++;
                }

                else
                {
                    if (sx == 0 && (storeFaceIndex % SliceCount) < (FaceIndex % SliceCount))
                    {
                        Debug.Log("destra normale");
                        dx = 1;
                        SliceAttackRight(storeFaceIndex, SliceCount);
                        storeFaceIndex++;
                        FaceIndex++;
                        y++;
                    }

                    if (dx == 0 && (storeFaceIndex % SliceCount) > (FaceIndex % SliceCount))
                    {
                        Debug.Log("sinistra normale");
                        sx = 1;
                        SliceAttackLeft(storeFaceIndex, SliceCount);
                        storeFaceIndex++;
                        FaceIndex++;
                        y++;
                    }
                }

                //if (storeFaceIndex == FaceIndex)
                //{
                //    Debug.Log("HIT BY THE WALL");
                //}
                //else
                //{

                //    if (sx == 0 && storeFaceIndex < FaceIndex && i != 8)
                //    {
                //        Debug.Log("face again " + FaceIndex);

                //        dx = 1;

                //        newFaceIndex = i + storeFaceIndex;

                //        Debug.Log("new " + newFaceIndex);
                //        Debug.Log("new + 2 " + ((newFaceIndex + 2) % SliceCount));
                //        Debug.Log(" i" + i);

                //        ground.ChangeColorSlice((newFaceIndex + 2) % SliceCount);
                //        ground.AllBlue((newFaceIndex + 1) % SliceCount);
                //        ground.AllRed((newFaceIndex) % SliceCount);
                //        i++;
                //    }

                //    if (dx == 0 && storeFaceIndex > (FaceIndex % SliceCount) && i != 8)
                //    {
                //        Debug.Log("face again " + FaceIndex);

                //        sx = 1;

                //        newFaceIndex = storeFaceIndex - i;

                //        ground.ChangeColorSlice((newFaceIndex - 2) % SliceCount);
                //        ground.AllBlue((newFaceIndex - 1) % SliceCount);
                //        ground.AllRed((newFaceIndex) % SliceCount);
                //        i++;
                //    }

                //}



            }


        }
    }

    public void SliceAttackRight(int storeFaceIndex, int SliceCount)
    {
        if (i != 8)
        {
            Debug.Log("i " + i);
            newFaceIndex = i + storeFaceIndex;

            ground.ChangeColorSlice((newFaceIndex + 3) % SliceCount);
            ground.AllBlue((newFaceIndex + 2) % SliceCount);
            ground.AllRed((newFaceIndex + 1) % SliceCount);
            ground.AllRed((newFaceIndex) % SliceCount);
            i++;
        }
    }

    public void SliceAttackLeft(int storeFaceIndex, int SliceCount)
    {
        if (i != 8)
        {
            Debug.Log("i " + i);
            newFaceIndex = storeFaceIndex - i;

            ground.ChangeColorSlice((newFaceIndex - 3) % SliceCount);
            ground.AllBlue((newFaceIndex - 2) % SliceCount);
            ground.AllRed((newFaceIndex - 1) % SliceCount);
            ground.AllRed((newFaceIndex) % SliceCount);
            i++;
        }
    }

    //public void SliceAttackLeft(int storeFaceIndex, int SliceCount)
    //{

    //    StartCoroutine(SliceAttackLeftCoroutine(storeFaceIndex, SliceCount));
    //}

    //IEnumerator SliceAttackLeftCoroutine(int storeFaceIndex, int SliceCount)
    //{
    //    for (int i = 0; i != 8; i++)
    //    {
    //        newFaceIndex = storeFaceIndex - i;

    //        ground.ChangeColorSlice((newFaceIndex - 3) % SliceCount);
    //        ground.AllBlue((newFaceIndex - 2) % SliceCount);
    //        ground.AllRed((newFaceIndex - 1) % SliceCount);
    //        ground.AllRed((newFaceIndex) % SliceCount);
    //        i++;
    //    }
    //}


}











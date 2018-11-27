using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceAttack : MonoBehaviour
{
    public Transform _player;
    public Transform _ground;
    public Transform _wall;
    //public Transform _centralring;

    public int LimitTime;

    int RingIndex;
    int faceIndex;
    int newFaceIndex = 0;
    int storeFaceIndex;
    int allredFaceIndex;

    int i = 0;
    int sx = 0;
    int dx = 0;
    int y = 0;
    private bool set = false;

    //-1 left +1 right
    private int direction = 0;
    private int index;

    public float endHeight = 7f;
    public float spawnHeight = 7f;
    int sliceCount;
    public Wall wallPrefab;
    private Wall wall;

    public GroundColorChanger ground;
    public CharacterController player;
    public GroundSections groundSections;

    public Material mat1;
    public Material mat2;

    public void StartAttack(float noteToPlayInSeconds){

        sliceCount = groundSections.rings[0].sections.Count;

        if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= 0){

            faceIndex = player.faceIndex;
            storeFaceIndex = player.faceIndex + sliceCount;
            allredFaceIndex = player.faceIndex + sliceCount;

            Vector3 spawnPos = new Vector3(groundSections.rings[2].sections[faceIndex].tr.position.x, spawnHeight, groundSections.rings[2].sections[faceIndex].tr.position.z);
            Vector3 endPos = new Vector3(groundSections.rings[2].sections[faceIndex].tr.position.x, endHeight, groundSections.rings[2].sections[faceIndex].tr.position.z);

            float WallTime = ScenePrototypeManager.Instance.notesInSeconds[ScenePrototypeManager.Instance.notesInSecondsIndex + 1].notePosInSeconds - noteToPlayInSeconds;

            wall = Instantiate(wallPrefab, spawnPos, Quaternion.identity);
            wall.MoveUpDown(spawnPos, endPos, WallTime);

            ground.ChangeColorSlice((storeFaceIndex + 2) % sliceCount, mat2);
            groundSections.rings[0].sections[(storeFaceIndex + 2) % sliceCount].hurts = true;
            groundSections.rings[1].sections[(storeFaceIndex + 2) % sliceCount].hurts = true;
            groundSections.rings[2].sections[(storeFaceIndex + 2) % sliceCount].hurts = true;


            ground.ChangeColorSlice((storeFaceIndex - 2) % sliceCount, mat2);
            groundSections.rings[0].sections[(storeFaceIndex - 2) % sliceCount].hurts = true;
            groundSections.rings[1].sections[(storeFaceIndex - 2) % sliceCount].hurts = true;
            groundSections.rings[2].sections[(storeFaceIndex - 2) % sliceCount].hurts = true;

            faceIndex += sliceCount;
        }
    }

    public void AttackPhase2(float noteToPlayInSeconds){

        if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= 0){

            if (!set) {
                faceIndex = (player.faceIndex + sliceCount);
                direction = faceIndex - storeFaceIndex;
                index = (faceIndex + direction) % sliceCount;                
                set = true;
            } else {
                index = (index + sliceCount + direction) % sliceCount;
            }
            //Debug.Log(index);
            ground.ChangeColorSlice(index, mat1);
            groundSections.rings[0].sections[index].hurts = false;
            groundSections.rings[1].sections[index].hurts = false;
            groundSections.rings[2].sections[index].hurts = false;

            ground.ChangeColorSlice((index + sliceCount + 1) % sliceCount, mat2);
            groundSections.rings[0].sections[(index + sliceCount + 1) % sliceCount].hurts = true;
            groundSections.rings[1].sections[(index + sliceCount + 1) % sliceCount].hurts = true;
            groundSections.rings[2].sections[(index + sliceCount + 1) % sliceCount].hurts = true;

            ground.ChangeColorSlice((index + sliceCount - 1) % sliceCount, mat2);
            groundSections.rings[0].sections[(index + sliceCount - 1) % sliceCount].hurts = true;
            groundSections.rings[1].sections[(index + sliceCount - 1) % sliceCount].hurts = true;
            groundSections.rings[2].sections[(index + sliceCount - 1) % sliceCount].hurts = true;


            //ground.AllBlue(index);
            //ground.AllRed((index + sliceCount + 1) % sliceCount);
            //ground.AllRed((index + sliceCount - 1) % sliceCount);

            //Debug.Log("face" + (faceIndex % sliceCount));
            //Debug.Log("store" + (storeFaceIndex % sliceCount));

            //if ((storeFaceIndex % sliceCount) == 8 && (faceIndex % sliceCount) == 0){
            //    Debug.Log("caso speciale uno");
            //    dx = 1;
            //    SliceAttackRight(storeFaceIndex, sliceCount);
            //    y++;
            //}

            //if ((storeFaceIndex % sliceCount) == 0 && (faceIndex % sliceCount) == 8){
            //    Debug.Log("caso speciale due");
            //    sx = 1;
            //    SliceAttackLeft(storeFaceIndex, sliceCount);
            //    y++;
            //}

            //else{
            //    if (sx == 0 && (storeFaceIndex % sliceCount) < (faceIndex % sliceCount)){
            //        Debug.Log("destra normale");
            //        dx = 1;
            //        SliceAttackRight(storeFaceIndex, sliceCount);
            //        storeFaceIndex++;
            //        faceIndex++;
            //        y++;
            //    }

            //    if (dx == 0 && (storeFaceIndex % sliceCount) > (faceIndex % sliceCount)){
            //        Debug.Log("sinistra normale");
            //        sx = 1;
            //        SliceAttackLeft(storeFaceIndex, sliceCount);
            //        storeFaceIndex++;
            //        faceIndex++;
            //        y++;
            //    }
            //}
        }
    }

    //public void SliceAttackRight(int storeFaceIndex, int SliceCount)
    //{
    //    if (i != 8)
    //    {
    //        Debug.Log("i " + i);
    //        newFaceIndex = i + storeFaceIndex;

    //        ground.ChangeColorSlice((newFaceIndex + 3) % SliceCount);
    //        ground.AllBlue((newFaceIndex + 2) % SliceCount);
    //        ground.AllRed((newFaceIndex + 1) % SliceCount);
    //        ground.AllRed((newFaceIndex) % SliceCount);
    //        i++;
    //    }
    //}

    //public void SliceAttackLeft(int storeFaceIndex, int SliceCount)
    //{
    //    if (i != 8)
    //    {
    //        Debug.Log("i " + i);
    //        newFaceIndex = storeFaceIndex - i;

    //        ground.ChangeColorSlice((newFaceIndex - 3) % SliceCount);
    //        ground.AllBlue((newFaceIndex - 2) % SliceCount);
    //        ground.AllRed((newFaceIndex - 1) % SliceCount);
    //        ground.AllRed((newFaceIndex) % SliceCount);
    //        i++;
    //    }
    //}

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

    public void ClearSections() {

        for (int i = 0; i < groundSections.rings.Count; i++) {
            for (int j = 0; j < groundSections.rings[i].sections.Count; j++) {
                groundSections.rings[i].sections[j].hurts = false;
                ground.ChangeColor(i, j, mat1);
            }
        }

        Destroy(wall.gameObject);
        set = false;
        //firstAtt = true;
    }

}











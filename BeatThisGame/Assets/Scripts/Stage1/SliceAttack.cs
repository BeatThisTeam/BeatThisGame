using UnityEngine;

public class SliceAttack : MonoBehaviour
{
    public Transform _player;
    public Transform _ground;
    public Transform _wall;

    public int LimitTime;
    
    int faceIndex;
    int storeFaceIndex;
    int allredFaceIndex;
    private bool set = false;

    //-1 left +1 right
    private int direction = 0;
    private int index;

    public float endHeight = 7f;
    public float spawnHeight = 7f;
    int sliceCount;

    public BossController bossContr;

    public GroundColorChanger ground;
    public PlayerController player;
    public GroundSections groundSections;

    public Material mat1;
    public Material mat2;

    public TilesAttack tilesAttack;

    public void StartAttack(float duration){

        sliceCount = groundSections.rings[0].sections.Count;

        faceIndex = player.faceIndex;
        storeFaceIndex = player.faceIndex + sliceCount;
        Debug.Log("sfi" + storeFaceIndex);
        allredFaceIndex = player.faceIndex + sliceCount;

        //Vector3 spawnPos = new Vector3(groundSections.rings[2].sections[faceIndex].tr.position.x, spawnHeight, groundSections.rings[2].sections[faceIndex].tr.position.z);
        //Vector3 endPos = new Vector3(groundSections.rings[2].sections[faceIndex].tr.position.x, endHeight, groundSections.rings[2].sections[faceIndex].tr.position.z);

        //float WallTime = ScenePrototypeManager.Instance.notesInSeconds[ScenePrototypeManager.Instance.notesInSecondsIndex + 1].notePosInSeconds - noteToPlayInSeconds;

        Vector3 lookAtPos = _player.position - bossContr.transform.position;
        lookAtPos.y = 0;
        bossContr.transform.rotation = Quaternion.LookRotation(lookAtPos);
        bossContr.transform.Rotate(0, 90, 0);
        bossContr.StartSlam(duration);

        groundSections.rings[0].sections[faceIndex].isTarget = true;
        //groundSections.rings[1].sections[faceIndex].isTarget = true;
        //groundSections.rings[2].sections[faceIndex].isTarget = true;

        //ground.ChangeColorSlice((storeFaceIndex + 2) % sliceCount, 0f);
        tilesAttack.AttackOnFace(duration, (storeFaceIndex + 2) % sliceCount);
        groundSections.SwitchFaceDelayed(player.ringIndex, (storeFaceIndex + 2) % sliceCount, true, duration);
        //groundSections.SwitchFace(1, (storeFaceIndex + 2) % sliceCount, true);
        //groundSections.SwitchFace(2, (storeFaceIndex + 2) % sliceCount, true);

        //ground.ChangeColorSlice((storeFaceIndex - 2) % sliceCount, 0f);
        tilesAttack.AttackOnFace(duration, (storeFaceIndex - 2) % sliceCount);
        groundSections.SwitchFaceDelayed(player.ringIndex, (storeFaceIndex - 2) % sliceCount, true, duration);
        //groundSections.SwitchFace(1, (storeFaceIndex - 2) % sliceCount, true);
        //groundSections.SwitchFace(2, (storeFaceIndex - 2) % sliceCount, true);

        faceIndex += sliceCount;
    }

    public void AttackPhase2(float duration){

        if (!set) {

            faceIndex = player.faceIndex;

            groundSections.rings[0].sections[faceIndex].isTarget = true;
            //groundSections.rings[1].sections[faceIndex].isTarget = true;
            //groundSections.rings[2].sections[faceIndex].isTarget = true;

            if (player.Dir == PlayerController.Direction.Left) {
                direction = -1;
            }else if(player.Dir == PlayerController.Direction.Right) {
                direction = 1;
            } else if(Random.value > 0.5){
                direction = -1;
            } else {
                direction = 1;
            }

            Debug.Log("dir" + direction);
            index = (storeFaceIndex + 2 * direction) % sliceCount;                
            set = true;
        } else {
            index = (index + sliceCount + direction) % sliceCount;
        }
        Debug.Log(index);
        //ground.ChangeColorSlice(index, duration);
        //tilesAttack.FadeTiles(duration, 0, index);
        groundSections.SwitchFaceDelayed(0, index, false, duration);
        //groundSections.SwitchFace(1, index, false);
        //groundSections.SwitchFace(2, index, false);

        tilesAttack.AttackOnFace(duration, (index + sliceCount + 1) % sliceCount);
        //ground.ChangeColorSlice((index + sliceCount + 1) % sliceCount, duration);
        groundSections.SwitchFaceDelayed(0, (index + sliceCount + 1) % sliceCount, true, duration);
        //groundSections.SwitchFace(1, (index + sliceCount + 1) % sliceCount, true);
        //groundSections.SwitchFace(2, (index + sliceCount + 1) % sliceCount, true);

        tilesAttack.AttackOnFace(duration, (index + sliceCount - 1) % sliceCount);
        //ground.ChangeColorSlice((index + sliceCount - 1) % sliceCount, duration);
        groundSections.SwitchFaceDelayed(0, (index + sliceCount - 1) % sliceCount, true, duration);
        //groundSections.SwitchFace(1, (index + sliceCount - 1) % sliceCount, true);
        //groundSections.SwitchFace(2, (index + sliceCount - 1) % sliceCount, true);
    }

    public void Return() {

        bossContr.StartReturn();
    }

    public void ClearSections(float duration) {

        for (int i = 0; i < groundSections.rings.Count; i++) {
            for (int j = 0; j < groundSections.rings[i].sections.Count; j++) {
                //groundSections.rings[i].sections[j].hurts = false;
                //groundSections.rings[i].sections[j].isTarget = false;
                ground.ResetGround(i, j, duration);
            }
        }
        set = false;
    }
}











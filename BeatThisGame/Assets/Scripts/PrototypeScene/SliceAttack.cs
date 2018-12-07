using UnityEngine;

public class SliceAttack : MonoBehaviour
{
    public Transform _player;
    public Transform _ground;
    public Transform _wall;

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

    public BossController bossContr;

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

            Vector3 lookAtPos = _player.position - bossContr.transform.position;
            lookAtPos.y = 0;
            bossContr.transform.rotation = Quaternion.LookRotation(lookAtPos);
            bossContr.transform.Rotate(0, 90, 0);
            bossContr.StartSlam(WallTime);

            ground.ChangeColorSlice((storeFaceIndex + 2) % sliceCount, mat2);
            groundSections.SwitchFace(0, (storeFaceIndex + 2) % sliceCount, true);
            groundSections.SwitchFace(1, (storeFaceIndex + 2) % sliceCount, true);
            groundSections.SwitchFace(2, (storeFaceIndex + 2) % sliceCount, true);

            ground.ChangeColorSlice((storeFaceIndex - 2) % sliceCount, mat2);
            groundSections.SwitchFace(0, (storeFaceIndex - 2) % sliceCount, true);
            groundSections.SwitchFace(1, (storeFaceIndex - 2) % sliceCount, true);
            groundSections.SwitchFace(2, (storeFaceIndex - 2) % sliceCount, true);

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

            ground.ChangeColorSlice(index, mat1);
            groundSections.SwitchFace(0, index, false);
            groundSections.SwitchFace(1, index, false);
            groundSections.SwitchFace(2, index, false);

            ground.ChangeColorSlice((index + sliceCount + 1) % sliceCount, mat2);
            groundSections.SwitchFace(0, (index + sliceCount + 1) % sliceCount, true);
            groundSections.SwitchFace(1, (index + sliceCount + 1) % sliceCount, true);
            groundSections.SwitchFace(2, (index + sliceCount + 1) % sliceCount, true);

            ground.ChangeColorSlice((index + sliceCount - 1) % sliceCount, mat2);
            groundSections.SwitchFace(0, (index + sliceCount - 1) % sliceCount, true);
            groundSections.SwitchFace(1, (index + sliceCount - 1) % sliceCount, true);
            groundSections.SwitchFace(2, (index + sliceCount - 1) % sliceCount, true);
        }
    }

    public void Return() {

        bossContr.StartReturn();
    }

    public void ClearSections() {

        for (int i = 0; i < groundSections.rings.Count; i++) {
            for (int j = 0; j < groundSections.rings[i].sections.Count; j++) {
                groundSections.rings[i].sections[j].hurts = false;
                ground.ChangeColor(i, j, mat1);
            }
        }
        set = false;
    }
}











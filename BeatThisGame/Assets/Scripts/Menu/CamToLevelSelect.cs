using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamToLevelSelect : MonoBehaviour {

    public GroundSections CamRingUI;

    public FollowCamUI cam;

    //public Transform button;
    public float speed;

    public int FaceIndex = 0;
    private int sliceCount;

    public Transform NewRotation;

    public bool SwitchRing = false;

    //public void CamToLevel(float duration)
    //{
    //    cam.SwitchRing = true;
    //    cam.i = 1;
    //    cam.duration = duration;
    //    StartCoroutine(CamToLevelSelectCoroutine(duration));
        
    //}

    public IEnumerator CamToLevelSelectCoroutine(float duration)
    {
        float TimeCounter = 0f;

        while (TimeCounter < duration)
        {
            Vector3 DesiredPosition = CamRingUI.rings[0].sections[0].tr.position;
            Quaternion DesiredRotation = NewRotation.rotation;

            Vector3 SmoothedPosition = Vector3.Lerp(transform.position, DesiredPosition, speed * Time.deltaTime);
            Quaternion SmoothedRotation = Quaternion.Slerp(transform.rotation, DesiredRotation, speed * Time.deltaTime);

            cam.transform.position = SmoothedPosition;
            cam.transform.rotation = SmoothedRotation;

            TimeCounter += Time.deltaTime;

            yield return null;
        }
    


}
}
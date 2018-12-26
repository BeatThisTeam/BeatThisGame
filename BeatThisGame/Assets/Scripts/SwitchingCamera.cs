using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingCamera : MonoBehaviour {

    public GameObject MainCamera;
    public GameObject OverheadCamera;

    public Transform MainCameraTrans;
    public Transform OverheadCameraTrans;
    public float SmoothSpeed;

    public float WaitingTime;

    public bool active = false;
    
    public void SwitchCamera()
    {
        //Camera MCamera = MainCamera.GetComponent<Camera>();
        //Camera OvCamera = OverheadCamera.GetComponent<Camera>();

        //MCamera.enabled = false;
        //OvCamera.enabled = true;

        MainCamera.SetActive(false);
        OverheadCamera.SetActive(true);

        //active = true;

    }

    public void SwitchCameraBack()
    {
        OverheadCamera.SetActive(false);
        MainCamera.SetActive(true);

       // StartCoroutine(MoveOverheadCamBack(OverheadCameraTrans.position, MainCameraTrans.position, SmoothSpeed));
       
    }

    IEnumerator MoveOverheadCamBack(Vector3 OverheadCam, Vector3 MainCam, float SmoothSpeed)
    {
        Vector3 SmoothedPosition = Vector3.Lerp(OverheadCam, MainCam, SmoothSpeed * Time.deltaTime);
        OverheadCam = SmoothedPosition;

        yield return new WaitForSeconds(WaitingTime);

        OverheadCamera.SetActive(false);
        MainCamera.SetActive(true);
        yield return null;
    }

    //private IEnumerator SwCamera(float WaitingTime, Vector3 TopViewCoord)
    //{
    //    yield return new WaitForSeconds(WaitingTime);
    //    Vector3 SmoothedPosition = Vector3.Lerp(OverheadCamera.transform.position, TopViewCoord, SmoothSpeed * Time.deltaTime);
    //    OverheadCamera.transform.position = SmoothedPosition;

    //    yield return new WaitForSeconds(WaitingTime);
    //}

    //public void SwitchBackCamera()
    //{
    //    Camera MCamera = MainCamera.GetComponent<Camera>();
    //    Camera OvCamera = OverheadCamera.GetComponent<Camera>();

    //    MCamera.enabled = true;
    //    OvCamera.enabled = false;

    //    Vector3 SmoothedPosition = Vector3.Lerp(OverheadCamera.transform.position, MainCamera.transform.position, SmoothSpeed * Time.deltaTime);

    //    OverheadCamera.transform.position = SmoothedPosition;
    //    OverheadCamera.transform.rotation = MainCamera.transform.rotation;

    //    Debug.Log("ov pos  " + OverheadCamera.transform.position);
    //    Debug.Log("ov rot  " + OverheadCamera.transform.rotation);
    //}


    ////    public void SwCamera()
    ////    {
    ////        StartCoroutine(SwitchCamera(WaitingTime));
    ////    }

    ////    private IEnumerator SwitchCamera(float WaitingTime)
    ////    {

    ////        Camera MCamera = MainCamera.GetComponent<Camera>();
    ////        Camera OvCamera = OverheadCamera.GetComponent<Camera>();

    ////        MCamera.enabled = false;
    ////        OvCamera.enabled = true;

    ////        Vector3 TopViewCoord;
    ////        TopViewCoord.x = 0;
    ////        TopViewCoord.y = 110;
    ////        TopViewCoord.z = 0;


    ////            Vector3 SmoothedPosition = Vector3.Lerp(OverheadCamera.transform.position, TopViewCoord, SmoothSpeed * Time.deltaTime);
    ////            OverheadCamera.transform.position = SmoothedPosition;
    ////            OverheadCamera.transform.Rotate(-90, 90, -90);

    ////            //Vector3 SmoothedPosition = Vector3.Lerp(OverheadCamera.transform.position, TopViewCoord, SmoothSpeed * Time.deltaTime);

    //////            OverheadCamera.transform.position = SmoothedPosition;
    ////            //OverheadCamera.transform.Rotate(90, -90, -90);




    ////        yield return new WaitForSeconds(WaitingTime);
    ////    }


    ////    public void SwBackCamera()
    ////    {
    ////        StartCoroutine(SwitchBackCamera(WaitingTime));
    ////    }

    ////    private IEnumerator SwitchBackCamera(float WaitingTime)
    ////    {
    ////        Camera MCamera = MainCamera.GetComponent<Camera>();
    ////        Camera OvCamera = OverheadCamera.GetComponent<Camera>();

    ////        MCamera.enabled = true;
    ////        OvCamera.enabled = false;

    ////        Vector3 SmoothedPosition = Vector3.Lerp(OverheadCamera.transform.position, MainCamera.transform.position, SmoothSpeed * Time.deltaTime);

    ////        OverheadCamera.transform.position = SmoothedPosition;
    ////        OverheadCamera.transform.Rotate(-90, 90, -90);
    ////        //OverheadCamera.transform.rotation = MainCamera.transform.rotation;

    ////        yield return new WaitForSeconds(WaitingTime);
    ////    }


}

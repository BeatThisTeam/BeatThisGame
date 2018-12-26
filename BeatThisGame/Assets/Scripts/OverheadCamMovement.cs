using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadCamMovement : MonoBehaviour
{

    public float SmoothSpeed;
    public float SmoothSpeedBack;
    public Transform OverheadPos;
    public Transform TopPos;
    public Transform MainPos;

    public void OverheadCamMove()
    { 
            StartCoroutine(OverheadCamCoroutine());
        //    StartCoroutine(OverheadCamBack());
        
    }

    public void OverheadCamMoveBack()
    {
        StartCoroutine(OverheadCamBack());
        //    StartCoroutine(OverheadCamBack());

    }


    public IEnumerator OverheadCamCoroutine()
    {
        while (true)
        {
            Vector3 SmoothedPosition = Vector3.Lerp(transform.position, TopPos.position, SmoothSpeed * Time.deltaTime);
            transform.position = SmoothedPosition;

            yield return null;
        }
       

    }

    public IEnumerator OverheadCamBack()
    {
        while (true)
        {
            Vector3 SmoothedPosition = Vector3.Lerp(transform.position, MainPos.position, SmoothSpeedBack * Time.deltaTime);
            transform.position = SmoothedPosition;
           

            yield return null;
        }
        
    }



}


//    public void MoveCamDown(Vector3 startpos, Vector3 endpos)
//    {
//        StartCoroutine(OverheadCamDown(startpos, endpos));
//    }
//    IEnumerator OverheadCamDown(Vector3 startpos, Vector3 endpos)
//    {
//        Vector3 SmoothedPosition = Vector3.Lerp(startpos, endpos, SmoothSpeed * Time.deltaTime);
//         = SmoothedPosition;
//        yield return new WaitForSeconds(WaitingTime);
//    }



//}



////    void FixedUpdate () {

////        Vector3 TopViewCoord;
////        TopViewCoord.x = 0;
////        TopViewCoord.y = 110;
////        TopViewCoord.z = 0;

////        StartCoroutine(OverheadCamMove(WaitingTime, SmoothSpeed, TopViewCoord));

////        //Vector3 TopViewCoord;
////        //TopViewCoord.x = 0;
////        //TopViewCoord.y = 110;
////        //TopViewCoord.z = 0;

////        //if(transform.position != TopViewCoord)
////        //{
////        //    Vector3 SmoothedPosition = Vector3.Lerp(transform.position, TopViewCoord, SmoothSpeed * Time.deltaTime);

////        //    transform.position = SmoothedPosition;
////        //}

////        //if(transform.position == TopViewCoord)
////        //{
////        //    Vector3 SmoothedPosition = Vector3.Lerp(transform.position, MainCamera.position, SmoothSpeed * Time.deltaTime);

////        //    transform.position = SmoothedPosition;
////        //}

////    }

////    private IEnumerator (OverheadCamMove(float WaitingTime, float SmoothSpeed, Vector3 TopViewCoord))
////    {
////        Vector3 SmoothedPosition = Vector3.Lerp(transform.position, TopViewCoord, SmoothSpeed * Time.deltaTime);
////        transform.position = SmoothedPosition;
////    }
////}


////Vector3 SmoothedPosition = Vector3.Lerp(transform.position, TopViewCoord, SmoothSpeed * Time.deltaTime);
////transform.position = SmoothedPosition;




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    Transform tr;

    private void Awake()
    {
        tr = GetComponent<Transform>();
    }


    public void MoveUpDown(Vector3 startPos, Vector3 endPos, float duration, float WaitingTime)
    {

        StartCoroutine(MoveUpDownCoroutine(startPos, endPos, duration, WaitingTime));
    }

    IEnumerator MoveUpDownCoroutine(Vector3 startPos, Vector3 endPos, float duration, float WaitingTime)
    {

       

        float tLerp = 0;

        while (tLerp <= duration)
        {
            tr.position = Vector3.Lerp(startPos, endPos, tLerp / duration);

            Vector3 lookAtPos = Vector3.zero - tr.position;
            lookAtPos.y = 0;
            

            transform.rotation = Quaternion.LookRotation(lookAtPos);
            
            tLerp += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds (WaitingTime);

        float t = 0;
        while (t <= duration)
        {
            tr.position = Vector3.Lerp(endPos, startPos, t / duration);
            t += Time.deltaTime;
           yield return null;
        }



        Destroy(this.gameObject);
    }
}


//Transform tr;

//private void Awake()
//{
//    tr = GetComponent<Transform>();
//}


//public void Move(Vector3 startPos, Vector3 endPos, float duration)
//{

//    StartCoroutine(MoveCoroutine(startPos, endPos, duration));
//}

//IEnumerator MoveCoroutine(Vector3 startPos, Vector3 endPos, float duration)
//{

//    float tLerp = 0;

//    while (tLerp <= duration)
//    {
//        tr.position = Vector3.Lerp(startPos, endPos, tLerp / duration);
//        tLerp += Time.deltaTime;
//        yield return null;
//    }

//    Destroy(this.gameObject);
//}
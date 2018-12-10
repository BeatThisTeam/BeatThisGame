using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    Transform tr;

    private void Awake(){
        tr = GetComponent<Transform>();
    }

    public void MoveUpDown(Vector3 startPos, Vector3 endPos, float duration){

        Vector3 lookAtPos = Vector3.zero - tr.position;
        lookAtPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookAtPos);
        StartCoroutine(MoveUpDownCoroutine(startPos, endPos, duration));
    }

    IEnumerator MoveUpDownCoroutine(Vector3 startPos, Vector3 endPos, float duration){
    
        float tLerp = 0;
        
        while (tLerp <= duration){
            tr.position = Vector3.Lerp(startPos, endPos, tLerp / duration);           
            tLerp += Time.deltaTime;
            yield return null;
        }
    }
}
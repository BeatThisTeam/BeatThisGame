using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    Transform tr;

    private Animator bossAnim;

    public Transform player;

    private void Awake() {

        tr = GetComponent<Transform>();
        bossAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("AttackCircle")) {
            ScenePrototypeManager.Instance.GetComponent<SpecialAttack>().BossHit();
        }
    }

    public void print() {
        Debug.Log("beat " + SongManager.Instance.SongPositionInBeats);
        Debug.Log("sec " + SongManager.Instance.SongPositionInSeconds);
    }

    public void StartIdle() {

        bossAnim.SetBool("Start", true);
        StartCoroutine(IdleAnimSync());
    }

    public void StartSlam(float duration) {

        bossAnim.SetFloat("slamTime", 0f);
        bossAnim.SetBool("return", false);
        bossAnim.SetBool("Start", false);
        bossAnim.SetBool("slam",true);
        StopCoroutine(IdleAnimSync());
        StartCoroutine(SlamAnimSync(duration));
    }

    public void StartReturn() {

        bossAnim.SetBool("slam", false);
        bossAnim.SetBool("return", true);
    }

    private IEnumerator IdleAnimSync() {
        while (true) {
            float songBeat = SongManager.Instance.SongPositionInBeats;
            float animTime = songBeat - Mathf.FloorToInt(songBeat);
            bossAnim.Play(0, -1, animTime);
            yield return null;
        }      
    }

    IEnumerator SlamAnimSync(float duration) {

        float tLerp = 0;
        float animTime = 0;

        while (animTime <= duration) {           
            bossAnim.SetFloat("slamTime", animTime);
            animTime += Time.deltaTime;
            yield return null;
        }
    }
}

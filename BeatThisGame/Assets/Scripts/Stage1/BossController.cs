using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    Transform tr;

    private Animator bossAnim;

    public Transform player;

    public Material damagedMaterial;

    public GameObject rejectPlayerComponent;

    private bool damageable = true;

    private Vector3 lookAtPos;
    public float rotationSpeed;
    private bool canRotate = true;

    private void Awake() {

        tr = GetComponent<Transform>();
        bossAnim = GetComponent<Animator>();
    }

    private void Update() {
        lookAtPos = player.position - transform.position;
        lookAtPos.y = 0;
        //transform.rotation = Quaternion.LookRotation(lookAtPos);
    }

    private void FixedUpdate() {

        if (canRotate) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookAtPos), rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("AttackCircle")) {
            if (damageable) {
                ScenePrototypeManager.Instance.GetComponent<SpecialAttack>().BossHit();
                ScoreManager.Instance.UpdateBossHealth();
                SoundManager.Instance.PlayBossDamageSound();
                StartCoroutine(PlayDamageAnimation(0.2f));
            }
        }

        if (other.CompareTag("Projectile")) {
            if (damageable && other.GetComponent<Projectile>().rejected) {
                ScoreManager.Instance.UpdateBossHealth(other.GetComponent<Projectile>().rejectAccuracy);
                SoundManager.Instance.PlayBossDamageSound();
                StartCoroutine(PlayDamageAnimation(0.2f));
            }            
        }
    }

    private IEnumerator PlayDamageAnimation(float duration) {

        damageable = false;
        float stateDuration = 0.1f;
        float timer = 0f;

        Material nextMaterial;
        Material defaultMat;
        Material temp;
        Renderer renderer;
        renderer = GetComponentInChildren<Renderer>();
        nextMaterial = damagedMaterial;
        defaultMat = renderer.material;

        while (timer < duration) {
            temp = renderer.material;
            renderer.material = nextMaterial;
            nextMaterial = temp;
            timer += stateDuration;
            yield return new WaitForSeconds(stateDuration);
        }
        renderer.material = defaultMat;
        damageable = true;
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

        canRotate = false;
        bossAnim.SetFloat("slamTime", 0f);
        bossAnim.SetBool("return", false);
        bossAnim.SetBool("Start", false);
        bossAnim.SetBool("slam",true);
        StopCoroutine(IdleAnimSync());
        StartCoroutine(SlamAnimSync(duration));
    }

    public void StartReturn() {

        canRotate = true;
        rejectPlayerComponent.SetActive(false);
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

        while (tLerp <= duration) {
            animTime = Mathf.Lerp(0, 1, tLerp / duration);
            bossAnim.SetFloat("slamTime", animTime);
            tLerp += Time.deltaTime;
            yield return null;
        }

        rejectPlayerComponent.SetActive(true);
    }
}

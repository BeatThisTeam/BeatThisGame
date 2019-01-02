using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum Direction { Up, Down, Left, Right };

    public float maxHealth;
    public float health;

    public GroundSections ground;

    public int faceIndex;
    public int ringIndex;
    Direction dir;
    public Direction Dir { get { return dir; } }

    private Animator anim;
    private Transform tr;
    public Renderer rend;

    public string teleportShader;

    public PlayerHealth characterHealthBarUI;


    public UpDownCam cam;

    public GameObject body;

    private bool damageable = true;
    private Shield shield;

    private bool enableVerticalMovement;
    private bool axisInUse = false;
    private float horizAxisInput;
    private float vertAxisInput;

    public void Setup() {

        health = maxHealth;
        characterHealthBarUI.Setup(maxHealth);
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        shield = GetComponent<Shield>();
        //rend = GetComponentInChildren<Renderer>();
        //rend.material.shader = Shader.Find(teleportShader);

        if (ground.rings.Count > 1) {
            enableVerticalMovement = true;
        } else {
            enableVerticalMovement = false;
        }
    }

    private void Update() {

        horizAxisInput = Input.GetAxisRaw("Horizontal");
        vertAxisInput = Input.GetAxisRaw("Vertical");
        
        if(horizAxisInput != 0 || vertAxisInput != 0) {

            if (!axisInUse) {

                axisInUse = true;

                if(horizAxisInput > 0) {
                    ScoreManager.Instance.HitNote(faceIndex, ringIndex);
                    SoundManager.Instance.PlayMoveSound();
                    //StartCoroutine("FadeOut");
                    faceIndex = (faceIndex + 1) % ground.rings[ringIndex].sections.Count;
                    dir = Direction.Right;
                }

                if(horizAxisInput < 0) {
                    ScoreManager.Instance.HitNote(faceIndex, ringIndex);
                    SoundManager.Instance.PlayMoveSound();
                    //StartCoroutine("FadeOut");
                    faceIndex--;
                    dir = Direction.Left;
                    if (faceIndex < 0) {
                        faceIndex = ground.rings[ringIndex].sections.Count - 1;
                    }
                }

                if(vertAxisInput > 0 && enableVerticalMovement) {
                    ScoreManager.Instance.HitNote(faceIndex, ringIndex);
                    SoundManager.Instance.PlayMoveSound();
                    //StartCoroutine("FadeOut");
                    ringIndex--;
                    dir = Direction.Up;
                    if (ringIndex < 0) {
                        ringIndex = ground.rings.Count - 1;
                    }
                }

                if(vertAxisInput < 0 && enableVerticalMovement) {
                    ScoreManager.Instance.HitNote(faceIndex, ringIndex);
                    SoundManager.Instance.PlayMoveSound();
                    //StartCoroutine("FadeOut");
                    ringIndex = (ringIndex + 1) % ground.rings.Count;
                    dir = Direction.Down;
                }
            }
        }

        if (horizAxisInput == 0 && vertAxisInput == 0) {

            axisInUse = false;
        }

        if (Input.GetButtonDown("Shield")) {
            ScoreManager.Instance.HitNote(faceIndex, ringIndex);
            SoundManager.Instance.PlayShieldSound();
            shield.ActivateShield();
        }
        
        tr.position = ground.rings[ringIndex].sections[faceIndex].tr.position;
        Vector3 lookAtPos = Vector3.zero - tr.position;
        lookAtPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookAtPos);
    }

    public void Damage(float damage) {

        if (damageable) {
            health -= damage;
            characterHealthBarUI.UpdateBar(health);
            StartCoroutine(PlayDamageAnimation(0.5f));
            SoundManager.Instance.PlayCharacterDamageSound();
        }
    }

    private IEnumerator PlayDamageAnimation(float duration) {

        damageable = false;
        float stateDuration = 0.1f;
        float timer = 0f;
        
        while (timer < duration) {
            body.SetActive(!body.activeSelf);
            timer += stateDuration;
            yield return new WaitForSeconds(stateDuration);
        }
        body.SetActive(true);
        damageable = true;
    }

    //private IEnumerator FadeOut() {

    //    float start = -2f;
    //    float finish = 10f;

    //    float tLerp = 0f;
    //    float duration = 0.2f;

    //    while (tLerp <= duration) {
    //        rend.material.SetFloat("_TransitionLevel", Mathf.Lerp(start, finish, tLerp/duration));
    //        tLerp += Time.deltaTime;

    //        yield return null;
    //    }
    //    rend.material.SetFloat("_TransitionLevel", 0);
    //}
}

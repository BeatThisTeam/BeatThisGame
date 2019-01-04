using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum Direction { Up, Down, Left, Right };

    public bool isAlive = true;
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
    public Material[] materials;

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
        materials = body.GetComponent<Renderer>().materials;
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
                StartCoroutine(FadeIn(0.4f));

                if (horizAxisInput > 0) {
                    ScoreManager.Instance.HitNote(faceIndex, ringIndex);
                    SoundManager.Instance.PlayMoveSound();
                    faceIndex = (faceIndex + 1) % ground.rings[ringIndex].sections.Count;
                    dir = Direction.Right;
                }

                if(horizAxisInput < 0) {
                    ScoreManager.Instance.HitNote(faceIndex, ringIndex);
                    SoundManager.Instance.PlayMoveSound();
                    faceIndex--;
                    dir = Direction.Left;
                    if (faceIndex < 0) {
                        faceIndex = ground.rings[ringIndex].sections.Count - 1;
                    }
                }

                if(vertAxisInput > 0 && enableVerticalMovement) {
                    ScoreManager.Instance.HitNote(faceIndex, ringIndex);
                    SoundManager.Instance.PlayMoveSound();
                    ringIndex--;
                    dir = Direction.Up;
                    if (ringIndex < 0) {
                        ringIndex = ground.rings.Count - 1;
                    }
                }

                if(vertAxisInput < 0 && enableVerticalMovement) {
                    ScoreManager.Instance.HitNote(faceIndex, ringIndex);
                    SoundManager.Instance.PlayMoveSound();
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
            PlayAttackAnimation();
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
            SoundManager.Instance.PlayCharacterDamageSound();
            characterHealthBarUI.UpdateBar(health);
            if (health <= 0) {
                StartCoroutine(PlayDeathAnimation(2f));
            } else {
                StartCoroutine(PlayDamageAnimation(0.5f));
            }           
                    
        }        
    }

    public void PlayAttackAnimation() {

        anim.SetTrigger("Attack");
    }

    private IEnumerator PlayDeathAnimation(float duration) {
        
        damageable = false;
        anim.SetBool("isDead", true);

        float i = 0;
        float tLerp = 0f;

        while (tLerp <= duration) {
            foreach (Material material in materials) {
                i = Mathf.Lerp(0, 1f, tLerp/duration);
                material.SetFloat("Vector1_D4B3BD5A", i);
                material.SetFloat("Vector1_DC68DE65", i);
                tLerp += Time.deltaTime;
                //Debug.Log("i is " + i);
                yield return null;
            }
        }

        isAlive = false;
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

    private IEnumerator FadeIn(float duration) {
        

        float i = 0;
        float tLerp = 0f;

        while (tLerp <= duration) {
            foreach (Material material in materials) {
                i = Mathf.Lerp(1, 0f, tLerp / duration);
                material.SetFloat("Vector1_DC68DE65", i);
                tLerp += Time.deltaTime;
                yield return null;
            }
        }
        materials[0].SetFloat("Vector1_DC68DE65", 0);
        materials[1].SetFloat("Vector1_DC68DE65", 0);
        materials[2].SetFloat("Vector1_DC68DE65", 0);
    }

    public void SetDir(Direction direction) {

        dir = direction;
    }
}

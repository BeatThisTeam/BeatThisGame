using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour {

    [SerializeField]
    public GroundSections ground;
    public GroundColorChanger gcc;
    public Transform player;
    public PlayerController playerCtrl;
    public Transform boss;

    public abstract void StartAttack(float duration);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

    public float velocity;
    public Vector3 spawnPos;
    public Vector3 removePos;
    public float timeOfSpawning;
    public float timeOfThisNote;
    private Transform tr;

    void Awake() {
        spawnPos = transform.position;
        removePos = new Vector3(0, -3, 0);
        timeOfSpawning = SongManager.Instance.SongPositionInSeconds;
        timeOfThisNote = Scene2Manager.Instance.noteToPlayInSeconds;
        tr = gameObject.GetComponent<Transform>();
    }

    private void FixedUpdate() {

        tr.position = tr.position - tr.up * velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(this.gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejectPlayer : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {

            PlayerController player = other.GetComponent<PlayerController>();

            player.Damage(20f);

            if(player.Dir == PlayerController.Direction.Left) {

                player.faceIndex = (player.faceIndex + 9 + 1) % 9;
                player.SetDir(PlayerController.Direction.Right);

            } else if(player.Dir == PlayerController.Direction.Right){

                player.faceIndex = (player.faceIndex + 9 - 1) % 9;
                player.SetDir(PlayerController.Direction.Left);
            }
        }
    }
}

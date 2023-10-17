using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    public void resumeGame() {
        GameObject player = GameObject.Find("Player(Clone)");
        Player1_Moveset moves = player.GetComponent<Player1_Moveset>();

        moves.OnMenuOpenClose();

    }
}

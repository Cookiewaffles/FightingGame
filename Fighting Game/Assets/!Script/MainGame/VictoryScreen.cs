using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VictoryScreen : MonoBehaviour
{
    public GameObject player1WinText;
    public GameObject player2WinText;


    public GameObject buttonPLayer1;
    public GameObject buttonPLayer2;

    // Start is called before the first frame update
    void Start()
    {
        player1WinText.SetActive(false);
        player2WinText.SetActive(false);
    }

    public void playerWin(int player) {



        if (player == 1)
        {
            player1WinText.SetActive(true);
            player2WinText.SetActive(false);

            EventSystem.current.SetSelectedGameObject(buttonPLayer1);
        }
        else if(player == 2) {
            player1WinText.SetActive(false);
            player2WinText.SetActive(true);
            EventSystem.current.SetSelectedGameObject(buttonPLayer2);

        }

    }
}

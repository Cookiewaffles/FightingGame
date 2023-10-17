using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeMenu : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject player1MainMenu;


    // Start is called before the first frame update
    void Start()
    {
        player1MainMenu.SetActive(false);
    }

    //Pause/Unpause
    public void Pause(int player)
    {
        if (player == 1)
        {
            player1MainMenu.SetActive(true);
        }
        else if (player == 2)
        {
            player1MainMenu.SetActive(false);
        }
    }

    public void Unpause(int player)
    {
        if (player == 1)
        {
            player1MainMenu.SetActive(false);
        }
        else if (player == 2)
        {
            player1MainMenu.SetActive(false);
        }
    }
}

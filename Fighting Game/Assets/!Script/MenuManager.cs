using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject player1MainMenu;
    [SerializeField] private GameObject player2MainMenu;

    [Header("First Selection")]
    [SerializeField] private GameObject player1Button;
    [SerializeField] private GameObject player2Button;

    //During the Game
    void Start()
    {
        player1MainMenu.SetActive(false);
        player2MainMenu.SetActive(false);
    }


    //Pause/Unpause
    public void Pause(int player) {
        if (player == 1) {
            player1MainMenu.SetActive(true);
            player2MainMenu.SetActive(false);

            EventSystem.current.SetSelectedGameObject(player1Button);
        }
        else if (player == 2) {
            player1MainMenu.SetActive(false);
            player2MainMenu.SetActive(true);

            EventSystem.current.SetSelectedGameObject(player2Button);
        }
    }

    public void Unpause(int player)
    {
        if (player == 1)
        {
            player1MainMenu.SetActive(false);
            player2MainMenu.SetActive(false);
        }
        else if (player == 2)
        {
            player1MainMenu.SetActive(false);
            player2MainMenu.SetActive(false);
        }

        EventSystem.current.SetSelectedGameObject(null);
    }

    //Settings Menu
    public void OnQuitPres() {
        SceneManager.LoadScene(0);
    }
}

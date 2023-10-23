using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("PlayerJoin")]
    [SerializeField] private GameObject playerJoinedMenu;

    [Header("Menu Objects")]
    [SerializeField] private GameObject player1MainMenu;
    [SerializeField] private GameObject player2MainMenu;

    [Header("Audio Menu Objects")]
    [SerializeField] private GameObject player1AudioMenu;
    [SerializeField] private GameObject player2AudioMenu;

    [Header("First Selection")]
    [SerializeField] private GameObject player1Button;
    [SerializeField] private GameObject player2Button;
    [SerializeField] private GameObject player1AudioButton;
    [SerializeField] private GameObject player2AudioButton;

    //During the Game
    void Start()
    {
        playerJoinedMenu.SetActive(true);

        player1MainMenu.SetActive(false);
        player2MainMenu.SetActive(false);

        player1AudioMenu.SetActive(false);
        player2AudioMenu.SetActive(false);
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

    //audio settings
    //open
    public void player1AudioOpen() {
        player1MainMenu.SetActive(false);

        player1AudioMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(player1AudioButton);
    }

    public void player2AudioOpen() {
        player2MainMenu.SetActive(false);

        player2AudioMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(player2AudioButton);
    }

    //close
    public void player1AudioClose()
    {
        player1MainMenu.SetActive(true);

        player1AudioMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(player1Button);
    }

    public void player2AudioClose()
    {
        player2MainMenu.SetActive(true);

        player2AudioMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(player2Button);
    }


    //Settings Menu
    public void OnQuitPres() {
        SceneManager.LoadScene(0);
    }
}

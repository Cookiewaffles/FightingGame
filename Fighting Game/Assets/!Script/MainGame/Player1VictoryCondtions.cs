using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player1VictoryCondtions : MonoBehaviour
{
    public Image playerHP;
    public Image playerGuage1;
    public Image playerGuage2;

    public bool restart;
    public bool round1 = true;

    int bestof;


    // Start is called before the first frame update
    void Start()
    {
        BindObjects();

        bestof = PlayerPrefs.GetInt("bestof");

        restart = false;
        playerGuage1.color = Color.black;
        playerGuage2.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (restart == true) {
            if (bestof == 2) {
                playerHP.fillAmount = 1;
            }
            restart = false; 
        }
 
        if (playerHP.fillAmount == 0 && playerGuage1.color == Color.black) { 
            Round1Win();
        }

        if (playerHP.fillAmount == 0 && playerGuage1.color == Color.red && playerGuage2.color == Color.black && restart == false)
        {
            Round2Win();
        }

        if (playerGuage1.color == Color.red && playerGuage2.color == Color.red) { 
            PlayerWin();
        }


    }

    private void Round1Win()
    {
        playerGuage1.color = Color.red;
        restart = true;
    }


    private void Round2Win()
    {
        playerGuage2.color = Color.red;
    }

    private void PlayerWin()
    {
        Time.timeScale = 0;

        VictoryScreen vs = GameObject.Find("VictoryManager").GetComponent<VictoryScreen>();

        vs.playerWin(1);
    }

    public void BindObjects()
    {

        GameObject placeholder;
        int num = PlayerPrefs.GetInt("bestof");

        if (num == 1)
        {
            placeholder = GameObject.Find("Player2_hp");
            playerHP = placeholder.GetComponent<Image>();

            placeholder = GameObject.Find("Player 1 - Light 1");
            playerGuage1 = placeholder.GetComponent<Image>();

            placeholder = GameObject.Find("Player 1 - Light 1");
            playerGuage2 = placeholder.GetComponent<Image>();            
        }
        else
        {

            placeholder = GameObject.Find("Player2_hp");
            playerHP = placeholder.GetComponent<Image>();

            placeholder = GameObject.Find("Player 1 - Light 1");
            playerGuage1 = placeholder.GetComponent<Image>();

            placeholder = GameObject.Find("Player 1 - Light 2");
            playerGuage2 = placeholder.GetComponent<Image>();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MatchData : MonoBehaviour { 

public Image playerHP;
public Image playerGuage1;
public Image playerGuage2;

public bool restart;

// Start is called before the first frame update
void Start()
{

    restart = false;
    playerGuage1.color = Color.black;
    playerGuage1.color = Color.black;
}

// Update is called once per frame
void Update()
{

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
}

public void BindObjects(string name, string name2)
{
    GameObject placeholder;
    int num = PlayerPrefs.GetInt("bestof");

    if (num == 1)
    {
        placeholder = GameObject.Find("Player1_hp");
        playerHP = placeholder.GetComponent<Image>();

        placeholder = GameObject.Find("Player 2 - Light 1");
        playerGuage1 = placeholder.GetComponent<Image>();

        placeholder = GameObject.Find("Player 2 - Light 1");
        playerGuage2 = placeholder.GetComponent<Image>();

        placeholder = GameObject.Find("Player 2 Wins");
    }
    else
    {
        placeholder = GameObject.Find("Player1_hp");
        playerHP = placeholder.GetComponent<Image>();

        placeholder = GameObject.Find("Player 2 - Light 1");
        playerGuage1 = placeholder.GetComponent<Image>();

        placeholder = GameObject.Find("Player 2 - Light 2");
        playerGuage2 = placeholder.GetComponent<Image>();

        placeholder = GameObject.Find("Player 2 Wins");
    }
}
}

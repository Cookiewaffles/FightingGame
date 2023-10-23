using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Restart time
        Time.timeScale = 1;
    }

    public void SceneChange(int sceneID) {
        SceneManager.LoadScene(sceneID);
    }

    public void ExitApplication() { 
        Application.Quit();
    }
}

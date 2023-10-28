using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSizer : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject player1C;
    // Start is called before the first frame update
    void Start()
    {
        mainCanvas.GetComponent<Canvas>().GetComponent<CanvasScaler>().referenceResolution =
            new Vector2(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"));

        player1C.GetComponent<Canvas>().GetComponent<CanvasScaler>().referenceResolution =
            new Vector2(PlayerPrefs.GetInt("width"), PlayerPrefs.GetInt("height"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

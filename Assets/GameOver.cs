using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text gameover = GameObject.FindGameObjectWithTag("GameOverButton").GetComponent<Text>();

        gameover.text = "Score: " + PlayerPrefs.GetString("score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

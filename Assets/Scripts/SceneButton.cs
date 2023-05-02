using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    //public Scene nextLevel;
    // Start is called before the first frame update

    public void playButton()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void creditsButton(){
        SceneManager.LoadScene("Credits");
    }
    public void playAgain(){
        SceneManager.LoadScene("title_scene");
    }
}

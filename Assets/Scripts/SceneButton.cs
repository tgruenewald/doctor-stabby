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
        Debug.Log("play");
        SceneManager.LoadScene(2);
    }
    public void creditsButton(){
        Debug.Log("credits");
        SceneManager.LoadScene(3);
    }

}

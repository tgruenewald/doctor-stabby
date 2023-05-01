using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public Scene nextLevel;
    // Start is called before the first frame update
    [Header("Mouse Over Sprite")]
    public Sprite mouseOver;
    [Header("Mouse Moves Off Sprite")]
    public Sprite mouseOff;
    private SpriteRenderer spriteR;
    // Update is called once per frame
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        SceneManager.LoadScene(nextLevel.buildIndex);
    }

    void OnMouseOver(){
        spriteR.sprite = mouseOver;
    }
    void OnMouseExit(){
        spriteR.sprite = mouseOff;
    }
}

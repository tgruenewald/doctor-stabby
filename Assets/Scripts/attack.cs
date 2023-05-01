using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject painBox;
    public float leftOffset;
    public float rightOffset;
    public float upOffset;
    public float downOffet;
    private enum FacingDirection 
    {
    Up,
    Down,
    Left,
    Right
    }
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    FacingDirection cd = FacingDirection.Up;
    void Update()
    {
        
        //update the current direction var
        if (Input.GetKeyDown(KeyCode.W))
        {
            cd = FacingDirection.Up;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            cd = FacingDirection.Left;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            cd = FacingDirection.Down;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            cd = FacingDirection.Right;
        }

        //call attack function when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack(cd);
        }
        

    }
    void Attack(FacingDirection cd){
        //creates an object near player location that's hit box will hurt zombies
        //direction of object depends on facing direction
        Debug.Log(cd);
        //if space is pushed
        //spawn pain box in current direction
        float playerX = transform.position.x;
        float playerY = transform.position.y;
        switch(cd) 
        {
        case FacingDirection.Up:
            Debug.Log("got into up, player box should go to " + playerX + " " + transform.position.y + upOffset);
            Quaternion quat = new Quaternion(0,0 ,0, 0);
            Instantiate(painBox, new Vector3(playerX, transform.position.y + upOffset, 0),  Quaternion.Euler(new Vector3(0, 0, 90)));
            break;
        case FacingDirection.Down:
            Debug.Log("got into down, player box should go to " + playerX + " " + transform.position.y + downOffet);
            Instantiate(painBox, new Vector3(playerX, transform.position.y + downOffet, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
            break;
        case FacingDirection.Left:
            Debug.Log("got into left, player box should go to " + transform.position.x + leftOffset + " " + playerY);
            Instantiate(painBox, new Vector3(transform.position.x + leftOffset, playerY, 0), Quaternion.identity);
            break;
        case FacingDirection.Right:
            Debug.Log("got into right, player box should go to " + transform.position.x + rightOffset + " " + playerY);
            Instantiate(painBox, new Vector3(transform.position.x + rightOffset, playerY, 0), Quaternion.identity);
            
            break;
        }
    }
    void destroyPainBox(){
        //destroy box when not using it
    }
}

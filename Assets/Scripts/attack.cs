using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject painBox;
    public float leftOffset = 0;
    public float rightOffset = 0;
    public float upOffset = 0;
    public float downOffet = 0;
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
    void Update()
    {
        FacingDirection cd = FacingDirection.Up;
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

        //if space is pushed
        //spawn pain box in current direction
        float playerX = transform.position.x;
        float playerY = transform.position.y;
        switch(cd) 
        {
        case FacingDirection.Up:
            Instantiate(painBox, new Vector3(playerX, transform.position.y + upOffset, 0), Quaternion.identity);
            break;
        case FacingDirection.Down:
            Instantiate(painBox, new Vector3(playerX, transform.position.y + downOffet, 0), Quaternion.identity);
            break;
        case FacingDirection.Left:
            Instantiate(painBox, new Vector3(transform.position.x + leftOffset, playerY, 0), Quaternion.identity);
            break;
        case FacingDirection.Right:
            Instantiate(painBox, new Vector3(transform.position.x + rightOffset, playerY, 0), Quaternion.identity);
            break;
        }
    }
    void destroyPainBox(){

    }
}

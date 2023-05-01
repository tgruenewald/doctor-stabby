using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
enum FacingDirection 
{
  Up,
  Down,
  Left,
  Right
}
FacingDirection currentDirection = FacingDirection.Left; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Attack(){
        //creates an object near player location that's hit box will hurt zombies
        //direction of object depends on facing direction
    }
}

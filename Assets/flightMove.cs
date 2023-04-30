using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flightMove : MonoBehaviour
{
    Rigidbody2D mrig;
    Transform mtrans;
    public bool tracking = false;
    public float speed;
    public float speedx;
    public float speedy;
    Vector2 vel;
    Vector2 velx;
    Vector2 vely;

    public float distToPlayer;
    public GameObject Player;
    public float area;
    public bool isRight;
    public bool isUp;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        mrig = GetComponent<Rigidbody2D>();
        mtrans = GetComponent<Transform>();
        vel = new Vector2(speedx, speedy);
       

    }

    // Update is called once per frame
    void Update()
    {
        vel = new Vector2(speedx, speedy);
        distToPlayer = Mathf.Sqrt((Player.GetComponent<Transform>().position.x - mtrans.position.x) * (Player.GetComponent<Transform>().position.x - mtrans.position.x) + (Player.GetComponent<Transform>().position.y - mtrans.position.y) * (Player.GetComponent<Transform>().position.y - mtrans.position.y));
        if (distToPlayer <= area)
        {

            tracking = true;
            if (Player.GetComponent<Transform>().position.x < mtrans.position.x)
            {
                //checks if player is left of zombie
                isRight = false;
                speedx = -speed;
                //mrig.MovePosition(mrig.position + (1.0f * vel * Time.deltaTime));
                //Debug.Log("left");

            }
            else if (Player.GetComponent<Transform>().position.x > mtrans.position.x)
            {
                //checks if player is right of zombie

                isRight = true;
                speedx = speed;
                //mrig.MovePosition(mrig.position + (1.0f * vel * Time.deltaTime));
                //Debug.Log("right");

            }
            else
            {
                //player and zombie have same x
                speedx = 0;
            }
            if (Player.GetComponent<Transform>().position.y < mtrans.position.y)
            {
                //if player is below zombie
                isUp = false;
                speedy = -speed;

            }
            else if (Player.GetComponent<Transform>().position.y > mtrans.position.y)
            {
                //if player is above zombie
                isUp = true;
                speedy = speed;

            }
            else
            {
                //player and zombie have the same y
                speedy = 0;
            }
        }
        else
        {
            tracking = false;
            speedx = 0.0f;
            speedy = 0.0f;
        }
        if (isRight)
        {
            mrig.MovePosition(mrig.position + (1.0f * vel * Time.deltaTime));
           // Debug.Log("right");
        }
        if (!isRight)
        {
           mrig.MovePosition(mrig.position + (1.0f * vel * Time.deltaTime));
           // Debug.Log("left");
        }
        if (isUp)
        {
           mrig.MovePosition(mrig.position + (1.0f * vel * Time.deltaTime));
           // Debug.Log("Up");
        }
        if (!isUp)
        {
           mrig.MovePosition(mrig.position + (1.0f * vel * Time.deltaTime));
           // Debug.Log("down");
        }
    }
}

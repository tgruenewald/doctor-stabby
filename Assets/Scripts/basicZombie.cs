using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicZombie : MonoBehaviour
{
    public float distToPlayer;
    public GameObject player;
    public float speed;
    public float distance;
    public bool isRight;
    public float holdXb;
    public float holdXf;
    Rigidbody2D mrig;
    Transform mtrans;
    Vector2 vel;
    public bool tracking = false;
    public float area;

    // Start is called before the first frame update
    void Start()
    {
        mrig = GetComponent<Rigidbody2D>();
        mtrans = GetComponent<Transform>();
        vel = new Vector2(speed, 0.0f);
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        holdXb = mtrans.position.x - distance;
        holdXf = mtrans.position.x + distance;
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            distToPlayer = Mathf.Sqrt(
                (player.GetComponent<Transform>().position.x - mtrans.position.x) 
                * (player.GetComponent<Transform>().position.x - mtrans.position.x) 
                + (player.GetComponent<Transform>().position.y - mtrans.position.y) 
                * (player.GetComponent<Transform>().position.y - mtrans.position.y));
        }

        if (isRight && mtrans.position.x < holdXf)
        {
            mrig.MovePosition(mrig.position + (1.0f * vel * Time.deltaTime));
            Debug.Log("right");
        }
        if(mtrans.position.x <= holdXb && !tracking)
        {
            isRight = true;
        }
        if (mtrans.position.x >= holdXf && !tracking)
        {
            isRight = false;
        }
        if (!isRight && mtrans.position.x > holdXb)
        {
            mrig.MovePosition(mrig.position - (1.0f * vel * Time.deltaTime));
            Debug.Log("left");
        }
        if(distToPlayer <= area)
        {
  
            tracking = true;
            if (player != null && player.GetComponent<Transform>().position.x < mtrans.position.x)
            {
                isRight = false;
                
            }
            if (player != null && player.GetComponent<Transform>().position.x > mtrans.position.x)
            {
                isRight = true;
                
            }
        }
        else
        {
            tracking = false;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit");
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if(playerController != null)
            {
                playerController.health -= 1;
            }
            SetSpeed();
            Invoke("SetSpeed", 2f);
        }
    }
    void SetSpeed()
    {
        speed = -speed;
    }
}

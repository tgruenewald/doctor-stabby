using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicZombie : MonoBehaviour
{
    public float distToPlayer;
    public GameObject Player;
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
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        mrig = GetComponent<Rigidbody2D>();
        mtrans = GetComponent<Transform>();
        vel = new Vector2(speed, 0.0f);
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        holdXb = mtrans.position.x - distance;
        holdXf = mtrans.position.x + distance;
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Mathf.Sqrt((Player.GetComponent<Transform>().position.x - mtrans.position.x) * (Player.GetComponent<Transform>().position.x - mtrans.position.x) + (Player.GetComponent<Transform>().position.y - mtrans.position.y) * (Player.GetComponent<Transform>().position.y - mtrans.position.y));
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
            if (Player.GetComponent<Transform>().position.x < mtrans.position.x)
            {
                isRight = false;
                
            }
            if (Player.GetComponent<Transform>().position.x > mtrans.position.x)
            {
                isRight = true;
                
            }
        }
        else
        {
            tracking = false;
        }
        if(health <= 0)
        {
            GameObject.Destroy(gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit");
            collision.gameObject.GetComponent<PlayerController>().health -= 1;
            SetSpeed();
            Invoke("SetSpeed", 2f);
        }
    }
    void SetSpeed()
    {
        speed = -speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightMove : MonoBehaviour
{
    public float distToPlayer;
    public GameObject Player;
    public float speed;
    public float speedx;
    public float speedy;
    Rigidbody2D mrig;
    Transform mtrans;
    Vector2 vel;
    public float area;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        mrig = GetComponent<Rigidbody2D>();
        mtrans = GetComponent<Transform>();
        vel = new Vector2(speedx, speedy);
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        vel = new Vector2(speedx, speedy);
        distToPlayer = Mathf.Sqrt((Player.GetComponent<Transform>().position.x - mtrans.position.x) * (Player.GetComponent<Transform>().position.x - mtrans.position.x) + ((Player.GetComponent<Transform>().position.y - mtrans.position.y) * (Player.GetComponent<Transform>().position.y - mtrans.position.y)));
        if (distToPlayer <= area) 
        {
            mrig.MovePosition(mrig.position + (1.0f * vel * Time.deltaTime));
            if (Player.GetComponent<Transform>().position.x < mtrans.position.x)
            {
                speedx = -speed;

            }
            else if (Player.GetComponent<Transform>().position.x > mtrans.position.x)
            {
                speedx = speed;

            }
            else
            {
                speedx = 0.0f;
            }

            if (Player.GetComponent<Transform>().position.y < mtrans.position.y)

            {
                speedy = -speed;

            }

            else if (Player.GetComponent<Transform>().position.y > mtrans.position.y)

            {
                speedy = speed;

            }
            else
            {
                speedy = 0.0f;
            }
        }
        else 
        {
            speedx = 0.0f;
            speedy = 0.0f;
        }
        if (health <= 0)
        {
            GameObject.Destroy(gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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

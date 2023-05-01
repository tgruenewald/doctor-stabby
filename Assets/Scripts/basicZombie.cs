using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    float knockBackForce = 30f;
    bool knockBackMode = false;
    public int health = 1;

    // Start is called before the first frame update
    void Start()
    {
        mrig = GetComponent<Rigidbody2D>();
        mtrans = GetComponent<Transform>();
        vel = new Vector2(speed, 0.0f);
        player = GameObject.FindGameObjectWithTag("Player");
        holdXb = mtrans.position.x - distance;
        holdXf = mtrans.position.x + distance;
    }


    void FixedUpdate()
    {
        if (!knockBackMode) 
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
            if (mtrans.position.x <= holdXb && !tracking)
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
            if (distToPlayer <= area)
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
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if(playerController != null)
            {
                // playerController.health -= 1;
            }
            
            float direction = 1f;
            if (isRight)
            {
                direction = -1f;
            }
            print("knockback: " + direction);
            mrig.AddForce(new Vector2(direction * knockBackForce, 0f), ForceMode2D.Impulse);
            knockBackMode = true;
            GetComponent<AudioSource>().Play();
            StartCoroutine(TimerCoroutine());
            //SetSpeed();
            // Invoke("SetSpeed", 2f);

        }
    }

    IEnumerator TimerCoroutine()
    {
        
        yield return new WaitForSeconds(0.5f);
        knockBackMode = false;
    }
    void SetSpeed()
    {
        speed = -speed;
    }
}

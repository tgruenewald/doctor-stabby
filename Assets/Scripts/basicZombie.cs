using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

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
    float knockBackForce = 20f;
    bool knockBackMode = false;
    public int health = 3;
    private bool facingRight = false;
    Animator animator;
    [Header("Default Score")]
    public int score = 0;
    [Header("Text Object for Displaying Score")]
    public Text scoreText;
    bool youDied = false;

    public bool isDead { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;   
        mrig = GetComponent<Rigidbody2D>();
        mtrans = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        vel = new Vector2(speed, 0.0f);
        player = GameObject.FindGameObjectWithTag("Player");
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        holdXb = mtrans.position.x - distance;
        holdXf = mtrans.position.x + distance;

    }


    void FixedUpdate()
    {
        if (!knockBackMode && !isDead) 
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

            if (isRight && !facingRight)
            {
                Flip();
            }
            if (!isRight && facingRight)
            {
                Flip();
            }

        }
        if(health <= 0 && !youDied)
        {
            youDied = true;
            AddScore(5);
            ZombieDie();

        }

    }

    void ZombieDie()
    {
        isDead = true;
        animator.SetBool("die", true);
        mrig.AddForce(new Vector2(0f, 25f), ForceMode2D.Impulse);
        StartCoroutine(DieTimerCoroutine());
    }

    IEnumerator DieTimerCoroutine()
    {

        yield return new WaitForSeconds(10f);
        GameObject.Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            KnockBack();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            KnockBack();
        }
    }

    public void ZombieHit()
    {
        health -= 1;
        animator.SetBool("hit", true);
        KnockBack();
    }

    void KnockBack()
    {
        
        float direction = 1f;
        if (isRight)
        {
            direction = -1f;
        }
        mrig.AddForce(new Vector2(direction * knockBackForce, 0f), ForceMode2D.Impulse);
        knockBackMode = true;
        // TODO: add back in
        //GetComponent<AudioSource>().Play();
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        
        yield return new WaitForSeconds(0.5f);
        knockBackMode = false;
        animator.SetBool("hit", false);
    }
    void SetSpeed()
    {
        speed = -speed;
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
    public void AddScore(int points)
    {
        int myscore = Int32.Parse(scoreText.text);
        scoreText.text = (myscore + 1).ToString();
        PlayerPrefs.SetString("score", scoreText.text);
    }
}

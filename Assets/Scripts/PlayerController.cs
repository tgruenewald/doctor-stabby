using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2D;
    float moveHorizontal;
    float moveVertical;
    public float moveSpeed = 3f;
    public float jumpForce = 10f;
    bool isJumping;
    bool facingRight = true;
    Animator animator;
    bool hit = false;
    bool fight_front = false;
    public int health = 10000;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        isJumping = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Jump");
        
        if (Input.GetMouseButtonDown(0))
        {
            print("fight!");
            fight_front = true;
        }

        if (Input.GetMouseButtonUp(0))  
        {
            print("weapon back");
            fight_front= false; 

        }

        if (Input.GetKeyDown(KeyCode.W)) 
        {
            animator.SetBool("fight_up", true);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("fight_up", false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("fight_down", true);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("fight_down", false);
        }


        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    
    void FixedUpdate()
    {
        if (fight_front)
        {
            animator.SetBool("fight_front", true);
        }
        else
        {
            animator.SetBool("fight_front", false);
        }
        animator.SetFloat("speed", Mathf.Abs(moveHorizontal));
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            StartCoroutine(SoundCoroutine());
            
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse );
            
        }

        if (!isJumping && moveVertical > 0.1f)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
        if(health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
    IEnumerator SoundCoroutine()
    {
        GetComponent<AudioSource>().Play();
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("enter");
        if (collision.gameObject.tag == "Platform")
        {
            print("jumping false");
            isJumping = false;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("exit");
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            beingHit();
        }
    }

        void Flip()
    {
        facingRight = !facingRight;
        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    void beingHit()
    {
        print("i got hit");
        if (hit != true)
        {
            health -= 1;
            hit = true;
            animator.SetBool("hit", true);
            StartCoroutine(TimerCoroutine());
        }

    }
    IEnumerator TimerCoroutine()
    {
        print("Starting timer");
        yield return new WaitForSeconds(0.5f); 
        animator.SetBool("hit", false);
        hit = false;
    }

}

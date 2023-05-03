using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    public GameObject painBox;
    public float leftOffset;
    public float rightOffset;
    public float upOffset;
    public float downOffet;

    [Header("Text Object for Displaying Health")]
    public Text livesText;
    Rigidbody2D rb2D;
    float moveHorizontal;
    float moveVertical;
    public float moveSpeed = 3f;
    public float jumpForce = 10f;
    public bool isJumping;
    bool facingRight = true;
    Animator animator;
    bool hit = false;
    bool fight_front = false;
    public int health = 10000;
    bool bang = false;
    public GameObject gameOverButton;

    public bool isZombieJumpCooldownOver { get; private set; }

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
        isZombieJumpCooldownOver = true;
        PlayerPrefs.SetString("score", "0");
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        isJumping = false;
        livesText.text =  health.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -100f)
        {
            // fell out of the scene
            SceneManager.LoadScene("GameOver");
        }
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Jump");
        
        if (Input.GetMouseButtonDown(0))
        {
            fight_front = true;
        }

        if (Input.GetMouseButtonUp(0))  
        {
            fight_front= false; 

        }

        if (Input.GetKeyDown(KeyCode.W)) 
        {
            animator.SetBool("fight_up", true);
            FacingDirection facingDirection = FacingDirection.Up;
            Attack(facingDirection);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("fight_up", false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("fight_down", true);
            FacingDirection facingDirection = FacingDirection.Down;
            Attack(facingDirection);
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
            FacingDirection facingDirection;
            if (transform.localScale.x > 0)
            {
                facingDirection = FacingDirection.Right;
            } 
            else
            {
                facingDirection= FacingDirection.Left;

            }
            Attack(facingDirection);
        }
        else
        {
            animator.SetBool("fight_front", false);
        }
        animator.SetFloat("speed", Mathf.Abs(moveHorizontal));
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            //StartCoroutine(SoundCoroutine());
            
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse );
            
        }

        if (!isJumping && moveVertical > 0.1f)
        {
            isJumping = true;
            print("jumping now");
            rb2D.AddForce(new Vector2(0f, Mathf.Clamp(moveVertical * jumpForce, 0.1f, 80f)), ForceMode2D.Impulse);
        }
        if(health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    IEnumerator ZombieJumpCooldownTimerCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        isZombieJumpCooldownOver = true;
    }
    IEnumerator SoundCoroutine()
    {
        GetComponent<AudioSource>().Play();
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isZombieJumpCooldownOver && collision.gameObject.tag == "Zombie")
        {
            isJumping = false;
            isZombieJumpCooldownOver = false;
            StartCoroutine(ZombieJumpCooldownTimerCoroutine());
        }
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isZombieJumpCooldownOver && collision.gameObject.tag == "Zombie")
        {
            isJumping = true;
            isZombieJumpCooldownOver = false;
            StartCoroutine(ZombieJumpCooldownTimerCoroutine());
        }
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
        if (collision.gameObject.tag == "Zombie")
        {
            beingHit();
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
        if (hit != true)
        {
            health -= 5;
            livesText.text = health.ToString();
            if(health < 1)
            {
                SceneManager.LoadScene("GameOver");
            }
            hit = true;
            animator.SetBool("hit", true);
            StartCoroutine(TimerCoroutine());
        }

    }


    IEnumerator GameOverTimerCoroutine()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(0.5f); 
        animator.SetBool("hit", false);
        hit = false;
    }

    IEnumerator PainCoroutine(FacingDirection cd)
    {
        yield return new WaitForSeconds(0.7f);

        //if space is pushed
        //spawn pain box in current direction
        float playerX = transform.position.x;
        float playerY = transform.position.y;
        print("causing pain");
        switch (cd)
        {
            case FacingDirection.Up:
                Quaternion quat = new Quaternion(0, 0, 0, 0);
                Instantiate(painBox, new Vector3(playerX, transform.position.y + upOffset, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
                break;
            case FacingDirection.Down:
                Instantiate(painBox, new Vector3(playerX, transform.position.y - downOffet, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
                break;
            case FacingDirection.Left:
                Instantiate(painBox, new Vector3(transform.position.x - leftOffset, playerY + 5f, 0), Quaternion.identity);
                break;
            case FacingDirection.Right:
                Instantiate(painBox, new Vector3(transform.position.x + rightOffset, transform.position.y + 5f, 0), Quaternion.identity);
                break;
        }
        bang = false;
    }


    void Attack(FacingDirection cd)
    {
        //creates an object near player location that's hit box will hurt zombies
        //direction of object depends on facing direction
        if (!bang)
        {
            bang = true;
            StartCoroutine(PainCoroutine(cd));    

        }
    }

}

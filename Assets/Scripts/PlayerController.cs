using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    bool isJumping;
    bool facingRight = true;
    Animator animator;
    bool hit = false;
    bool fight_front = false;
    public int health = 10000;
    bool bang = false;

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
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        isJumping = false;
        livesText.text =  health.ToString();
        
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
                print("attacking right");
                facingDirection = FacingDirection.Right;
            } 
            else
            {
                print("attacking left");
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
            livesText.text = health.ToString();
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

    IEnumerator PainCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        bang = false;
    }


    void Attack(FacingDirection cd)
    {
        //creates an object near player location that's hit box will hurt zombies
        //direction of object depends on facing direction
        if (!bang)
        {
            bang = true;
            StartCoroutine(PainCoroutine());    
            //if space is pushed
            //spawn pain box in current direction
            float playerX = transform.position.x;
            float playerY = transform.position.y;
            switch (cd)
            {
                case FacingDirection.Up:
                    Debug.Log("got into up, player box should go to " + playerX + " " + transform.position.y + upOffset);
                    Quaternion quat = new Quaternion(0, 0, 0, 0);
                    Instantiate(painBox, new Vector3(playerX, transform.position.y + upOffset, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
                    break;
                case FacingDirection.Down:
                    Debug.Log("got into down, player box should go to " + playerX + " " + transform.position.y + downOffet);
                    Instantiate(painBox, new Vector3(playerX, transform.position.y - downOffet, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
                    break;
                case FacingDirection.Left:
                    Debug.Log("got into left, player box should go to " + transform.position.x + leftOffset + " " + playerY);
                    Instantiate(painBox, new Vector3(transform.position.x - leftOffset, playerY + 5f, 0), Quaternion.identity);
                    break;
                case FacingDirection.Right:
                    print("Up offset is " + upOffset);
                    Instantiate(painBox, new Vector3(transform.position.x + rightOffset, transform.position.y + 5f, 0), Quaternion.identity);

                    break;
            }
        }
    }

}

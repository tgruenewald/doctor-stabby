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

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        isJumping = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    
    void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse );
        }

        if (!isJumping && moveVertical > 0.1f)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
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
}
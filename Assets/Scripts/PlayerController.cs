using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 3.0f;
    public float JumpForce = 200.0f;
    public float Health = 100f;
    public float Armour = 0f;

    public LayerMask Ground;
    public Transform groundCheck;

    private bool grounded = false;
    private bool secondJump = false;

    private Vector2 moveDirection;
    private bool facingLeft = true;

    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider;

    public float SpeedBuff = 0f;
    public float BuffReduction = 1.0f;

    // Use this for initialization
    void Start () 
    {
        moveDirection = new Vector2(0, 0);
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        HandleBuffs();
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.01f, Ground);
        if (grounded)
        {
            secondJump = false;
        }
        rb2d.velocity = new Vector2(moveDirection.x, rb2d.velocity.y);
        moveDirection.x = 0;
	}

    public void TakeDamage(float damage)
    {
        if (Armour > 0)
        {
            Armour -= damage;
            if (Armour < 0)
            {
                Health += Armour;
                Armour = 0;
            }
        }
        else Health -= damage;
    }

    public void Movement(Vector2 direction)
    {
        SetDirection(direction.x);
        direction.x *= speed + SpeedBuff;
        moveDirection.x += direction.x;
        Mathf.Clamp(moveDirection.x, -speed - SpeedBuff, speed + SpeedBuff);
    }

    public void HandleBuffs()
    {
        if (SpeedBuff > 0)
        {
            SpeedBuff -= BuffReduction * Time.deltaTime;
        }
        else if (SpeedBuff < 0)
        {
            SpeedBuff = 0;
        }
    }

    public void Jump()
    {
        if (grounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.AddForce(new Vector2(0f, JumpForce));
        }
        else
        {
            if (secondJump == false)
            {
                secondJump = true;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
                rb2d.AddForce(new Vector2(0f, JumpForce));
            }
        }
    }

    public bool IsLeftFacing()
    {
        return facingLeft;
    }

    // Controls character direction
    private void SetDirection(float xAxis)
    {
        if(xAxis < 0 && !facingLeft)
        {
            facingLeft = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if(xAxis > 0 && facingLeft)
        {
            facingLeft = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}

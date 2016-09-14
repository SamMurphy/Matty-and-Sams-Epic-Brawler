using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 3.0f;
    public float JumpForce = 200.0f;
    public float Health = 100f;
    public float Armour = 0f;
    public int Lives = 3;
    public float DeathTime = 3.0f;

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

    private float deathTimer = 0.0f;
    
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
        if (Health <= 0 && Lives > 0)
        {
            deathTimer += Time.deltaTime;
            if(deathTimer >= DeathTime)
            {
                deathTimer = 0;
                Respawn();
            }

        }
        else
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

        if (Health <= 0)
            Death();
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

    private void Death()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<InputManager>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        Collider2D [] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
            collider.enabled = false;
        Lives--;
        if (Lives <= 0)
            PermaDeath();
    }

    private void Respawn()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<InputManager>().enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
            collider.enabled = true;
        Health = 100;
    }

    private void PermaDeath()
    {

    }
}

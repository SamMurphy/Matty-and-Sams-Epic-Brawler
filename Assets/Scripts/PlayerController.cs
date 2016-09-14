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
        transform.position = BestSpawnPoint();
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
        transform.position = BestSpawnPoint();
    }

    private void PermaDeath()
    {

    }

    private Vector2 BestSpawnPoint()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawn");
        GameObject bestSpawnPoint = spawnPoints[0];

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float bestDistance = 0;

        // Loop through spawn points
        foreach(GameObject spawn in spawnPoints)
        {
            float shortestPlayerDistance = 0;

            // Loop through players to get an intial shortest distance
            foreach(GameObject player in players)
            {
                if (player.transform != transform && player.GetComponent<PlayerController>().Health > 0)
                {
                    shortestPlayerDistance = Vector2.Distance(player.transform.position, spawn.transform.position);
                    break;
                }
            }

            // Find the true shortest distance
            foreach(GameObject player in players)
            {
                if(player.GetComponent<PlayerController>().Health > 0 && player.transform != transform)
                {
                    float distance = Vector2.Distance(player.transform.position, spawn.transform.position);
                    if(distance < shortestPlayerDistance)
                    {
                        shortestPlayerDistance = distance;
                    }
                }
            }

            // If shortest distance is furthest away from previously checked spawns then use this one
            if(shortestPlayerDistance > bestDistance)
            {
                bestDistance = shortestPlayerDistance;
                bestSpawnPoint = spawn;
            }
        }

        Vector2 spawnPosition = bestSpawnPoint.transform.position;

        return spawnPosition;
    }
}

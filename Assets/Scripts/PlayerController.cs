using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 3.0f;
    public float jumpHeight = 3.0f;
    public float gravity = -10.0f;

    private Vector2 moveDirection;

    private Rigidbody2D rb2d;
    private bool grounded = false;
    public Transform groundCheck;

    // Use this for initialization
    void Start () 
    {
        moveDirection = new Vector2(0, 0);
        rb2d = GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        rb2d.velocity = moveDirection;
        

        moveDirection.y += gravity * Time.deltaTime;
        if (grounded)
            moveDirection.y = 0;

        moveDirection.x = 0;
	}

    public void Movement(Vector2 direction)
    {
        direction.x *= speed;
        moveDirection.x += direction.x;
        Mathf.Clamp(moveDirection.x, -speed, speed);
    }

    public void Jump()
    {
        if (grounded)
            moveDirection.y = jumpHeight;
    }
}

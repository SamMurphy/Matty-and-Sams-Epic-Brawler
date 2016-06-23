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
    private BoxCollider2D boxCollider;

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
        grounded = Physics2D.Linecast(transform.position + new Vector3(boxCollider.size.x / 2, boxCollider.size.y / 2, 0), groundCheck.position + new Vector3(boxCollider.size.x / 2, 0, 0), 1 << LayerMask.NameToLayer("Ground"));
        if(!grounded)
            grounded = Physics2D.Linecast(transform.position + new Vector3(-boxCollider.size.x / 2, boxCollider.size.y / 2, 0), groundCheck.position - new Vector3(boxCollider.size.x / 2, 0, 0), 1 << LayerMask.NameToLayer("Ground"));

        if (moveDirection.y > 0)
            grounded = false;

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

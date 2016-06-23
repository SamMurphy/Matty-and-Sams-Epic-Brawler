using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public CharacterController character;

    public float speed = 3.0f;
    public float jumpHeight = 3.0f;
    public float gravity = -10.0f;

    private Vector2 moveDirection;

	// Use this for initialization
	void Start () 
    {
        moveDirection = new Vector2(0, 0);	
	}
	
	// Update is called once per frame
	void Update () 
    {
        character.Move(moveDirection * Time.deltaTime);

        moveDirection.y += gravity * Time.deltaTime;
        if (character.isGrounded)
            moveDirection.y = 0;

        moveDirection.x = 0;
	}

    public void Movement(Vector2 direction)
    {
        direction.x *= speed;
        moveDirection += direction;
        Mathf.Clamp(moveDirection.x, -speed, speed);
    }

    public void Jump()
    {
        if (character.isGrounded)
            moveDirection.y = jumpHeight;
    }
}

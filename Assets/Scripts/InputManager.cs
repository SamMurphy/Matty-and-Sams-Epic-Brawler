using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
    PlayerController player;
    private bool isJumping = false;

    // Use this for initialization
    void Start () 
    {
        player = GetComponent<PlayerController>();	
	}
	
	// Update is called once per frame
	void Update () 
    {
        float horizontal = Input.GetAxis("Horizontal");
        player.Movement(new Vector2(horizontal, 0));

        float isJumpPressed = Input.GetAxis("Jump");
        if (isJumpPressed > 0 && !isJumping)
        {
            isJumping = true;
            player.Jump();
        }
        if (isJumpPressed <= 0)
            isJumping = false;
	}
}

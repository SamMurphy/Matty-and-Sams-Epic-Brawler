using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
    PlayerController player;

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

        float isJumping = Input.GetAxis("Jump");
        if (isJumping > 0)
        {
            player.Jump();
        }
	}
}

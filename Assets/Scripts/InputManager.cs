using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(FightingScript))]
public class InputManager : MonoBehaviour 
{
    PlayerController player;
    FightingScript fightScript;

    private bool isJumping = false;

    public string playerNumber;

    // Use this for initialization
    void Start () 
    {
        player = GetComponent<PlayerController>();
        fightScript = GetComponent<FightingScript>();	
	}
	
	// Update is called once per frame
	void Update () 
    {
        float horizontal = Input.GetAxis("Horizontal" + playerNumber);
        player.Movement(new Vector2(horizontal, 0));

        float isJumpPressed = Input.GetAxis("Jump" + playerNumber);
        if (isJumpPressed > 0 && !isJumping)
        {
            isJumping = true;
            player.Jump();
        }
        if (isJumpPressed <= 0)
            isJumping = false;

        float meleeAttack = Input.GetAxis("Fire" + playerNumber);
        if(meleeAttack > 0)
        {
            fightScript.MeleeAttack();
        }
	}
}

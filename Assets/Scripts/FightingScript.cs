using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class FightingScript : MonoBehaviour {

    // Other Scripts
    private PlayerController player;

    // Combat parameters
    public float meleeRange = 1.0f;
    public float meleeDamage = 1.0f;
    public float meleeDropOff = 1.0f; // If set to 1.0 there is no drop off
    public float meleeStartPoint = 0.0f; // The distance of the melee hit box in relation to the centre of the character

    // Ranged attack parameters
    public float rangeAttackDamage = 1.0f;
    public float rangeAttackProjectileSpeed = 1.0f;
    public float rangeAttackStartPoint = 0.0f;
     
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    public void MeleeAttack()
    {
        Vector3 startPoint = transform.position;
        startPoint.x += meleeStartPoint;
        
        Vector2 direction;
        if (player.IsLeftFacing())
            direction = new Vector2(-1.0f, 0.0f);
        else
            direction = new Vector2(1.0f, 0.0f);

        RaycastHit2D[] allHits = Physics2D.RaycastAll(startPoint, direction, meleeRange);
        foreach (RaycastHit2D hit in allHits) // Loops through all things that are hit when player does melee
        {
            if (hit.collider != null) // If the ray hits an object
            {
                if (hit.transform.tag == "Player" && hit.transform != transform) // If object is a player and not itself
                {
                    PlayerController playerHit = hit.transform.gameObject.GetComponent<PlayerController>();
                    playerHit.Movement(direction);
                }
            }
        }
    }

    public void ProjectileAttack()
    {

    }
}

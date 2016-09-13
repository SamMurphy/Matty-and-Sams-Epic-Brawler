using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class FightingScript : MonoBehaviour {

    // Other Scripts
    private PlayerController player;

    // Combat parameters
    public float meleeDelay = 1.0f;
    public float meleeRange = 1.0f;
    public float meleeDamage = 1.0f;
    [Range(0f,1f)]
    public float meleeDropOff = 1.0f; // If set to 0 there is no drop off
    public float meleeStartPoint = 0.0f; // The distance of the melee hit box in relation to the centre of the character

    // Ranged attack parameters
    public float fireDelay = 1.0f;
    public float rangeAttackDamage = 1.0f;
    public float rangeAttackRange = 10.0f;
    public float rangeAttackProjectileSpeed = 1.0f;
    public float rangeAttackStartPoint = 0.0f;

    bool canFire = true;
    float rangeTimer = 0f;

    bool canHit = true;
    float meleeTimer = 0f;
     
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!canFire)
        {
            rangeTimer += Time.deltaTime;
            if (rangeTimer >= fireDelay) canFire = true;
        }
        if (!canHit)
        {
            meleeTimer += Time.deltaTime;
            if (meleeTimer >= meleeDelay) canHit = true;
        }
    }

    public void MeleeAttack()
    {
        if (!canHit) return;

        canHit = false;
        meleeTimer = 0f;

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
                    // Calcualte damage
                    float hitDistance = hit.distance / meleeRange;
                    float damageReduction = hitDistance * meleeDropOff;
                    float damage = meleeDamage * (1 - damageReduction);

                    PlayerController playerHit = hit.transform.gameObject.GetComponent<PlayerController>();
                    playerHit.Movement(direction);
                    playerHit.TakeDamage(damage);
                    break; // So only one game object is damaged
                }
            }
        }
    }

    public void ProjectileAttack()
    {
        if (!canFire) return;

        canFire = false;
        rangeTimer = 0f;

        Vector3 startPoint = transform.position;
        startPoint.x += rangeAttackStartPoint;

        GameObject projectile = Instantiate(Resources.Load<GameObject>("Prefabs/Bullet"));

        Physics2D.IgnoreCollision(projectile.transform.GetComponent<Collider2D>(), player.GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(projectile.transform.GetComponent<Collider2D>(), player.GetComponent<CircleCollider2D>());

        projectile.transform.position = startPoint;

        ProjectileScript projectileScript = projectile.GetComponent<ProjectileScript>();
        projectileScript.Speed = rangeAttackProjectileSpeed;
        projectileScript.Damage = rangeAttackDamage;
        projectileScript.Owner = player;
        projectileScript.Range = rangeAttackRange;
        projectileScript.startingPoint = startPoint;

        if (player.IsLeftFacing())
            projectileScript.Direction = new Vector2(-1.0f, 0.0f);
        else
            projectileScript.Direction = new Vector2(1.0f, 0.0f);

    }
}

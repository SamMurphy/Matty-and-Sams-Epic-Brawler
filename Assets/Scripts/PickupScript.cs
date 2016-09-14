using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PickupScript : MonoBehaviour {

    public PickupType type;
    public float BounceRange = 0.5f;
    public float BounceSpeed = 0.5f;
    public float RotationSpeed = 0.5f;

    private Rigidbody2D rb2d;
    private Vector2 initialPosition;

	// Use this for initialization
	void Start ()
    {
        if (type == PickupType.Random)
        {
            type = (PickupType)Random.Range(1, 5);
        }

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, -BounceSpeed);
        initialPosition = new Vector2(transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.y > initialPosition.y + BounceRange)
        {
            rb2d.velocity = new Vector2(0, -BounceSpeed);
        }
        else if (transform.position.y < initialPosition.y - BounceRange)
        {
            rb2d.velocity = new Vector2(0, BounceSpeed);
        }

        transform.Rotate(0, (RotationSpeed * Time.deltaTime), 0);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            // Do things to the player
            PlayerController player = coll.gameObject.GetComponent<PlayerController>();
            switch (type)
            {
                case PickupType.Health:
                    player.Health += 10f;
                    break;
                case PickupType.Armour:
                    player.Armour += 10f;
                    break;
                case PickupType.Damaage:
                    coll.gameObject.GetComponent<FightingScript>().DamageBuff += 10f;
                    break;
                case PickupType.Speed:
                    player.SpeedBuff += 10f;
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
        }
    }
}

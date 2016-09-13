using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ProjectileScript : MonoBehaviour {


    public float Speed = 1.0f;
    public Vector2 Direction = new Vector2(1.0f, 0f);
    public float Range = 1.0f;
    public float Damage = 1.0f;
    public PlayerController Owner;
    public Vector2 startingPoint = new Vector2(0, 0);

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        rb2d.velocity = new Vector2(Direction.x * Speed, 0);

        float distance = Vector2.Distance(startingPoint, transform.position);
        if (distance > Range)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Bullet")
        {

            if (coll.gameObject.tag == "Player")
            {
                if (coll.gameObject != Owner.gameObject)
                {
                    // Enemy collision
                    PlayerController playerHit = coll.transform.gameObject.GetComponent<PlayerController>();
                    playerHit.TakeDamage(Damage);
                }
            }

            Destroy(gameObject);
        }
    }
}

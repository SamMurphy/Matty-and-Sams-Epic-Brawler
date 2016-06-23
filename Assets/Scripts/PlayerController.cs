using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public CharacterController character;

    public float speed = 3.0f;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Movement(Vector2 direction)
    {
        direction.x *= speed;
        character.Move(new Vector3(direction.x, direction.y, 0) * Time.deltaTime);
    }

    public void Jump()
    {

    }
}

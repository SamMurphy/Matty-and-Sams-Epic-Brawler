using UnityEngine;
using System.Collections;


public enum PickupType
{
    Random = 0,
    Health = 1,
    Armour = 2,
    Damaage = 3,
    Speed = 4,
}

public class PickupSpawner : MonoBehaviour {

    public PickupType type = PickupType.Random;
    public float RespawnTime = 10f;

    private float timer = 0f;

	void Start ()
    {
        SpawnPickup();
	}
	
	void Update ()
    {
        // Check if pickup already exists
        if (transform.childCount == 0)
        {
            // Wait and spawn pickup
            timer += Time.deltaTime;
            if (timer >= RespawnTime)
            {
                timer = 0f;
                SpawnPickup();
            }
        }
    }

    // Draw icon, for easier placement in editor
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "spawner.tif", true);
    }

    void SpawnPickup()
    {
        GameObject pickup = Instantiate(Resources.Load<GameObject>("Prefabs/Pickup"));
        PickupScript pickupScript = pickup.GetComponent<PickupScript>();
        pickupScript.type = type;
        pickupScript.transform.parent = transform;
        pickupScript.name = type.ToString();
        pickupScript.transform.position = transform.position;
    }
}

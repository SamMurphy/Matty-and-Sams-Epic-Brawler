using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour {
    public float MinZoom = 5.0f;
    public float ZoomOffset = 1.0f;
    public float MaxMoveSpeed = 1.0f;
    public float MaxZoomSpeed = 0.1f;
    

    Camera camera;

    GameObject[] players;
    
    Vector2 averagePosition;


	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        camera.orthographicSize = CalculateZoom();
        averagePosition = AveragePositionOfAllPlayers();
        camera.transform.position = new Vector3(averagePosition.x, averagePosition.y, -10);
	}

    Vector2 AveragePositionOfAllPlayers()
    {
        int alivePlayers = 0;
        Vector2 average = Vector2.zero;
        foreach(GameObject player in players)
        {
            if(player.GetComponent<PlayerController>().Health > 0)
            {
                alivePlayers++;
                average += new Vector2(player.transform.position.x, player.transform.position.y);
            }
        }

        average = average / alivePlayers;


        float currentX = camera.transform.position.x;
        if (average.x > currentX + MaxMoveSpeed)
            average.x = currentX + MaxMoveSpeed;
        else if (average.x < currentX - MaxMoveSpeed)
            average.x = currentX - MaxMoveSpeed;

        float currentY = camera.transform.position.y;
        if (average.y > currentY + MaxMoveSpeed)
            average.y = currentY + MaxMoveSpeed;
        else if (average.y < currentY - MaxMoveSpeed)
            average.y = currentY - MaxMoveSpeed;

        return average;
    }

    float CalculateZoom()
    {
        float zoom = MinZoom;
        Vector2 cameraVec2 = new Vector2(camera.transform.position.x, camera.transform.position.y);

        float distanceToMaxPlayer = 0;
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerController>().Health > 0)
            {
                float distanceToPlayer = Vector2.Distance(cameraVec2, new Vector2(player.transform.position.x, player.transform.position.y)) + ZoomOffset;
                if (distanceToPlayer > distanceToMaxPlayer)
                {
                    distanceToMaxPlayer = distanceToPlayer;
                }
            }
        }

        if (distanceToMaxPlayer > MinZoom)
            zoom = distanceToMaxPlayer;

        float currentZoom = camera.orthographicSize;
        if (zoom > currentZoom + MaxZoomSpeed)
            zoom = currentZoom + MaxZoomSpeed;
        else if (zoom < currentZoom - MaxZoomSpeed)
            zoom = currentZoom - MaxZoomSpeed;
        
        return zoom;
    }
}

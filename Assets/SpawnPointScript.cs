using UnityEngine;
using System.Collections;

public class SpawnPointScript : MonoBehaviour {

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
    }
}

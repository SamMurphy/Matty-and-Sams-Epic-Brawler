using UnityEngine;
using System.Collections;

public class DrawMeshGizmo : MonoBehaviour {

    public Mesh mesh;
    public Color color = Color.red;

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        if (mesh == null)
            Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
        else if (mesh != null)
        {
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
            Gizmos.DrawMesh(mesh, -1, transform.position, transform.rotation, Vector3.one);
        }
    }
}

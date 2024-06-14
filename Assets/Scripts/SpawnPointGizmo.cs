using UnityEngine;

public class SpawnPointGizmo : MonoBehaviour
{
    public Color gizmoColor = Color.yellow;
    public float gizmoSize = 0.5f;

    void OnDrawGizmos()
    {

        Gizmos.color = gizmoColor;

        Gizmos.DrawSphere(transform.position, gizmoSize);
       
    }
}

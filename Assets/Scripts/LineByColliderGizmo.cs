using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineByColliderGizmo : MonoBehaviour
{
    [ExecuteInEditMode]
    private BoxCollider2D boxCollider;

    void OnDrawGizmos()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
            return;

        Gizmos.color = Color.white;


        Vector2 colliderSize = boxCollider.size;
        Vector2 colliderOffset = boxCollider.offset;

        Vector3 bottomLeft = transform.TransformPoint(new Vector2(colliderOffset.x - colliderSize.x / 2, colliderOffset.y - colliderSize.y / 2));
        Vector3 topLeft = transform.TransformPoint(new Vector2(colliderOffset.x - colliderSize.x / 2, colliderOffset.y + colliderSize.y / 2));
        Vector3 topRight = transform.TransformPoint(new Vector2(colliderOffset.x + colliderSize.x / 2, colliderOffset.y + colliderSize.y / 2));
        Vector3 bottomRight = transform.TransformPoint(new Vector2(colliderOffset.x + colliderSize.x / 2, colliderOffset.y - colliderSize.y / 2));

        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
    }
}

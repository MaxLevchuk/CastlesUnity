using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineByCollider : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
       
        if (boxCollider != null)
        {
         
            lineRenderer.positionCount = 5;
            lineRenderer.loop = true;

         
            Vector2 colliderSize = boxCollider.size;
            Vector2 colliderOffset = boxCollider.offset;

            Vector3[] points = new Vector3[5];
            points[0] = transform.TransformPoint(new Vector2(colliderOffset.x - colliderSize.x / 2, colliderOffset.y - colliderSize.y / 2)); 
            points[1] = transform.TransformPoint(new Vector2(colliderOffset.x - colliderSize.x / 2, colliderOffset.y + colliderSize.y / 2)); 
            points[2] = transform.TransformPoint(new Vector2(colliderOffset.x + colliderSize.x / 2, colliderOffset.y + colliderSize.y / 2)); 
            points[3] = transform.TransformPoint(new Vector2(colliderOffset.x + colliderSize.x / 2, colliderOffset.y - colliderSize.y / 2)); 
            points[4] = points[0];

  
            lineRenderer.SetPositions(points);
        }
        else
        {
            Debug.LogError("BoxCollider2D не найден на объекте.");
        }


        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.widthMultiplier = 0.05f; 
    }
}

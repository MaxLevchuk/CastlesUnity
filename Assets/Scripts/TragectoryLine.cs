using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int lineSegmentCount = 50;
    public float timeBetweenPoints = 0.1f;
    // Distance limitation
    public float maxTrajectoryDistance = 7f;

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        lineRenderer.positionCount = lineSegmentCount;

        // Width
        lineRenderer.startWidth = 0.25f;
        lineRenderer.endWidth = 0.05f;

        // Gradient and colour
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[]
            {
                new GradientColorKey(new Color(0.5f, 0.8f, 1f, 1f), 0.0f),
                new GradientColorKey(Color.clear, 1.0f) 
            },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1.0f, 0.0f), 
                new GradientAlphaKey(0.0f, 0.5f) 
            }
        );
        lineRenderer.colorGradient = gradient;
    }

    public void UpdateTrajectory(Vector2 startPosition, Vector2 initialVelocity)
    {
        Vector3[] points = new Vector3[lineSegmentCount];
        Vector2 currentPosition = startPosition;
        Vector2 velocity = initialVelocity;

        for (int i = 0; i < lineSegmentCount; i++)
        {
            float t = i * timeBetweenPoints;
            points[i] = currentPosition + velocity * t + 0.5f * Physics2D.gravity * (t * t);

            // Overlaying the max distance length over further stretching
            if(Vector2.Distance(startPosition, points[i]) > maxTrajectoryDistance)
            {
                Vector2 direction = ((Vector2)points[i] - startPosition).normalized;
                points[i] = (Vector3)(startPosition + direction * maxTrajectoryDistance);
                for (int j = i + 1; j < lineSegmentCount; j++)
                {
                    points[j] = points[i];
                }
                break;
            }
        }

        lineRenderer.SetPositions(points);
    }

    public void ClearTrajectory()
    {
        lineRenderer.positionCount = 0;
    }
}


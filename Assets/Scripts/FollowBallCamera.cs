using UnityEngine;

public class FollowBallCamera : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = transform.position.z;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            smoothedPosition.y += 0.2f;
            transform.position = smoothedPosition;
        }
    }
}

using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    // Changable movement properties
    public float amplitude = 1.5f; 
    public float speed = 1f;      

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newX = startPosition.x + amplitude * Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(newX, startPosition.y, startPosition.z);
    }
}


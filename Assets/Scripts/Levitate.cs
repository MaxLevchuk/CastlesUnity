using UnityEngine;

public class Levitate : MonoBehaviour
{
    public float amplitude = 0.5f; 
    public float speed = 1f;       

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + amplitude * Mathf.Sin(Time.time * speed);
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}

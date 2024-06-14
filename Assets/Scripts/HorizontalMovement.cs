using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    // Changable movement properties
    public float moveSpeed = 0.5f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    public float leftBound = -0.3f;
    public float rightBound = 0.3f;

    private Rigidbody2D rb;
    private float currentSpeed = 0f;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        Vector3 localPosition = transform.localPosition;


        if (localPosition.x >= rightBound)
        {
            movingRight = false;
        }
        else if (localPosition.x <= leftBound)
        {
            movingRight = true;
        }


        if (movingRight)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, -moveSpeed, acceleration * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + new Vector2(currentSpeed * Time.fixedDeltaTime, 0));
    }
}



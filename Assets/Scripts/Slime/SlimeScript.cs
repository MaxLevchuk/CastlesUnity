using System.Collections;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public Animator animator;
    public float minJumpForce = 4f;
    public float maxJumpForce = 6f;

    public float minHorizontalJumpSpeed = 1f;
    public float maxHorizontalJumpSpeed = 3f;

    public float minJumpDelay = 0.8f;
    public float maxJumpDelay = 1.5f;

    public float minDistanceToChangeDirection = 0.1f;
    private Rigidbody2D rb;
    private bool isTouchingSurface;
    private float jumpTimer;
    private Vector2 lastLandingPosition;
    private bool movingRight = true;

    private float jumpForce;
    private float horizontalJumpSpeed;
    private float jumpDelay; 

    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetRandomJumpParameters();
        jumpTimer = jumpDelay; 
        lastLandingPosition = rb.position;
    }

    void Update()
    {
        AirCheck();

        
        if (isTouchingSurface && jumpTimer <= 0f && !isJumping)
        {       
            StartCoroutine(PrepareJump());
        }
        else if (isTouchingSurface)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void AirCheck()
    {
        animator.SetBool("InAir", !isTouchingSurface);
    }

    IEnumerator PrepareJump()
    {
        isJumping = true;
        animator.SetTrigger("Jump");
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        while (!stateInfo.IsName("Jump") || stateInfo.normalizedTime < 1.0f)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        Jump();

        SetRandomJumpParameters();
        jumpTimer = jumpDelay;

        isJumping = false;
    }

    void Jump()
    {
        float horizontalSpeed = movingRight ? horizontalJumpSpeed : -horizontalJumpSpeed;
        rb.velocity = new Vector2(horizontalSpeed, jumpForce);
        lastLandingPosition = rb.position;
    }

    void SetRandomJumpParameters()
    {
        jumpForce = Random.Range(minJumpForce, maxJumpForce);
        horizontalJumpSpeed = Random.Range(minHorizontalJumpSpeed, maxHorizontalJumpSpeed);
        jumpDelay = Random.Range(minJumpDelay, maxJumpDelay);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isTouchingSurface)
        {
            isTouchingSurface = true;
            float distance = Vector2.Distance(lastLandingPosition, rb.position);
            if (distance < minDistanceToChangeDirection)
            {
                movingRight = !movingRight;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isTouchingSurface = false;
        jumpTimer = jumpDelay;
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
}

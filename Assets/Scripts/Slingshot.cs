using UnityEngine;

public class SlingshotScript : MonoBehaviour
{
    public static SlingshotScript Instance { get; private set; }
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float launchForce = 10f;
    public float maxStretch = 3f;
    public float minStretch = 0.5f;
    private GameObject currentProjectile;
    public bool isAiming = false;

    // Particles
    public GameObject hitParticlesPrefab;

    // Trajectory property
    private TrajectoryLine trajectoryLine;

    private bool initialCursorVisibility;

    private BallCount ballCount;

    private Vector3 touchStartPoint;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        initialCursorVisibility = Cursor.visible;
        trajectoryLine = GetComponent<TrajectoryLine>();
        ballCount = FindObjectOfType<BallCount>();
    }

    void Update()
    {
        if (ballCount.GetBallCount() > 0 && Time.timeScale != 0) // Pause check
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    StartAiming(touch);
                }

                if (touch.phase == TouchPhase.Moved && isAiming)
                {
                    Aim(touch);
                }

                if (touch.phase == TouchPhase.Ended && isAiming)
                {
                    LaunchProjectile();
                }
            }
        }
    }

    void StartAiming(Touch touch)
    {
        isAiming = true;
        currentProjectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        currentProjectile.GetComponent<Rigidbody2D>().isKinematic = true;
        currentProjectile.GetComponent<Collider2D>().enabled = false;

        touchStartPoint = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
        touchStartPoint.z = 0;

        var particleCollisionScript = currentProjectile.GetComponent<BallParticleCollision>();
        if (particleCollisionScript != null && particleCollisionScript.hitParticles == null)
        {
            particleCollisionScript.hitParticles = Instantiate(hitParticlesPrefab, currentProjectile.transform).GetComponent<ParticleSystem>();
            Debug.Log("Particle system instantiated and attached to projectile");
        }
        Cursor.visible = false;

        // Resetting LineRenderer
        trajectoryLine.lineRenderer.positionCount = trajectoryLine.lineSegmentCount;
    }

    void Aim(Touch touch)
    {
        Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
        touchWorldPosition.z = 0;   
        Vector3 currentDirection = touchWorldPosition - touchStartPoint;
        float stretchDistance = currentDirection.magnitude;
        if (stretchDistance > maxStretch)
        {
            currentDirection = currentDirection.normalized * maxStretch;
        }
        currentProjectile.transform.position = launchPoint.position + currentDirection;
        Vector2 launchVelocity = (currentDirection.normalized * (stretchDistance / maxStretch) * launchForce);
        trajectoryLine.UpdateTrajectory(currentProjectile.transform.position, launchVelocity);
    }

    void LaunchProjectile()
    {
        isAiming = false;
        currentProjectile.GetComponent<Collider2D>().enabled = true;

        Vector3 launchDirection = (launchPoint.position - currentProjectile.transform.position).normalized;
        float stretchDistance = (launchPoint.position - currentProjectile.transform.position).magnitude;

        if (stretchDistance < minStretch)
        {
            Destroy(currentProjectile);
            currentProjectile = null;
            Cursor.visible = initialCursorVisibility;
            trajectoryLine.ClearTrajectory();
            return;
        }

        Rigidbody2D rb = currentProjectile.GetComponent<Rigidbody2D>();
        if (rb == null) return;
        rb.isKinematic = false;

        BallDisappearing bd = currentProjectile.GetComponent<BallDisappearing>();  // enable ball disappearing and disable kinematic
        bd.enabled = true;

        float launchForceMultiplier = Mathf.Clamp(stretchDistance / maxStretch, 0.5f, 1f);
        Vector2 initialVelocity = launchDirection * launchForceMultiplier * launchForce;
        rb.AddForce(initialVelocity, ForceMode2D.Impulse);

        var transformBallScript = currentProjectile.GetComponent<Split>();
        if (transformBallScript != null)
        {
            transformBallScript.MarkAsLaunched();
        }

        currentProjectile = null;

        Cursor.visible = initialCursorVisibility;
        trajectoryLine.ClearTrajectory();

        if (ballCount != null)
        {
            ballCount.RemoveBall();
            if (ballCount.GetBallCount() <= 0)
            {
                ballCount.NoBallsLeft();
            }
        }
    }
}

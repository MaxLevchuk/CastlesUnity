using UnityEngine;

public class TutorialSlingshot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float launchForce = 10f;
    public float maxStretch = 3f;
    public float minStretch = 0.5f;
    private GameObject currentProjectile;
    public bool isAiming = false;
    public bool canShoot = true;
    public TutorialHandAnimation hand;

    // Particles
    public GameObject hitParticlesPrefab;

    // Trajectory property
    private TrajectoryLine trajectoryLine;

    private bool initialCursorVisibility;


    void Start()
    {

        initialCursorVisibility = Cursor.visible;
        trajectoryLine = GetComponent<TrajectoryLine>();
       
    }

    void Update()
    {
        if (canShoot && Time.timeScale != 0) // Tutorial check 
        {
           
            if (Input.GetMouseButtonDown(0))
            {
                StartAiming();
                hand.HandDisable();
            }

            if (Input.GetMouseButton(0) && isAiming)
            {
                Aim();
            }

            if (Input.GetMouseButtonUp(0) && isAiming)
            {
                LaunchProjectile();
            }
             
        }
    }

    void StartAiming()
    {
        isAiming = true;
        currentProjectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        currentProjectile.GetComponent<Rigidbody2D>().isKinematic = true;
        currentProjectile.GetComponent<Collider2D>().enabled = false;

        // Attaching particles to new projectile
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

    void Aim()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 mouseMovement = new Vector3(mouseX, mouseY, 0);
        Vector3 worldPosition = currentProjectile.transform.position + mouseMovement;

        Vector3 aimDirection = worldPosition - launchPoint.position;
        float stretchDistance = aimDirection.magnitude;

        if (stretchDistance > maxStretch)
        {
            aimDirection = aimDirection.normalized * maxStretch;
        }

        currentProjectile.transform.position = launchPoint.position + aimDirection;

        Vector2 launchVelocity = (aimDirection.normalized * (stretchDistance / maxStretch) * launchForce);

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

        float launchForceMultiplier = Mathf.Clamp(stretchDistance / maxStretch, 0.5f, 1f);
        Vector2 initialVelocity = launchDirection * launchForceMultiplier * launchForce;
        rb.AddForce(initialVelocity, ForceMode2D.Impulse);

        // Ball transformation script is notified of the launch
        var transformBallScript = currentProjectile.GetComponent<Split>();
        if (transformBallScript != null)
        {
            transformBallScript.MarkAsLaunched();
        }

        currentProjectile = null;

        Cursor.visible = initialCursorVisibility;
        trajectoryLine.ClearTrajectory();

        
    }
}

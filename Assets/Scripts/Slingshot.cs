using UnityEngine;

public class SlingshotScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float launchForce = 10f;
    public float maxStretch = 3f;
    public float minStretch = 0.5f;
    private GameObject currentProjectile;
    private bool isAiming = false;

    // Trajectory property
    private TrajectoryLine trajectoryLine;

    private bool initialCursorVisibility;

    public BallCount ballCount;

    void Start()
    {
        initialCursorVisibility = Cursor.visible;
        trajectoryLine = GetComponent<TrajectoryLine>();
    }

    void Update()
    {
        if (ballCount.GetBallCount() > 0 && !PauseManager.isPaused) // Проверка паузы
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartAiming();
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
        rb.AddForce(launchDirection * launchForceMultiplier * launchForce, ForceMode2D.Impulse);

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

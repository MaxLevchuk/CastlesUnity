using UnityEngine;
using UnityEngine.UI;

public class SlingshotScript : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float launchForce = 10f;
    public float maxStretch = 3f;
    private GameObject currentProjectile;
    private bool isAiming = false;

    // Trajectory property
    private TrajectoryLine trajectoryLine;

    private bool initialCursorVisibility;

    void Start()
    {
        initialCursorVisibility = Cursor.visible;
        // Show TrajectoryLine in the scene
        trajectoryLine = GetComponent<TrajectoryLine>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentProjectile == null)
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
        // Some coordinates changes
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Ensure the Z-coordinate is zero

        Vector3 aimDirection = launchPoint.position - mouseWorldPosition;
        float stretchDistance = aimDirection.magnitude;

        if (stretchDistance > maxStretch)
        {
            aimDirection = aimDirection.normalized * maxStretch;
        }

        currentProjectile.transform.position = launchPoint.position - aimDirection;

        // Calculate the launch velocity
        Vector2 launchVelocity = (aimDirection.normalized * (stretchDistance / maxStretch) * launchForce);

        trajectoryLine.UpdateTrajectory(currentProjectile.transform.position, launchVelocity);
    }

    void LaunchProjectile()
    {
        isAiming = false;

        Rigidbody2D rb = currentProjectile.GetComponent<Rigidbody2D>();
        if (rb == null) return;
        rb.isKinematic = false;

        Vector3 launchDirection = (launchPoint.position - currentProjectile.transform.position).normalized;
        float launchForceMultiplier = Mathf.Clamp((launchPoint.position - currentProjectile.transform.position).magnitude / maxStretch, 0.5f, 1f);

        rb.AddForce(launchDirection * launchForceMultiplier * launchForce, ForceMode2D.Impulse);

        currentProjectile = null;

        Cursor.visible = initialCursorVisibility;

        // Clear trajectory line
        trajectoryLine.ClearTrajectory();
    }
}

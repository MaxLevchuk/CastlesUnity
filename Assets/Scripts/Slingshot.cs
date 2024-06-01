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

   
    private bool initialCursorVisibility;

    void Start()
    {
     
        initialCursorVisibility = Cursor.visible;
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
    }
}

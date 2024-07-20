using System.Collections;
using UnityEngine;

public class AutoObjectFlyIn : MonoBehaviour
{
    public float duration = 2.0f;

    void Start()
    {

        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (var obj in allObjects)
        {

            if (obj.CompareTag("Score") || obj.CompareTag("Options") || obj.CompareTag("MainCamera"))
            {
                continue;
            }

            Vector3 startPos = GetRandomStartPosition(obj.transform.position);
            Vector3 endPos = obj.transform.position;


            obj.transform.position = startPos;


            Quaternion startRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            Quaternion endRotation = obj.transform.rotation;
            obj.transform.rotation = startRotation;


            SetComponentsEnabled(obj, false);
            SetPhysicsEnabled(obj, false);

            StartCoroutine(FlyIn(obj, startPos, endPos, startRotation, endRotation, duration));
        }
    }

    Vector3 GetRandomStartPosition(Vector3 endPos)
    {
        Camera mainCamera = Camera.main;
        float screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).x;
        float screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane)).x;
        float screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).y;

        return new Vector3(Random.Range(screenLeft, screenRight), screenBottom - 5, endPos.z);
    }


    IEnumerator FlyIn(GameObject obj, Vector3 startPos, Vector3 endPos, Quaternion startRotation, Quaternion endRotation, float duration)
    {
        float elapsedTime = 0;

        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        Color originalColor = Color.white;
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        }

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            obj.transform.position = Vector3.Lerp(startPos, endPos, t);
            obj.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            if (spriteRenderer != null)
            {
                float alpha = Mathf.Lerp(0, originalColor.a, t);
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = endPos;
        obj.transform.rotation = endRotation;

        SetComponentsEnabled(obj, true);
        SetPhysicsEnabled(obj, true);
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    void SetComponentsEnabled(GameObject obj, bool enabled)
    {
        var components = obj.GetComponents<Component>();
        foreach (var component in components)
        {
            if (!(component is Transform))
            {
                if (component is Behaviour behaviour)
                {
                    behaviour.enabled = enabled;
                }
                else if (component is Collider collider)
                {
                    collider.enabled = enabled;
                }

            }
        }
    }

    void SetPhysicsEnabled(GameObject obj, bool enabled)
    {
        var rigidbody = obj.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.isKinematic = !enabled;
        }
        var rigidbody2D = obj.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null)
        {
            rigidbody2D.isKinematic = !enabled;
        }
    }
}

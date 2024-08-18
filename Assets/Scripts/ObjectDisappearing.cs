using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ObjectDisappearing : MonoBehaviour
{
    public float disappearTime = 4f;

    private Light2D objLight;
    private SpriteRenderer spriteRenderer;

    private float creationTime;

    void Start()
    {
        objLight = GetComponent<Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        creationTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - creationTime;

        float currentAlpha = Mathf.Lerp(1f, 0f, elapsedTime / disappearTime);

        Color newColor = spriteRenderer.color;
        newColor.a = currentAlpha;
        spriteRenderer.color = newColor;

      
        if (objLight != null)
        {
            objLight.color = newColor;
        }
        else 
        {
            Debug.Log("Light not set: " + gameObject.name.ToString());
         }

        if (elapsedTime >= disappearTime)
        {
            Destroy(gameObject);
        }
    }
}

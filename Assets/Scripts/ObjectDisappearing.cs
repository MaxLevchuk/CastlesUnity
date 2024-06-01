using UnityEngine;

public class ObjectDisappearing : MonoBehaviour
{
    public float disappearTime = 4f;

    
    private SpriteRenderer spriteRenderer;

    private float creationTime;

    void Start()
    {
      
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

        if (elapsedTime >= disappearTime)
        {
            Destroy(gameObject);
        }
    }
}

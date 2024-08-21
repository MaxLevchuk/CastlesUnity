using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BallDisappearing : MonoBehaviour
{
    public float disappearTime = 8f; 
    private SpriteRenderer spriteRenderer;
    private Light2D ballLight;

    void Start()
    {
        ballLight = GetComponent<Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
      
    }

    private void OnEnable()
    {
        StartCoroutine(DisappearAfterTime(disappearTime));
    }
    private IEnumerator DisappearAfterTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // w8 to start dissapearing

        float fadeDuration = 1f; // time to dissapear
        float elapsedTime = 0f;

        // hide trail
        gameObject.GetComponent<TrailRenderer>().enabled = false;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // alpha color change
            Color newColor = spriteRenderer.color;
            newColor.a = alpha;
            spriteRenderer.color = newColor;
            ballLight.color = newColor;
            yield return null;
        }

        Destroy(gameObject); 
    }
}

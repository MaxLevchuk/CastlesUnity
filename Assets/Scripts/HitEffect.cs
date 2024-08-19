using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class HitEffect : MonoBehaviour
{
    public Light2D hitLight; 
    public float intensityIncrease = 2f; 
    public Color hitColor = Color.white; 
    public float effectDuration = 0.5f; 
    private SpriteRenderer sprite;

    private float originalIntensity;
    private Color originalColor;

    private void Start()
    {
        hitLight = GetComponent<Light2D>();
        sprite = GetComponent<SpriteRenderer>();
        originalIntensity = hitLight.intensity; 
        originalColor = hitLight.color;
    }

    public void TriggerHitEffect()
    {
        StartCoroutine(AnimateLightEffect());
    }

    private IEnumerator AnimateLightEffect()
    {
        float elapsedTime = 0f;
        float targetIntensity = originalIntensity + intensityIncrease;

     
        while (elapsedTime < effectDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            hitLight.intensity = Mathf.Lerp(originalIntensity, targetIntensity, elapsedTime / (effectDuration / 2));
            sprite.color = Color.Lerp(originalColor, hitColor, elapsedTime / (effectDuration / 2));
            yield return null;
        }

        elapsedTime = 0f;

     
        while (elapsedTime < effectDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            hitLight.intensity = Mathf.Lerp(targetIntensity, originalIntensity, elapsedTime / (effectDuration / 2));
            sprite.color = Color.Lerp(hitColor, originalColor, elapsedTime / (effectDuration / 2));
            yield return null;
        }

        hitLight.intensity = originalIntensity;
        sprite.color = originalColor;
    }
}

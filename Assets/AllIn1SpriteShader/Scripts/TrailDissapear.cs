using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDissapear : MonoBehaviour
{

    /*private void OnEnable()
    {
        StartCoroutine(DisappearAfterTime(disappearTime));
    }
    private IEnumerator DisappearAfterTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // w8 to start dissapearing

        float fadeDuration = 1f; // time to dissapear
        float elapsedTime = 0f;



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
    }*/
}

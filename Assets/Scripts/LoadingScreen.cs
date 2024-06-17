using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public TMP_Text loadingText;
    public Image loadingBar;
    public Canvas loadingCanvas;
    public float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(LoadLevelAsync("Level4"));
    }

    IEnumerator LoadLevelAsync(string sceneName)
    {
        loadingCanvas.enabled = true;
        yield return StartCoroutine(FadeCanvas(true, fadeDuration));

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Disabling scene activation until loading is done
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Calculating loading progress
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // Debug test - can be deleted
            Debug.Log($"Loading Progress: {progress * 100}%");

            loadingBar.fillAmount = progress;
            loadingText.text = "Loading... " + (progress * 100f).ToString("F0") + "%";

            if (operation.progress >= 0.9f)
            {
                // Scene activation and fading when loading is almost done
                operation.allowSceneActivation = true;
                yield return StartCoroutine(FadeCanvas(false, fadeDuration));

                loadingCanvas.enabled = false;
            }


            yield return null;
        }

        // Fading logic
        IEnumerator FadeCanvas(bool fadeIn, float duration)
        {
            float startAlpha = fadeIn ? 0f : 1f;
            float endAlpha = fadeIn ? 1f : 0f;
            float elapsed = 0f;

            CanvasGroup canvasGroup = loadingCanvas.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = loadingCanvas.gameObject.AddComponent<CanvasGroup>();
            }

            while (elapsed < duration)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = endAlpha;
        }
    }
}


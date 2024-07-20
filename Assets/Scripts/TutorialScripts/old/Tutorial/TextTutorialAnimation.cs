using System.Collections;
using TMPro;
using UnityEngine;

public class TextTutorialAnimation : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    [SerializeField] private float startDelay = 1f;
    [SerializeField] private float disappearTime = 1f;
    [SerializeField] private float appearTimeDuration = 1f;
    [SerializeField] private float textApha = 1f;
    [SerializeField] private float delay = 1f;

    public TutorialHandAnimation TutorialHandAnimation;

    private PanelLogoTutorialAnimation panelLogo;
    private SlingshotScript slingshot;

    private void Start()
    {

        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.alpha = 0;
        panelLogo = PanelLogoTutorialAnimation.Instance;
        slingshot = SlingshotScript.Instance;
        StartCoroutine(FadeText());
    }

    private IEnumerator FadeText()
    {

        yield return new WaitForSeconds(startDelay);
        
        yield return StartCoroutine(FadeInText("Hello"));
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(FadeOutText());

        yield return new WaitForSeconds(delay);

        yield return StartCoroutine(FadeInText("Welcome to"));
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(FadeOutText());

        yield return new WaitForSeconds(delay);

        textMeshPro.color = new Color(1f, 167f / 255f, 1f);
        yield return StartCoroutine(FadeInText("Luminous\nBlocks", panelLogo));
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(FadeOutText(panelLogo));

        yield return new WaitForSeconds(delay * 2);

        yield return StartCoroutine(FadeInSprite(slingshot));
        TutorialHandAnimation.gameObject.SetActive(true);
        slingshot.enabled = true;
        yield return new WaitForSeconds(delay * 4);
        MovePlayer(new Vector3(-16.23f, -1.57f, 0f), 3);
        while (true)
        {

            if (GameObject.FindGameObjectsWithTag("Ball").Length != 0 && slingshot.isAiming == false)
                slingshot.enabled = false;

            yield return new WaitForFixedUpdate();
        }

    }

    private IEnumerator FadeInText(string text, PanelLogoTutorialAnimation panelLogo = null)
    {
        textMeshPro.text = text;
        for (float t = 0.01f; t < appearTimeDuration; t += Time.deltaTime)
        {
            textMeshPro.alpha = Mathf.Lerp(0, textApha, t / appearTimeDuration);
            if (panelLogo != null)
            {
                panelLogo.ChangeAlphaInPanel(Mathf.Lerp(0, textApha, t / appearTimeDuration));
            }
            yield return null;
        }
    }

    private IEnumerator FadeOutText(PanelLogoTutorialAnimation panelLogo = null)
    {
        for (float t = 0.01f; t < disappearTime; t += Time.deltaTime)
        {
            textMeshPro.alpha = Mathf.Lerp(textApha, 0, t / disappearTime);
            if (panelLogo != null)
            {
                panelLogo.ChangeAlphaInPanel(Mathf.Lerp(textApha, 0, t / disappearTime));
            }
            yield return null;
        }
        if (panelLogo != null)
        {
            Destroy(panelLogo.gameObject);
        }
    }

    private IEnumerator FadeInSprite(SlingshotScript slingshot)
    {
        SpriteRenderer spriteRenderer = slingshot.GetComponent<SpriteRenderer>();
        Color spriteColor = spriteRenderer.color;
        for (float t = 0.01f; t < appearTimeDuration; t += Time.deltaTime)
        {
            float newAlpha = Mathf.Lerp(0, textApha, t / appearTimeDuration);
            spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, newAlpha);
            yield return null;
        }
    }
    private IEnumerator MovePlayer(Vector3 target, float duration)
    {
        Vector3 initialPosition = slingshot.transform.position;
        for (float t = 0.01f; t < duration; t += Time.deltaTime)
        {
            slingshot.transform.position = Vector3.Lerp(initialPosition, target, t / duration);
            yield return null;
        }
        transform.position = target; // Ensure the final position is the target position
    }
}

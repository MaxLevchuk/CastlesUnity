using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialAnimator : MonoBehaviour
{
    public TutorialHandAnimation tutorialHandObject;
    public TutorialSlingshot tutorialSlingshotScript;
    private Animator animator;

 
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ShootEnabled()
    {
        tutorialSlingshotScript.canShoot = true;
    }
    public void ShootDisabled()
    {
        tutorialSlingshotScript.canShoot = false;
    }

    public void OneShoot()
    {
        StartCoroutine(ShootCheck());
    }

    public void ShowHand()  
    {
        tutorialHandObject.HandEnable();


    }

    private IEnumerator ShootCheck()
    {
        while (true)
        {

            if (GameObject.FindGameObjectsWithTag("Ball").Length != 0 && tutorialSlingshotScript.isAiming == false)

            { 
                tutorialSlingshotScript.canShoot = false;
                animator.SetBool("tutorial1", true);
                yield break;



            }


            yield return new WaitForFixedUpdate();
        }
        
    }

/*    private IEnumerator FadeInSlinghsot(SlingshotScript slingshot)
    {
        SpriteRenderer spriteRenderer = slingshot.GetComponent<SpriteRenderer>();
        Color spriteColor = spriteRenderer.color;
        for (float t = 0.01f; t < appearTimeDuration; t += Time.deltaTime)
        {
            float newAlpha = Mathf.Lerp(0, textApha, t / appearTimeDuration);
            spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, newAlpha);
            yield return null;
        }
    }*/
}

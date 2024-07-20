using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLogoTutorialAnimation : MonoBehaviour
{
    public static PanelLogoTutorialAnimation Instance { get; private set; }
   

    private void Awake()
    {
       
            Instance = this;
       
    }

    public void ChangeAlphaInPanel(float alpha)
    {
        
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }
}

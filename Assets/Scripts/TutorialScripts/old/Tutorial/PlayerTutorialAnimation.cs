using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorialAnimation : MonoBehaviour
{
    public static PlayerTutorialAnimation Instance { get; private set; }


    private void Awake()
    {

        Instance = this;

    }

    public void ChangeAlphaInPlayer(float alpha)
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

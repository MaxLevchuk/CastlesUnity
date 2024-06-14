using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsAnimation : MonoBehaviour
{
    public float colorTime = 1f;
    private float animationSpeed;
    private  SpriteRenderer starSprite;
    

    void Start()
    {

        animationSpeed = Random.Range(0.1f, 1f);

        starSprite = GetComponent<SpriteRenderer>();
   
    }


    void Update()
    {
        Color newColor = starSprite.color;    
        starSprite.color = newColor;

        float newA = colorTime * Mathf.Sin(Time.time * animationSpeed);
        newColor.a = newA;
        starSprite.color = newColor;


    }
}

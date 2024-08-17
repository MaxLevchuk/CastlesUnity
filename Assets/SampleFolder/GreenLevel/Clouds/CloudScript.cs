using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Random.Range(200,255));
    }
    private void FixedUpdate()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        if (transform.position.x == -30)
        {
            Destroy(gameObject);
        }
    }
}

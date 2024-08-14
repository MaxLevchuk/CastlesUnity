using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private void FixedUpdate()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        if (transform.position.x == -30)
        {
            Destroy(gameObject);
        }
    }
}

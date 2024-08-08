using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowflake : MonoBehaviour
{
    private bool isStacked = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isStacked)
        {
            isStacked = true;
            StartCoroutine(DisappearAfterDelay());
        }
    }

    IEnumerator DisappearAfterDelay()
    {
        // Time before disappearing
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}




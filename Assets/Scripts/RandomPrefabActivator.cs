using System.Collections;
using UnityEngine;

public class RandomPrefabActivator : MonoBehaviour
{
    public GameObject prefab; 
    public float minWaitTime = 1f;
    public float maxWaitTime = 5f; 

    void Start()
    {
        StartCoroutine(ActivatePrefabAtRandomIntervals());
    }

    IEnumerator ActivatePrefabAtRandomIntervals()
    {
        while (true)
        {
        
            float waitTime = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(waitTime);    
            Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

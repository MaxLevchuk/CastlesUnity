using UnityEngine;

public class PatriclesScript : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (particleSystem != null && !particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}

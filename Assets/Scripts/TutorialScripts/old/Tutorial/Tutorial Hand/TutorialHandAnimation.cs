using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandAnimation : MonoBehaviour
{
    public void HandEnable()
    {
        gameObject.SetActive(true);
    }
    public void HandDisable()
    {
        gameObject.SetActive(false);
    }
}

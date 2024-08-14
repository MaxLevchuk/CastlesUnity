using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Добавьте это пространство имен

public class ButtonHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // Добавьте интерфейсы
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Реализуйте методы интерфейсов
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("isHovering", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("isHovering", false);
    }
}

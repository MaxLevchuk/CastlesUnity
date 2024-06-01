using UnityEngine;

public class RotateTowardsCursor : MonoBehaviour
{
    // Скорость вращения объекта
    public float rotationSpeed = 5f;

    // Update вызывается каждый кадр
    void Update()
    {
        // Получаем позицию курсора в мировых координатах
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Убираем z координату, чтобы объект не вращался вокруг оси z
        mousePosition.z = 0f;

        // Находим вектор, указывающий от объекта к позиции курсора
        Vector3 direction = mousePosition - transform.position;

        // Находим угол между текущим направлением объекта и направлением к курсору
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Создаем новую кватернионную переменную для вращения объекта
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Плавно поворачиваем объект к курсору
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}

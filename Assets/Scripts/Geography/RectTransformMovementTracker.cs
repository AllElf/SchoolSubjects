using UnityEngine;

public class RectTransformMovementTracker : MonoBehaviour
{
    public RectTransform target; // RectTransform объекта, который нужно отслеживать
    public float sensitivity = 0.01f; // Порог чувствительности для определения движения

    private Vector2 previousPosition;
    public bool isMoving = false;

    void Start()
    {
        if (target == null)
        {
            target = GetComponent<RectTransform>();
        }

        if (target != null)
        {
            previousPosition = target.anchoredPosition;
        }
    }

    void Update()
    {
        if (target == null) return;

        // Текущее положение RectTransform
        Vector2 currentPosition = target.anchoredPosition;

        // Проверка на изменение позиции
        if (Vector2.Distance(currentPosition, previousPosition) > sensitivity)
        {
            if (!isMoving)
            {
                isMoving = true;
                //Debug.Log("Объект начал перемещаться");
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                //Debug.Log("Объект находится в покое");
            }
        }

        // Обновление предыдущей позиции
        previousPosition = currentPosition;
    }
}

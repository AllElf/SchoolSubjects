using UnityEngine; // Подключение библиотеки UnityEngine

public class RectTransformDragTracker : MonoBehaviour // Определение класса, наследующегося от MonoBehaviour
{
    public RectTransform target; // Объявление публичной переменной типа RectTransform для отслеживаемого объекта
    public float sensitivity = 0.01f; // Объявление публичной переменной для порога чувствительности движения

    private Vector2 startPosition; // Объявление приватной переменной для хранения начальной позиции объекта
    public bool wasMoved = false; // Объявление публичной булевой переменной для фиксации перемещения

    void Start() // Метод, вызываемый при старте
    {
        if (target == null) // Проверка, если target не задан
        {
            target = GetComponent<RectTransform>(); // Получение компонента RectTransform для текущего объекта
        }
    }

    void Update() // Метод, вызываемый каждый кадр
    {
        if (target == null) return; // Если target не задан, выход из метода

        // Нажатие кнопки мыши
        if (Input.GetMouseButtonDown(0)) // Проверка, нажата ли левая кнопка мыши
        {
            startPosition = target.anchoredPosition; // Сохранение текущей позиции объекта
            wasMoved = false; // Сброс флага перемещения
        }

        // Удержание кнопки мыши
        if (Input.GetMouseButton(0)) // Проверка, удерживается ли левая кнопка мыши
        {
            Vector2 currentPosition = target.anchoredPosition; // Сохранение текущей позиции объекта

            // Проверка на перемещение
            if (Vector2.Distance(currentPosition, startPosition) > sensitivity) // Проверка, переместился ли объект больше, чем на чувствительность
            {
                wasMoved = true; // Установка флага перемещения
            }
        }

        // Отпускание кнопки мыши
        // if (Input.GetMouseButtonUp(0)) // Проверка, отпущена ли левая кнопка мыши
        // {
        //     if (wasMoved) // Проверка, был ли объект перемещён
        //     {
        //         Debug.Log("Объект был перемещён во время удержания кнопки мыши."); // Вывод сообщения в консоль, если объект был перемещён
        //     }
        //     else
        //     {
        //         Debug.Log("Объект не двигался."); // Вывод сообщения в консоль, если объект не двигался
        //     }
        // }
    }
}

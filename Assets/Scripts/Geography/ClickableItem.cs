using UnityEngine; // Подключение библиотеки UnityEngine

public class ClickableItem : MonoBehaviour // Определение класса, наследующегося от MonoBehaviour
{
    [SerializeField] RectTransformDragTracker rectTransformDragTracker; // Объявление приватной переменной rectTransformDragTracker с возможностью настройки в инспекторе

    private void Start() // Метод, вызываемый при старте
    {
        rectTransformDragTracker = GameObject.FindObjectOfType<RectTransformDragTracker>(); // Инициализация переменной rectTransformDragTracker поиском объекта типа RectTransformDragTracker в сцене
    }

    void OnMouseUp() // Метод, вызываемый при отпускании кнопки мыши
    {
        if (rectTransformDragTracker != null && rectTransformDragTracker.wasMoved == false) // Проверка, если rectTransformDragTracker существует и объект не перемещался
        {
            Debug.Log("Объект был нажат: " + gameObject.name); // Вывод сообщения в консоль, если объект был нажат, но не перемещался
        }
    }
}

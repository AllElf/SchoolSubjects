using UnityEngine; // Используется для взаимодействия с движком Unity

public class SpriteClickDetector : MonoBehaviour // Класс для обработки кликов по спрайту
{
    private SpriteRenderer spriteRenderer; // Ссылка на компонент SpriteRenderer

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Получаем компонент SpriteRenderer, прикрепленный к тому же объекту
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) // Проверяем, был ли клик мыши (левая кнопка мыши)
        {
            // Получаем мировые координаты клика мыши
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = 0; // Обнуляем Z координату, так как проверка ведется в 2D

            // Проверка, находится ли клик в пределах границ спрайта
            if (spriteRenderer.bounds.Contains(worldPoint))
            {
                Debug.Log("Клик по спрайту!"); // Выводим сообщение в консоль, если клик внутри границ спрайта
            }
            else
            {
                Debug.Log("Клик за пределами спрайта!"); // Выводим сообщение в консоль, если клик за границами спрайта
            }
        }
    }
}

using UnityEngine; // Используется для взаимодействия с движком Unity
using UnityEngine.EventSystems; // Используется для обработки событий ввода

public class PixelPerfectClickSpriteRenderer : MonoBehaviour, IPointerClickHandler // Класс реализует интерфейс IPointerClickHandler для обработки кликов мыши
{
    [SerializeField] private SpriteRenderer spriteRenderer; // Ссылка на компонент SpriteRenderer

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Получаем компонент SpriteRenderer, прикрепленный к тому же объекту
    }

    public void OnPointerClick(PointerEventData eventData) // Метод вызывается при клике мыши на объект
    {
        CheckTransparency(eventData); // Вызов метода для проверки прозрачности
    }

    private void CheckTransparency(PointerEventData eventData) // Метод для проверки прозрачности пикселя под курсором
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(eventData.position); // Преобразуем координаты экрана в мировые координаты
        Vector2 localPos = transform.InverseTransformPoint(worldPos); // Преобразуем мировые координаты в локальные координаты

        // Преобразуем локальные координаты в UV координаты
        Vector2 uv = new Vector2(
            localPos.x / spriteRenderer.sprite.bounds.size.x + spriteRenderer.sprite.bounds.extents.x,
            localPos.y / spriteRenderer.sprite.bounds.size.y + spriteRenderer.sprite.bounds.extents.y);

        // Преобразуем UV координаты в пиксельные координаты текстуры
        Texture2D texture = spriteRenderer.sprite.texture;
        Rect rect = spriteRenderer.sprite.rect;
        uv.x = rect.x + uv.x * rect.width;
        uv.y = rect.y + uv.y * rect.height;

        // Получаем цвет пикселя текстуры по рассчитанным координатам
        Color color = texture.GetPixel((int)uv.x, (int)uv.y);

        // Проверяем, непрозрачный ли пиксель
        if (color.a > 0) // Если альфа-канал пикселя больше 0
        {
            Debug.Log("Непрозрачный!"); // Выводим сообщение в консоль
        }
        else // Если альфа-канал пикселя равен 0
        {
            Debug.Log("Прозрачный!"); // Выводим сообщение в консоль
        }
    }
}

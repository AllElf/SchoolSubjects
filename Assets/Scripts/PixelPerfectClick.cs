using UnityEngine; // Используется для взаимодействия с движком Unity
using UnityEngine.EventSystems; // Используется для обработки событий ввода
using UnityEngine.UI; // Используется для работы с UI элементами

public class PixelPerfectClickImage : MonoBehaviour, IPointerClickHandler // Класс реализует интерфейс IPointerClickHandler для обработки кликов мыши
{
    [SerializeField] private Image image; // Ссылка на компонент Image, которую можно задать в инспекторе

    void Start()
    {
        image = GetComponent<Image>(); // Получаем компонент Image, прикрепленный к тому же объекту
    }

    public void OnPointerClick(PointerEventData eventData) // Метод вызывается при клике мыши на UI элемент
    {
        Vector2 localCursor; // Переменная для хранения локальных координат курсора
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform as RectTransform, // Преобразование transform в RectTransform
            eventData.position, // Позиция курсора мыши на экране
            eventData.pressEventCamera, // Камера, с которой было произведено нажатие
            out localCursor); // Выходная переменная для локальных координат курсора

        localCursor.x += (transform as RectTransform).pivot.x * (transform as RectTransform).rect.width; // Корректировка X координаты с учетом pivot
        localCursor.y += (transform as RectTransform).pivot.y * (transform as RectTransform).rect.height; // Корректировка Y координаты с учетом pivot

        Vector2 uv = new Vector2(
            localCursor.x / (transform as RectTransform).rect.width, // Преобразование X координаты в UV
            localCursor.y / (transform as RectTransform).rect.height); // Преобразование Y координаты в UV

        Texture2D texture = image.sprite.texture; // Получаем текстуру из спрайта
        Rect rect = image.sprite.rect; // Получаем прямоугольник спрайта
        uv.x = rect.x + uv.x * rect.width; // Преобразование UV координат в пиксельные координаты текстуры по X
        uv.y = rect.y + uv.y * rect.height; // Преобразование UV координат в пиксельные координаты текстуры по Y

        Color color = texture.GetPixel((int)uv.x, (int)uv.y); // Получаем цвет пикселя текстуры по рассчитанным координатам

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

    // Промежуточный метод для вызова OnPointerClick
    //public void HandleClick()
    //{
    //    // Создайте PointerEventData с текущими параметрами
    //    PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
    //    {
    //        position = Input.mousePosition
    //    };

    //    OnPointerClick(pointerEventData); // Вызываем метод OnPointerClick
    //}
}

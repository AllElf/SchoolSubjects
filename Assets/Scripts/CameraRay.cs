using UnityEngine; // Используется для взаимодействия с движком Unity

public class MouseRayDrawer : MonoBehaviour // Класс для отрисовки луча из камеры по направлению курсора
{
    public Camera mainCamera; // Ссылка на основную камеру

    void Update()
    {
        
            // Получаем экранные координаты курсора мыши
            Vector3 mousePosition = Input.mousePosition;

            // Преобразуем экранные координаты в мировые координаты
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red); // Длина луча 100 единиц, красный цвет
       


        if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                if (hit.collider != null) // Если луч попал в коллайдер
                {
                    SpriteRenderer spriteRenderer = hit.collider.GetComponent<SpriteRenderer>(); // Проверяем, есть ли у объекта компонент SpriteRenderer
                    if (spriteRenderer != null) // Если компонент SpriteRenderer найден
                    {
                        Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green); // Если попал в SpriteRenderer, делаем луч зелёным
                        Debug.Log("Hit a SpriteRenderer!"); // Выводим сообщение в консоль
                    }
                    else
                    {
                        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red); // Иначе, делаем луч красным
                        Debug.Log("Hit something else!"); // Выводим сообщение в консоль
                    }
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red); // Если ни во что не попал, делаем луч красным
                    Debug.Log("Hit nothing!"); // Выводим сообщение в консоль
                }
            }
        }
    
}

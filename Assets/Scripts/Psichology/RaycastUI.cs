using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class RaycastUI : MonoBehaviour
{
    // Ссылка на компонент GraphicRaycaster, который будет использоваться для RayCast
    public GraphicRaycaster raycaster;
    // Ссылка на EventSystem, необходимый для работы с PointerEventData
    public EventSystem eventSystem;
    // Переменная для хранения имени объекта, с которым столкнулся луч
    public string hitObjectName;
    //// Переменная для хранения текста компонента Text, если он есть
    //[Multiline]
    //[SerializeField] string hitObjectText;

    void Start()
    {
        // Попытка найти компоненты в сцене, если они не установлены в инспекторе
        if (raycaster == null)
        {
            raycaster = FindObjectOfType<GraphicRaycaster>();
        }
        if (eventSystem == null)
        {
            eventSystem = FindObjectOfType<EventSystem>();
        }
    }

    void Update()
    {
        // Проверяем, была ли нажата левая кнопка мыши
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            // Создаем новый объект PointerEventData с текущей позицией мыши
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            // Список для хранения результатов RayCast
            List<RaycastResult> results = new List<RaycastResult>();
            // Выполняем RayCast с использованием GraphicRaycaster
            raycaster.Raycast(pointerEventData, results);

            // Проверяем, что список не пустой
            if (results.Count > 0)
            {
                // Получаем первый элемент списка
                RaycastResult firstResult = results[0];
                // Сохраняем имя объекта в переменную
                hitObjectName = firstResult.gameObject.name;
                //// Проверяем, есть ли у объекта компонент Text
                //if (firstResult.gameObject.GetComponent<Text>() != null)
                //{
                //    // Сохраняем текст компонента в переменную
                //    hitObjectText = firstResult.gameObject.GetComponent<Text>().text;
                //}
                // Выводим имя первого объекта в консоль
                //Debug.Log(firstResult.gameObject.name);
            }
        }
    }
}

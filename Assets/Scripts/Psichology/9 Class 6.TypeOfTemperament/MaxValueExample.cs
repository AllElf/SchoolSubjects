using UnityEngine;
using System.Collections.Generic;

public class MaxValueExample : MonoBehaviour
{
    [Multiline]public string str;
    void Start()
    {
        float value1 = 5.7f;
        float value2 = 2.5f;
        float value3 = 5.7f;
        float value4 = 3.2f;
        

        List<float> values = new List<float> { value1, value2, value3, value4 };

        // ЌайдЄм максимальное значение
        float maxValue = Mathf.Max(values.ToArray());

        // —оздадим список дл€ хранени€ всех максимальных значений
        List<float> maxValues = new List<float>();

        // ѕройдемс€ по всем значени€м и добавим те, которые равны максимальному значению
        foreach (float value in values)
        {
            if (value == maxValue)
            {
                maxValues.Add(value);
            }
        }

        // ¬ыведем все максимальные значени€
        Debug.Log("ћаксимальные значени€: ");
        foreach (float maxValueItem in maxValues)
        {
            Debug.Log(maxValueItem);
            str = maxValueItem.ToString();
        }
    }
}

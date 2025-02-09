using System.Collections.Generic;     // Подключаем пространство имён, которое содержит классы для работы со списками (List и т.д.)
using UnityEngine;                   // Подключаем пространство имён UnityEngine для использования функционала Unity

public class TemperamentResult : MonoBehaviour  // Определяем класс TemperamentResult, наследуемый от MonoBehaviour
{
    [SerializeField] CountingInformationPsychology _countingInformationPsychology;
    // Поле для ссылки на компонент CountingInformationPsychology (видно в инспекторе из-за [SerializeField])

    public float _melancholic, _choleric, _sanguine, _phlegmatic;
    // Поля для хранения «сырых» значений каждого типа темперамента

    [SerializeField][Multiline] string _result;
    // Строка для итогового результата, которую можно редактировать в инспекторе и писать в несколько строк

    private void Start()
    {
        _countingInformationPsychology = GameObject.FindObjectOfType<CountingInformationPsychology>();
        // Находим в текущей сцене объект CountingInformationPsychology и сохраняем ссылку на него в переменную
    }

    public void Definition()
    {
        List<float> values = new List<float> { _melancholic, _choleric, _sanguine, _phlegmatic };
        // Создаём список, который содержит значения всех четырёх темпераментов

        // Найдём максимальное значение
        float maxValue = Mathf.Max(values.ToArray());
        // Используем статический метод Mathf.Max, передав список как массив (ToArray())

        // Создадим список для хранения всех максимальных значений
        List<float> maxValues = new List<float>();

        // Выведем все переменные, которые имеют максимальное значение
        foreach (float value in values)
        {
            if (value == maxValue)
            {
                maxValues.Add(value);
            }
            foreach (float maxValueItem in maxValues)
            {
                Debug.Log(maxValueItem);
                // Выводим текущее максимальное значение в консоль (Unity Console)

                _countingInformationPsychology._answer = maxValueItem.ToString();
                // Сохраняем это максимальное значение в переменную _answer
            }
        }
    }// Ненужный метод (создан просто для наглядного примера

    public void CalculateTemperamentPercentage()
    {
        // Рассчитываем общий балл (сумму всех темпераментов)
        float total = _melancholic + _choleric + _sanguine + _phlegmatic;

        // Проверяем, что общий балл не равен нулю, чтобы избежать деления на ноль
        if (total == 0)
        {
            Debug.Log("Ошибка: Общий балл равен нулю.");
            return;  // Прекращаем метод, если нет смысла считать проценты
        }

        // Рассчитываем проценты для каждого темперамента
        float melancholicPercentage = (_melancholic / total) * 100;
        float cholericPercentage = (_choleric / total) * 100;
        float sanguinePercentage = (_sanguine / total) * 100;
        float phlegmaticPercentage = (_phlegmatic / total) * 100;

        // Округляем результаты до десятых и добавляем их в _answer
        _countingInformationPsychology._answer += $"Меланхолик: {melancholicPercentage = Mathf.Round(melancholicPercentage * 10f) / 10f}%\n";
        _countingInformationPsychology._answer += $"Холерик: {cholericPercentage = Mathf.Round(cholericPercentage * 10f) / 10f}%\n";
        _countingInformationPsychology._answer += $"Сангвиник: {sanguinePercentage = Mathf.Round(sanguinePercentage * 10f) / 10f}%\n";
        _countingInformationPsychology._answer += $"Флегматик: {phlegmaticPercentage = Mathf.Round(phlegmaticPercentage * 10f) / 10f}%";

        // Определяем максимальное из четырёх значений темпераментов
        float maxValue = Mathf.Max(_melancholic, _choleric, _sanguine, _phlegmatic);

        // Округляем максимальное значение до целого и сохраняем в _scores
        _countingInformationPsychology._scores = Mathf.RoundToInt(maxValue);
    }
}

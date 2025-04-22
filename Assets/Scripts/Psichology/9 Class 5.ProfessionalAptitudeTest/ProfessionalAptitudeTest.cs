using UnityEngine;
using UnityEngine.UI; // Подключаем UI для работы с кнопками
using System.Collections.Generic;
using System.Linq;

public class ProfessionalAptitudeTest : MonoBehaviour
{
    [SerializeField] int _workingWithPeople;
    [SerializeField] int _intellectualWork;
    [SerializeField] int _practicalActivity;
    [SerializeField] int _aestheticActivities;
    [SerializeField] int _extremeActivities;
    [SerializeField] int _economicActivities;

    void Start() // Автоматически вызывается при запуске сцены
    {
        Debug.Log("Скрипт запущен. Нажми кнопку, чтобы выполнить тест.");
    }

    public void FindExtremesWrapper() // Этот метод вызовется кнопкой
    {
        Dictionary<string, int> values = new Dictionary<string, int>
        {
            { "склонность к работе с людьми", _workingWithPeople },
            { "склонность к исследовательской (интеллектуальной) работе", _intellectualWork },
            { "склонность к практической деятельности", _practicalActivity },
            { "склонность к эстетическим видам деятельности", _aestheticActivities },
            { "склонность к экстремальным видам деятельности", _extremeActivities },
            { "склонность к планово-экономическим видам деятельности", _economicActivities }
        };

        FindExtremes(values); // Вызываем анализ значений
    }

    void FindExtremes(Dictionary<string, int> data)
    {
        if (data.Count == 0)
        {
            Debug.LogError("Ошибка: Входные данные пусты.");
            return;
        }

        int maxValue = data.Values.Max();
        int minValue = data.Values.Min();

        var maxCategories = data.Where(pair => pair.Value == maxValue).Select(pair => pair.Key);
        var minCategories = data.Where(pair => pair.Value == minValue).Select(pair => pair.Key);

        Debug.Log($"Максимальное значение: {maxValue}, Категории: {string.Join(", ", maxCategories)}");
        Debug.Log($"Минимальное значение: {minValue}, Категории: {string.Join(", ", minCategories)}");
    }
}
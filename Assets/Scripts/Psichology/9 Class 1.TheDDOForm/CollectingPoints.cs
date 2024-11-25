using System;
using UnityEngine;
using UnityEngine.UI;

public class CollectingPoints : MonoBehaviour
{
    [Header("Числовые переменные для каждого типа личности")]
    public float _countH_H;
    public float _countH_T;
    public float _countH_I;
    public float _countH_N;
    public float _countH_S;
    public float _countResult;

    [Header("Типы личности")] // Создание строковых переменных для каждого значения
    [SerializeField] string typeH_H = "человек-человек";
    [SerializeField] string typeH_T = "человек-техника";
    [SerializeField] string typeH_I = "человек-художественный образ";
    [SerializeField] string typeH_N = "человек-природа";
    [SerializeField] string typeH_S = "человек-знаковая система";
    [SerializeField][Multiline] string decoding;

    [Header("Расшифровка типов личности")]
    [SerializeField]
    [Multiline]
    string descriptionH_H =
        "Главное содержание труда в профессиях типа «человек-человек» сводится к взаимодействию между людьми.\n" +
        "Если не наладится это взаимодействие, значит, не наладится и работа.\n" +
        "Качества, необходимые для работы с людьми:\n" +
        "- устойчивое, хорошее настроение в процессе работы с людьми,\n" +
        "- потребность в общении,\n" +
        "- способность мысленно ставить себя на место другого человека,\n" +
        "- быстро понимать намерения, помыслы, настроение людей,\n" +
        "- умение разбираться в человеческих взаимоотношениях,\n" +
        "- хорошая память (умение держать в уме имена и особенности многих людей),\n" +
        "- терпение.";

    [SerializeField]
    [Multiline]
    string descriptionH_T =
        "Особенность технических объектов в том, что они, как правило, могут быть точно измерены по многим признакам.\n" +
        "При их обработке, преобразовании, перемещении или оценке от работника требуется точность, определенность действий.\n" +
        "Техника как предмет труда представляет широкие возможности для новаторства, выдумки, творчества.\n" +
        "Важное значение приобретает практическое мышление и техническая фантазия.";

    [SerializeField]
    [Multiline]
    string descriptionH_I =
        "Важнейшие требования, которые предъявляют профессии, связанные с изобразительной, музыкальной, литературно-художественной, актерско-сценической деятельностью человека:\n" +
        "- наличие способности к искусствам,\n" +
        "- творческое воображение,\n" +
        "- образное мышление,\n" +
        "- талант и трудолюбие.";

    [SerializeField]
    [Multiline]
    string descriptionH_N =
        "Представителей профессий типа «человек-природа» объединяет любовь к природе.\n" +
        "Однако это любовь деятельная, связанная с познанием её законов и применением их.\n" +
        "Специалист должен:\n" +
        "- регулярно ухаживать за живыми организмами,\n" +
        "- прогнозировать изменения,\n" +
        "- принимать меры по уходу и лечению.\n" +
        "Требуются инициативность, заботливость, терпение и дальновидность.";

    [SerializeField]
    [Multiline]
    string descriptionH_S =
        "Мы встречаемся со знаками значительно чаще, чем обычно представляем себе.\n" +
        "Цифры, коды, языки, чертежи, таблицы и формулы — всё это знаки.\n" +
        "Для успешной работы важно:\n" +
        "- абстрагироваться от реальных физических свойств предметов,\n" +
        "- сосредотачиваться на сведениях, которые несут знаки,\n" +
        "- погружаться в мир условных обозначений.";

    [Header("Результат")] // Инициализируем переменную, чтобы хранить результат
    [SerializeField][Multiline] string resultType = "";

    [Header("Максимальное значение среди всех типов")] // Максимальное значение среди всех типов
    [SerializeField] float maxValue;

    [SerializeField] Text _textResultType;

    [Header("Определение главного скрипта для отправки информации")]
    [SerializeField] CountingInformationPsychology _countingInformationPsychology;

    private void Awake()
    {
        _textResultType = GameObject.Find("Text (resultType)").GetComponent<Text>();
        _textResultType.text = "";
    }
    private void Start()
    {
        
        _countingInformationPsychology = GameObject.FindObjectOfType<CountingInformationPsychology>();
    }

    public void Push()
    {
        // Максимальное значение среди всех типов
        maxValue = Mathf.Max(_countH_H, _countH_T, _countH_I, _countH_N, _countH_S);

        // Проверяем каждое значение и добавляем его тип в результат, если оно равно maxValue
        if (_countH_H == maxValue) resultType += typeH_H + "\n";
        if (_countH_T == maxValue) resultType += typeH_T + "\n";
        if (_countH_I == maxValue) resultType += typeH_I + "\n";
        if (_countH_N == maxValue) resultType += typeH_N + "\n";
        if (_countH_S == maxValue) resultType += typeH_S + "\n";

        // Выводим результат
        Debug.Log("Результат:\n" + resultType);

        // Выводим расшифровку для каждого типа в результате
        if (resultType.Contains(typeH_H)) _textResultType.text += descriptionH_H + "\n\n"; Debug.Log(descriptionH_H);
        if (resultType.Contains(typeH_T)) _textResultType.text += descriptionH_T + "\n\n"; Debug.Log(descriptionH_T);
        if (resultType.Contains(typeH_I)) _textResultType.text += descriptionH_I + "\n\n"; Debug.Log(descriptionH_I);
        if (resultType.Contains(typeH_N)) _textResultType.text += descriptionH_N + "\n\n"; Debug.Log(descriptionH_N);
        if (resultType.Contains(typeH_S)) _textResultType.text += descriptionH_S + "\n\n"; Debug.Log(descriptionH_S);

        _countingInformationPsychology._scores = Convert.ToInt32(maxValue);
        _countingInformationPsychology._answer = resultType;
    }
}

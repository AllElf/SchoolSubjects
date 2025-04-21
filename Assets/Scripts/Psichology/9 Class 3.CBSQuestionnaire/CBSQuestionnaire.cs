using UnityEngine;
using UnityEngine.UI;

public class CBSQuestionnaire : MonoBehaviour
{
    [SerializeField] CountingInformationPsychology _countingInformationPsychology;
    [SerializeField] private int _communicative;
    [SerializeField] private int _organizational;
    [SerializeField] private int _scores;
    [Multiline] public string result;
    [SerializeField] private Text skillText; // UI-элемент Text

    private void Start()
    {
        _countingInformationPsychology = GetComponent<CountingInformationPsychology>(); 
        UpdateSkillText();
    }

    public void Communicative()
    {
        _communicative++;
        _scores = _communicative + _organizational;
        UpdateSkillText();   
    }

    public void Organizational()
    {
        _organizational++;
        _scores = _communicative + _organizational;
        UpdateSkillText();
    }

    private void UpdateSkillText()
    {
        result = EvaluateSkillLevel(_communicative, _organizational);
        skillText.text = result;
    }

    public string EvaluateSkillLevel(int communicative, int organizational)
    {
        string communicativeLevel = DetermineCommunicativeLevel(communicative);
        string organizationalLevel = DetermineOrganizationalLevel(organizational);
        return $"Коммуникативные  склонности: {communicativeLevel}\nОрганизаторские склонности: {organizationalLevel}";
    }

    private string DetermineCommunicativeLevel(int communicative)
    {
        if (communicative <= 5) return "Очень низкий уровень";
        if (communicative <= 9) return "Низкий уровень";
        if (communicative <= 13) return "Средний уровень";
        if (communicative <= 15) return "Высокий уровень";
        if (communicative <= 20) return "Очень высокий уровень";
        return "Ошибка: некорректное значение";
    }

    private string DetermineOrganizationalLevel(int organizational)
    {
        if (organizational <= 7) return "Очень низкий уровень";
        if (organizational <= 11) return "Низкий уровень";
        if (organizational <= 14) return "Средний уровень";
        if (organizational <= 16) return "Высокий уровень";
        if (organizational <= 20) return "Очень высокий уровень";
        return "Ошибка: некорректное значение";
    }
    public void Result()
    {
        if (_countingInformationPsychology != null)
        {
            _countingInformationPsychology._scores = _scores;
            _countingInformationPsychology._answer = result;
        }

    }
}
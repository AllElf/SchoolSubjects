using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ProfessionalAptitudeTest : MonoBehaviour
{
    [SerializeField] CountingInformationPsychology _countingInformationPsychology;

    [SerializeField] private int _workingWithPeople;
    [SerializeField] private int _intellectualWork;
    [SerializeField] private int _practicalActivity;
    [SerializeField] private int _aestheticActivities;
    [SerializeField] private int _extremeActivities;
    [SerializeField] private int _economicActivities;

    [SerializeField] private GameObject[] _workingWithPeopleObj;
    [SerializeField] private GameObject[] _intellectualWorkObj;
    [SerializeField] private GameObject[] _practicalActivityObj;
    [SerializeField] private GameObject[] _aestheticActivitiesObj;
    [SerializeField] private GameObject[] _extremeActivityObj;
    [SerializeField] private GameObject[] _economicActivityObj;

    [SerializeField] GameObject _panel;
    [SerializeField] Text text;
    [Multiline] public string _inclination;
    public int _scores;
    [SerializeField] int _allScore;
    [SerializeField] bool _enabled;

    
    void Start()
    {
        _countingInformationPsychology = FindObjectOfType<CountingInformationPsychology>();
        _workingWithPeopleObj = GameObject.FindGameObjectsWithTag("WorkingWithPeople");
        _intellectualWorkObj = GameObject.FindGameObjectsWithTag("IntellectualWork");
        _practicalActivityObj = GameObject.FindGameObjectsWithTag("PracticalActivity");
        _aestheticActivitiesObj = GameObject.FindGameObjectsWithTag("AestheticActivities");
        _extremeActivityObj = GameObject.FindGameObjectsWithTag("ExtremeActivities");
        _economicActivityObj = GameObject.FindGameObjectsWithTag("EconomicActivities");
        _enabled = false;
        _panel.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (_enabled == false)
        {
            _allScore = _workingWithPeople + _intellectualWork +
                _practicalActivity + _aestheticActivities + _extremeActivities +
                _economicActivities;
        }
        if (_allScore == 24 && _enabled == false)
        {
            _panel.SetActive(true);
            FindExtremesWrapper();
            text.text = _inclination;
            _countingInformationPsychology._answer = _inclination;
            _countingInformationPsychology._scores = _scores;
            _enabled = true;
        }
    }
    private int CountActiveToggles(GameObject[] objects)
    {
        return objects.Count(obj => obj.GetComponent<Toggle>().isOn);
    }

    public void WorkingWithPeople() => _workingWithPeople = CountActiveToggles(_workingWithPeopleObj);
    public void IntellectualWork() => _intellectualWork = CountActiveToggles(_intellectualWorkObj);
    public void PracticalActivity() => _practicalActivity = CountActiveToggles(_practicalActivityObj);
    public void AestheticActivities() => _aestheticActivities = CountActiveToggles(_aestheticActivitiesObj);
    public void ExtremeActivities() => _extremeActivities = CountActiveToggles(_extremeActivityObj);
    public void EconomicActivities() => _economicActivities = CountActiveToggles(_economicActivityObj);

    public void FindExtremesWrapper()
    {
        Dictionary<string, int> values = new Dictionary<string, int>
        {
            { "Склонность к работе с людьми", _workingWithPeople },
            { "Склонность к исследовательской (интеллектуальной) работе", _intellectualWork },
            { "Склонность к практической деятельности", _practicalActivity },
            { "Склонность к эстетическим видам деятельности", _aestheticActivities },
            { "Склонность к экстремальным видам деятельности", _extremeActivities },
            { "Склонность к планово-экономическим видам деятельности", _economicActivities }
        };

        FindExtremes(values);
    }

    private void FindExtremes(Dictionary<string, int> data)
    {
        if (data.Count == 0)
        {
            Debug.LogError("Ошибка: Входные данные пусты.");
            return;
        }

        var maxCategories = data.Where(pair => pair.Value == data.Values.Max()).Select(pair => pair.Key);
        var minCategories = data.Where(pair => pair.Value == data.Values.Min()).Select(pair => pair.Key);

        Debug.Log($"Максимальные: {string.Join(", ", maxCategories)} ({data.Values.Max()})");
        Debug.Log($"Минимальные: {string.Join(", ", minCategories)} ({data.Values.Min()})");

        _scores = data.Values.Max();
        if (_scores != 0)
        {
            _inclination = $"{string.Join(", ", maxCategories)}\nНабранный былл по склонностям: {_scores}";
        }
        else
        {
            _inclination = "Ошибка: Входные данные пусты.";
        }
            
    }
}
using UnityEngine; // ����������� ���������� UnityEngine, ������� ������������� �������� ������ � ������� ��� ������ � Unity.
using UnityEngine.UI; // ����������� ���������� UnityEngine.UI ��� ������ � ���������� ����������������� ���������� (UI), ������ ��� ��������� ����.

public class GeographyUzbekistan : MonoBehaviour // ���������� ���������� ������ GeographyUzbekistan, ������� ��������� MonoBehaviour. ��� �������� �����, ������� ����� �������������� � Unity ��� ���������.
{
    public string _currentCity; // ���������� ��� �������� �������� �������� ���������� ������.
    public string[] _allCities; // ������ ��� �������� �������� ���� �������, ������� ����� ����� � ����.
    public int _count = 0; // ����������-������� ��� ������������ �������� ������� ������, ������� ����� ������ �����.
    public int _scores = 0; // ���������� ��� �������� �������� ���������� ����� ������.
    [SerializeField] GameObject[] _nameCities; // ������ �������� GameObject, �������������� ������ �� ����� (��������, ����� �� �����).
    [SerializeField] Text _cityFind; // ������ �� UI ������� ������, ������� ����� ���������� �������� ������, ������� ����� �����.
    [SerializeField] Text _right_Wrong; // ������ �� UI ������� ������, ������� ����� ���������� ���������� � ������������ ������.
    [SerializeField] Text _textScores; // ������ �� UI ������� ������, ������� ����� ���������� ���������� ����� ������.

    private void Start() // �����, ������� ���������� ��� ������ ����� (������������� �����������).
    {
        _nameCities = GameObject.FindGameObjectsWithTag("NameCity"); // ������� ��� ������� � ����� "NameCity" � ��������� �� � ������ _nameCities.
        if (_nameCities != null) // ��������, ��� ������ _nameCities �� ������.
        {
            for (int j = 0; j < _nameCities.Length; j++) // ���� �� ���� �������� _nameCities.
            {
                _nameCities[j].SetActive(false); // ����������� ������� ������� (������ ��� ���������).
            }
        }
        ClickOnTheCountry[] cities = GameObject.FindObjectsOfType<ClickOnTheCountry>(); // ������� ��� ������� � ����������� ClickOnTheCountry � ��������� �� � ������ cities.
        _allCities = new string[cities.Length]; // ������������� ������� _allCities �������� � ���������� ��������� �������.
        for (int i = 0; i < cities.Length; i++) // ���� �� ���� ��������� �������.
        {
            _allCities[i] = cities[i].GetComponent<ClickOnTheCountry>()._nameCityOrCountry; // ���������� �������� ������� �� ����������� ClickOnTheCountry � ���������� �� � ������ _allCities.
        }
        _cityFind.text = "����� " + _allCities[_count]; // ��������� ������ � UI �������� _cityFind �� ������ ������� (����� ������ �����).
        _right_Wrong.text = ""; // ������� ������ � UI �������� _right_Wrong (���������� � ������������ ������).
    }

    void SCORES() // ����� ��� ���������� ������ � ����������� �����.
    {
        if (_textScores != null) // ��������, ��� UI ������� _textScores �� ������.
        {
            _textScores.text = _scores.ToString(); // ���������� ������ UI �������� _textScores ������� ����������� ����� ������.
        }
    }

    private void FixedUpdate() // �����, ���������� ����� ������������� ��������� �������, ������������ ��� ���������� ��������� ����.
    {
        if (_count < _allCities.Length) // ��������, ��� ��� ���� ������, ������� ����� �����.
        {
            _cityFind.text = "����� " + _allCities[_count]; // ���������� ������ UI �������� _cityFind �� ��������� �����, ������� ����� �����.
        }
        else
        {
            _cityFind.text = "�������! "; // ���� ��� ������ �������, ������������ ��������� "�������!".
        }
        SCORES(); // ����� ������ SCORES ��� ���������� ���������� �����.
    }

    public void Check() // ����� ��� �������� ������ ������.
    {
        if (_count < _allCities.Length && _currentCity == _allCities[_count]) // ��������, ��� ����� ��� �� ����� ��� ������ � ������� ��������� ����� ��������� � ������.
        {
            for (int j = 0; j < _nameCities.Length; j++) // ���� �� ���� �������� _nameCities.
            {
                if ("Canvas - " + _allCities[_count] == _nameCities[j].name) // ��������, ��� ��� ������� ��������� � ������� �������.
                {
                    _nameCities[j].SetActive(true); // ��������� (�����������) ������� �� �����.
                }
            }
            _count++; // ���������� �������� ��������� �������.
            _scores++; // ���������� ���������� �����.
            _right_Wrong.text = "�����"!; // ����������� ������ "�����!" � UI �������� _right_Wrong.
        }
        else if (_count < _allCities.Length && _currentCity != _allCities[_count]) // ��������, ��� ����� ��� �� ������, �� ��������� ����� ��������.
        {
            if (_scores > 0) // ���� ���� ����, ����������� ���������� �����.
            {
                _scores--;
            }
            _right_Wrong.text = "�������"!; // ����������� ������ "�������!" � UI �������� _right_Wrong.
        }
        else if (_count >= _allCities.Length) // ���� ��� ������ �������.
        {
            _right_Wrong.text = "�� ���� ��!"!; // ����������� ������ "�� ���� ��!" � UI �������� _right_Wrong.
        }
    }
}

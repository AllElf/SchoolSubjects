using UnityEngine; // Подключение библиотеки UnityEngine, которая предоставляет основные классы и функции для работы с Unity.
using UnityEngine.UI; // Подключение библиотеки UnityEngine.UI для работы с элементами пользовательского интерфейса (UI), такими как текстовые поля.

public class GeographyUzbekistan : MonoBehaviour // Объявление публичного класса GeographyUzbekistan, который наследует MonoBehaviour. Это основной класс, который будет использоваться в Unity как компонент.
{
    public string _currentCity; // Переменная для хранения названия текущего выбранного города.
    public string[] _allCities; // Массив для хранения названий всех городов, которые нужно найти в игре.
    public int _count = 0; // Переменная-счётчик для отслеживания текущего индекса города, который игрок должен найти.
    public int _scores = 0; // Переменная для хранения текущего количества очков игрока.
    [SerializeField] GameObject[] _nameCities; // Массив объектов GameObject, представляющих города на карте (например, точки на карте).
    [SerializeField] Text _cityFind; // Ссылка на UI элемент текста, который будет отображать название города, который нужно найти.
    [SerializeField] Text _right_Wrong; // Ссылка на UI элемент текста, который будет отображать информацию о правильности ответа.
    [SerializeField] Text _textScores; // Ссылка на UI элемент текста, который будет отображать количество очков игрока.

    private void Start() // Метод, который вызывается при старте сцены (инициализация компонентов).
    {
        _nameCities = GameObject.FindGameObjectsWithTag("NameCity"); // Находит все объекты с тегом "NameCity" и сохраняет их в массив _nameCities.
        if (_nameCities != null) // Проверка, что массив _nameCities не пустой.
        {
            for (int j = 0; j < _nameCities.Length; j++) // Цикл по всем объектам _nameCities.
            {
                _nameCities[j].SetActive(false); // Деактивация каждого объекта (делает его невидимым).
            }
        }
        ClickOnTheCountry[] cities = GameObject.FindObjectsOfType<ClickOnTheCountry>(); // Находит все объекты с компонентом ClickOnTheCountry и сохраняет их в массив cities.
        _allCities = new string[cities.Length]; // Инициализация массива _allCities размером в количество найденных городов.
        for (int i = 0; i < cities.Length; i++) // Цикл по всем найденным городам.
        {
            _allCities[i] = cities[i].GetComponent<ClickOnTheCountry>()._nameCityOrCountry; // Извлечение названий городов из компонентов ClickOnTheCountry и сохранение их в массив _allCities.
        }
        _cityFind.text = "Найди " + _allCities[_count]; // Установка текста в UI элементе _cityFind на первое задание (найти первый город).
        _right_Wrong.text = ""; // Очистка текста в UI элементе _right_Wrong (информация о правильности ответа).
    }

    void SCORES() // Метод для обновления текста с количеством очков.
    {
        if (_textScores != null) // Проверка, что UI элемент _textScores не пустой.
        {
            _textScores.text = _scores.ToString(); // Обновление текста UI элемента _textScores текущим количеством очков игрока.
        }
    }

    private void FixedUpdate() // Метод, вызываемый через фиксированные интервалы времени, используется для обновления состояния игры.
    {
        if (_count < _allCities.Length) // Проверка, что ещё есть города, которые нужно найти.
        {
            _cityFind.text = "Найди " + _allCities[_count]; // Обновление текста UI элемента _cityFind на следующий город, который нужно найти.
        }
        else
        {
            _cityFind.text = "Молодец! "; // Если все города найдены, отображается сообщение "Молодец!".
        }
        SCORES(); // Вызов метода SCORES для обновления количества очков.
    }

    public void Check() // Метод для проверки ответа игрока.
    {
        if (_count < _allCities.Length && _currentCity == _allCities[_count]) // Проверка, что игрок ещё не нашёл все города и текущий выбранный город совпадает с нужным.
        {
            for (int j = 0; j < _nameCities.Length; j++) // Цикл по всем объектам _nameCities.
            {
                if ("Canvas - " + _allCities[_count] == _nameCities[j].name) // Проверка, что имя объекта совпадает с текущим городом.
                {
                    _nameCities[j].SetActive(true); // Активация (отображение) объекта на карте.
                }
            }
            _count++; // Увеличение счётчика найденных городов.
            _scores++; // Увеличение количества очков.
            _right_Wrong.text = "Верно"!; // Отображение текста "Верно!" в UI элементе _right_Wrong.
        }
        else if (_count < _allCities.Length && _currentCity != _allCities[_count]) // Проверка, что город ещё не найден, но выбранный город неверный.
        {
            if (_scores > 0) // Если очки есть, уменьшается количество очков.
            {
                _scores--;
            }
            _right_Wrong.text = "Неверно"!; // Отображение текста "Неверно!" в UI элементе _right_Wrong.
        }
        else if (_count >= _allCities.Length) // Если все города найдены.
        {
            _right_Wrong.text = "На этом всё!"!; // Отображение текста "На этом всё!" в UI элементе _right_Wrong.
        }
    }
}

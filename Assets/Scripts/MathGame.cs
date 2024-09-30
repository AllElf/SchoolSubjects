using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MathGame : MonoBehaviour
{
    // UI элементы
    public Text problemText; // Текст для отображения математической задачи
    public InputField answerInput; // Поле ввода для ответа игрока
    public Button submitButton; // Кнопка для отправки ответа
    public Text feedbackText; // Текст для обратной связи (правильный/неправильный ответ)
    public Dropdown difficultyDropdown; // Выпадающий список для выбора уровня сложности
    public Dropdown problemTypeDropdown; // Выпадающий список для выбора типа задачи
    [SerializeField] Timer _timer;
    public int _scores = 0;
    [SerializeField] Text _scoreText;

    // Внутренние переменные
    private int number1; // Первое число задачи
    private int number2; // Второе число задачи
    private int correctAnswer; // Правильный ответ на задачу
    private string selectedProblemType; // Выбранный тип задачи (сложение, вычитание и т.д.)
    private string selectedDifficulty; // Выбранный уровень сложности
    [SerializeField] bool _enable = false;

    void Start()
    {
        if (GameObject.FindObjectOfType<Timer>() != null)
        {
            _timer = GameObject.FindObjectOfType<Timer>();
        }
        _enable = false;
        // Установить обработчики событий для элементов UI
        submitButton.onClick.AddListener(CheckAnswer); // Добавляем обработчик для кнопки отправки ответа
        difficultyDropdown.onValueChanged.AddListener(delegate { UpdateDifficulty(); }); // Добавляем обработчик для изменения уровня сложности
        problemTypeDropdown.onValueChanged.AddListener(delegate { UpdateProblemType(); }); // Добавляем обработчик для изменения типа задачи

        // Инициализация значений
        UpdateDifficulty(); // Инициализация уровня сложности
        UpdateProblemType(); // Инициализация типа задачи
        GenerateProblem(); // Генерация первой задачи
    }
   
    void FixedUpdate()
    {
        _scoreText.text = _scores.ToString();
        if (_timer != null && !_enable && _timer._image.fillAmount <= 0f)
        {
            if (answerInput.text != correctAnswer.ToString())
            {
                _scores--;
                if (_scores <= 0)
                {
                    _scores = 0;
                }
            }
            else if (answerInput.text == correctAnswer.ToString())
            {
                _scores++;
            }
            GenerateProblem(); // Генерация новой задачи
            
            feedbackText.text = "Время вышло! Следующее слово!";
           _enable = true;
        }
        Check();
    }
    void Check()
    {
        if (_timer != null && _timer.fillAmounT <= 0.5f)
        {
            _enable = false;
        }
    }

    // Обновление уровня сложности на основе выбора в выпадающем списке
    void UpdateDifficulty()
    {
        selectedDifficulty = difficultyDropdown.options[difficultyDropdown.value].text; // Получаем выбранный уровень сложности
    }

    // Обновление типа задачи на основе выбора в выпадающем списке
    void UpdateProblemType()
    {
        selectedProblemType = problemTypeDropdown.options[problemTypeDropdown.value].text; // Получаем выбранный тип задачи
    }

    // Генерация новой задачи на основе выбранных параметров
    void GenerateProblem()
    {
        
        // Установка диапазона чисел в зависимости от уровня сложности
        int range = 10;
        switch (selectedDifficulty)
        {
            case "Легкий":
                range = 10; // Диапазон чисел для легкого уровня
                break;
            case "Средний":
                range = 20; // Диапазон чисел для среднего уровня
                break;
            case "Сложный":
                range = 50; // Диапазон чисел для сложного уровня
                break;
        }

        // Генерация случайных чисел в заданном диапазоне
        number1 = Random.Range(1, range);
        number2 = Random.Range(1, range);

        // Генерация задачи в зависимости от выбранного типа
        switch (selectedProblemType)
        {
            case "Сложение":
                correctAnswer = number1 + number2; // Правильный ответ для сложения
                problemText.text = number1.ToString() + " + " + number2.ToString() + " = ?"; // Текст задачи для сложения
                break;
            case "Вычитание":
                correctAnswer = number1 - number2; // Правильный ответ для вычитания
                problemText.text = number1.ToString() + " - " + number2.ToString() + " = ?"; // Текст задачи для вычитания
                break;
            case "Умножение":
                correctAnswer = number1 * number2; // Правильный ответ для умножения
                problemText.text = number1.ToString() + " * " + number2.ToString() + " = ?"; // Текст задачи для умножения
                break;
            case "Деление":
                correctAnswer = number1; // Сохранение делимого как правильного ответа
                number1 = number1 * number2; // Умножаем делимое и делитель для получения делимого
                problemText.text = number1.ToString() + " / " + number2.ToString() + " = ?"; // Текст задачи для деления
                break;
        }

        // Очистка полей ввода и обратной связи
        answerInput.text = ""; // Очистка поля ввода
        
        //feedbackText.text = ""; // Очистка текста обратной связи
        if (_timer != null)
        {
            _timer.fillAmounT = 1f;
        }
    }

    // Проверка ответа игрока
   public void CheckAnswer()
    {
        int playerAnswer; // Переменная для хранения ответа игрока
        if (int.TryParse(answerInput.text, out playerAnswer)) // Проверка, что введенный ответ является числом
        {
            if (playerAnswer == correctAnswer) // Сравнение ответа игрока с правильным ответом
            {
                feedbackText.text = "Правильно!"; // Если ответ правильный, выводим сообщение
                _scores++;
                GenerateProblem(); // Генерация новой задачи
            }
            else if (playerAnswer != correctAnswer)
            {
                if (_scores > 0)
                {
                    _scores--;
                }
                feedbackText.text = "Попробуй снова."; // Если ответ неправильный, выводим сообщение
            }
        }
        else
        {
            feedbackText.text = "Пожалуйста, введите число."; // Если введенное значение не является числом, выводим сообщение
        }

        //GenerateProblem(); // Генерация новой задачи
    }
}

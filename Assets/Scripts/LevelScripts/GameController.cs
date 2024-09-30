using System.Linq; // Для работы с LINQ
using UnityEngine;
using UnityEngine.UI;

// Основной контроллер игры, управляющий уровнями
public class GameController : MonoBehaviour
{
    [Header("Поле для отображения описания задачи")]
    public Text taskDescription; // Поле для отображения описания задачи
    [Header("Поле для отображения шаблона кода")]
    public Text codeTemplate;     // Поле для отображения шаблона кода
    [Header("Поле ввода кода игрока")]
    public InputField codeInput;  // Поле ввода кода игрока
    [Header("Кнопка отправки кода")]
    public Button submitButton;   // Кнопка отправки кода
    [Header("Поле для отображения обратной связи")]
    public Text feedbackText;     // Поле для отображения обратной связи

    [Header("Массив скриптов уровней")]
    // Массив скриптов уровней
    public LevelScript[] levelScripts;
    [Header("Текущий скрипт уровня")]
    // Текущий скрипт уровня
    public LevelScript currentLevelScript;
    [Header("Текущий уровень")]
    // Текущий уровень
    public int currentLevel = 0;
    [Header("Текущий ответ")]
    public string answer;

    [Header("Баллы")]
    [SerializeField] int scores = 0;
    [SerializeField] Text scoresText;

    [Header("Таймер")]
    [SerializeField] Timer _timer;
    [SerializeField] bool _done = false;
    [SerializeField] bool _hasExecuted = false;

    void Start()
    {
        if (GameObject.FindObjectOfType<Timer>() != null)
        {
            _timer = GameObject.FindObjectOfType<Timer>();
        }
        _done = false;
        // Инициализация уровней
        InitializeLevels();

        // Назначение метода проверки ответа на событие нажатия кнопки
        submitButton.onClick.AddListener(CheckAnswer);

        // Загрузка начального уровня
        LoadLevel(currentLevel);
    }

    // Метод инициализации уровней
    void InitializeLevels()
    {
        // Инициализация массива уровней с их задачами и шаблонами кода
        levelScripts = new LevelScript[] {
            new Level1(), // Уровень 1
            new Level2(), // Уровень 2
            new Level3(), // Уровень 3
            new Level4(), // Уровень 4
            new Level5(), // Уровень 5
            new Level6(), // Уровень 6
            new Level7(), // Уровень 7
            new Level8(), // Уровень 8
            new Level9(), // Уровень 9
            new Level10() // Уровень 10
        };
    }

    // Метод загрузки уровня
    public void LoadLevel(int level)
    {
        // Сбрасываем флаг выполнения при загрузке нового уровня
        _hasExecuted = false;

        // Проверка, существует ли уровень
        if (level < levelScripts.Length)
        {
            // Установка текущего скрипта уровня
            currentLevelScript = levelScripts[level];

            // Обновление UI элементами для текущего уровня
            taskDescription.text = currentLevelScript.taskDescription;
            codeTemplate.text = currentLevelScript.codeTemplate;
            answer = currentLevelScript.correctCodeSnippet;
            codeInput.text = ""; // Очистка поля ввода
            feedbackText.text = ""; // Очистка обратной связи
        }
        else
        {
            // Отображение сообщения о завершении игры
            taskDescription.text = "Поздравляем! Вы завершили все уровни!";
            codeTemplate.text = ""; // Очистка шаблона кода
        }
    }

    // Метод проверки ответа
    private void FixedUpdate()
    {
        scoresText.text = "Баллы: " + scores.ToString();
        if (_timer != null && _timer._image.fillAmount <= 0 && !_hasExecuted)
        {
            _hasExecuted = true; // Устанавливаем флаг, чтобы предотвратить повторное выполнение

            _done = true;
            

            if (_done)
            {
                _done = false;
                CheckAnswer();
                
                _timer._image.fillAmount = 1f; // Исправлено имя свойства fillAmount
            }
        }
    }
    public void CheckAnswer()
    {
        // Получение введенного игроком кода
        string playerCode = codeInput.text;

        // Получение правильного фрагмента кода для текущего уровня
        string correctCodeSnippet = currentLevelScript.correctCodeSnippet;

        // Проверка, содержит ли введенный код правильный фрагмент
        if (playerCode.Contains(correctCodeSnippet))
        {
            feedbackText.text = "Правильно!"; // Показать успешное сообщение
            currentLevel++; // Переход к следующему уровню
            scores++;
            LoadLevel(currentLevel); // Загрузить следующий уровень
        }
        else if (_timer != null && _timer._image.fillAmount <= 0)
        {
            currentLevel++; // Переход к следующему уровню
            LoadLevel(currentLevel); // Загрузить следующий уровень
            if (currentLevel < levelScripts.Length)
            {
                feedbackText.text = "Время вышло! Решай следующий код";
            }
            else if (currentLevel >= levelScripts.Length)
            {
                feedbackText.text = "Время вышло полностью!";
                Destroy(_timer);
            }  
        }
        else
        {
            feedbackText.text = "Попробуйте снова."; // Показать сообщение о неправильном ответе
        }
        _done = false;
    }
}

// Абстрактный базовый класс для скриптов уровней
public abstract class LevelScript
{
    // Описание задачи для уровня
    public string taskDescription;

    // Шаблон кода, который нужно дописать
    public string codeTemplate;

    // Правильный фрагмент кода, который должен быть в ответе
    public string correctCodeSnippet;
}

// Скрипт уровня 1
public class Level1 : LevelScript
{
    public Level1()
    {
        taskDescription = "Напишите программу, которая выводит 'Hello, World!'";
        codeTemplate = "using System;\n\npublic class HelloWorld\n{\n    public static void Main(string[] args)\n    {\n        // Ваш код здесь\n    }\n}";
        correctCodeSnippet = "Console.WriteLine(\"Hello, World!\");";
    }
}

// Скрипт уровня 2
public class Level2 : LevelScript
{
    public Level2()
    {
        taskDescription = "Напишите программу, которая складывает два числа и выводит результат.";
        codeTemplate = "using System;\n\npublic class AddNumbers\n{\n    public static void Main(string[] args)\n    {\n        int a = 5;\n        int b = 10;\n        int sum = 0;\n        // Ваш код здесь\n        Console.WriteLine(\"Sum: \" + sum);\n    }\n}";
        correctCodeSnippet = "sum = a + b;";
    }
}

// Скрипт уровня 3
public class Level3 : LevelScript
{
    public Level3()
    {
        taskDescription = "Напишите программу, которая находит максимум из трех чисел.";
        codeTemplate = "using System;\n\npublic class MaxNumber\n{\n    public static void Main(string[] args)\n    {\n        int x = 5;\n        int y = 10;\n        int z = 15;\n        int max = 0;\n        // Ваш код здесь\n        Console.WriteLine(\"Max: \" + max);\n    }\n}";
        correctCodeSnippet = "max = Math.Max(x, Math.Max(y, z));";
    }
}

// Скрипт уровня 4
public class Level4 : LevelScript
{
    public Level4()
    {
        taskDescription = "Напишите программу, которая вычисляет факториал числа.";
        codeTemplate = "using System;\n\npublic class Factorial\n{\n    public static void Main(string[] args)\n    {\n        int n = 5;\n        int result = 1;\n        // Ваш код здесь\n        Console.WriteLine(\"Factorial: \" + result);\n    }\n}";
        correctCodeSnippet = "for (int i = 1; i <= n; i++) { result *= i; }";
    }
}

// Скрипт уровня 5
public class Level5 : LevelScript
{
    public Level5()
    {
        taskDescription = "Напишите программу, которая проверяет, является ли число четным или нечетным.";
        codeTemplate = "using System;\n\npublic class EvenOdd\n{\n    public static void Main(string[] args)\n    {\n        int number = 8;\n        bool isEven = false;\n        // Ваш код здесь\n        Console.WriteLine(isEven ? \"Even\" : \"Odd\");\n    }\n}";
        correctCodeSnippet = "isEven = (number % 2 == 0);";
    }
}

// Скрипт уровня 6
public class Level6 : LevelScript
{
    public Level6()
    {
        taskDescription = "Напишите программу, которая находит среднее значение массива чисел.";
        codeTemplate = "using System;\n\npublic class AverageArray\n{\n    public static void Main(string[] args)\n    {\n        int[] numbers = { 1, 2, 3, 4, 5 };\n        double average = 0;\n        // Ваш код здесь\n        Console.WriteLine(\"Average: \" + average);\n    }\n}";
        correctCodeSnippet = "average = numbers.Average();";
    }
}

// Скрипт уровня 7
public class Level7 : LevelScript
{
    public Level7()
    {
        taskDescription = "Напишите программу, которая переворачивает строку.";
        codeTemplate = "using System;\n\npublic class ReverseString\n{\n    public static void Main(string[] args)\n    {\n        string input = \"Hello\";\n        string reversed = \"\";\n        // Ваш код здесь\n        Console.WriteLine(\"Reversed: \" + reversed);\n    }\n}";
        correctCodeSnippet = "reversed = new string(input.Reverse().ToArray());";
    }
}

// Скрипт уровня 8
public class Level8 : LevelScript
{
    public Level8()
    {
        taskDescription = "Напишите программу, которая проверяет, является ли число простым.";
        codeTemplate = "using System;\n\npublic class PrimeCheck\n{\n    public static void Main(string[] args)\n    {\n        int number = 17;\n        bool isPrime = true;\n        // Ваш код здесь\n        Console.WriteLine(isPrime ? \"Prime\" : \"Not Prime\");\n    }\n}";
        correctCodeSnippet = "for (int i = 2; i <= Math.Sqrt(number); i++) { if (number % i == 0) { isPrime = false; break; } }";
    }
}

// Скрипт уровня 9
public class Level9 : LevelScript
{
    public Level9()
    {
        taskDescription = "Напишите программу, которая находит сумму всех элементов в массиве.";
        codeTemplate = "using System;\n\npublic class SumArray\n{\n    public static void Main(string[] args)\n    {\n        int[] numbers = { 1, 2, 3, 4, 5 };\n        int sum = 0;\n        // Ваш код здесь\n        Console.WriteLine(\"Sum: \" + sum);\n    }\n}";
        correctCodeSnippet = "sum = numbers.Sum();";
    }
}

// Скрипт уровня 10
public class Level10 : LevelScript
{
    public Level10()
    {
        taskDescription = "Напишите программу, которая сортирует массив чисел по возрастанию.";
        codeTemplate = "using System;\n\npublic class SortArray\n{\n    public static void Main(string[] args)\n    {\n        int[] numbers = { 5, 3, 1, 4, 2 };\n        // Ваш код здесь\n        Array.Sort(numbers);\n        Console.WriteLine(\"Sorted: \" + string.Join(\", \", numbers));\n    }\n}";
        correctCodeSnippet = "Array.Sort(numbers);";
    }
}

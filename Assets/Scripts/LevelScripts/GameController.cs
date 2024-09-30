using System.Linq; // ��� ������ � LINQ
using UnityEngine;
using UnityEngine.UI;

// �������� ���������� ����, ����������� ��������
public class GameController : MonoBehaviour
{
    [Header("���� ��� ����������� �������� ������")]
    public Text taskDescription; // ���� ��� ����������� �������� ������
    [Header("���� ��� ����������� ������� ����")]
    public Text codeTemplate;     // ���� ��� ����������� ������� ����
    [Header("���� ����� ���� ������")]
    public InputField codeInput;  // ���� ����� ���� ������
    [Header("������ �������� ����")]
    public Button submitButton;   // ������ �������� ����
    [Header("���� ��� ����������� �������� �����")]
    public Text feedbackText;     // ���� ��� ����������� �������� �����

    [Header("������ �������� �������")]
    // ������ �������� �������
    public LevelScript[] levelScripts;
    [Header("������� ������ ������")]
    // ������� ������ ������
    public LevelScript currentLevelScript;
    [Header("������� �������")]
    // ������� �������
    public int currentLevel = 0;
    [Header("������� �����")]
    public string answer;

    [Header("�����")]
    [SerializeField] int scores = 0;
    [SerializeField] Text scoresText;

    [Header("������")]
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
        // ������������� �������
        InitializeLevels();

        // ���������� ������ �������� ������ �� ������� ������� ������
        submitButton.onClick.AddListener(CheckAnswer);

        // �������� ���������� ������
        LoadLevel(currentLevel);
    }

    // ����� ������������� �������
    void InitializeLevels()
    {
        // ������������� ������� ������� � �� �������� � ��������� ����
        levelScripts = new LevelScript[] {
            new Level1(), // ������� 1
            new Level2(), // ������� 2
            new Level3(), // ������� 3
            new Level4(), // ������� 4
            new Level5(), // ������� 5
            new Level6(), // ������� 6
            new Level7(), // ������� 7
            new Level8(), // ������� 8
            new Level9(), // ������� 9
            new Level10() // ������� 10
        };
    }

    // ����� �������� ������
    public void LoadLevel(int level)
    {
        // ���������� ���� ���������� ��� �������� ������ ������
        _hasExecuted = false;

        // ��������, ���������� �� �������
        if (level < levelScripts.Length)
        {
            // ��������� �������� ������� ������
            currentLevelScript = levelScripts[level];

            // ���������� UI ���������� ��� �������� ������
            taskDescription.text = currentLevelScript.taskDescription;
            codeTemplate.text = currentLevelScript.codeTemplate;
            answer = currentLevelScript.correctCodeSnippet;
            codeInput.text = ""; // ������� ���� �����
            feedbackText.text = ""; // ������� �������� �����
        }
        else
        {
            // ����������� ��������� � ���������� ����
            taskDescription.text = "�����������! �� ��������� ��� ������!";
            codeTemplate.text = ""; // ������� ������� ����
        }
    }

    // ����� �������� ������
    private void FixedUpdate()
    {
        scoresText.text = "�����: " + scores.ToString();
        if (_timer != null && _timer._image.fillAmount <= 0 && !_hasExecuted)
        {
            _hasExecuted = true; // ������������� ����, ����� ������������� ��������� ����������

            _done = true;
            

            if (_done)
            {
                _done = false;
                CheckAnswer();
                
                _timer._image.fillAmount = 1f; // ���������� ��� �������� fillAmount
            }
        }
    }
    public void CheckAnswer()
    {
        // ��������� ���������� ������� ����
        string playerCode = codeInput.text;

        // ��������� ����������� ��������� ���� ��� �������� ������
        string correctCodeSnippet = currentLevelScript.correctCodeSnippet;

        // ��������, �������� �� ��������� ��� ���������� ��������
        if (playerCode.Contains(correctCodeSnippet))
        {
            feedbackText.text = "���������!"; // �������� �������� ���������
            currentLevel++; // ������� � ���������� ������
            scores++;
            LoadLevel(currentLevel); // ��������� ��������� �������
        }
        else if (_timer != null && _timer._image.fillAmount <= 0)
        {
            currentLevel++; // ������� � ���������� ������
            LoadLevel(currentLevel); // ��������� ��������� �������
            if (currentLevel < levelScripts.Length)
            {
                feedbackText.text = "����� �����! ����� ��������� ���";
            }
            else if (currentLevel >= levelScripts.Length)
            {
                feedbackText.text = "����� ����� ���������!";
                Destroy(_timer);
            }  
        }
        else
        {
            feedbackText.text = "���������� �����."; // �������� ��������� � ������������ ������
        }
        _done = false;
    }
}

// ����������� ������� ����� ��� �������� �������
public abstract class LevelScript
{
    // �������� ������ ��� ������
    public string taskDescription;

    // ������ ����, ������� ����� ��������
    public string codeTemplate;

    // ���������� �������� ����, ������� ������ ���� � ������
    public string correctCodeSnippet;
}

// ������ ������ 1
public class Level1 : LevelScript
{
    public Level1()
    {
        taskDescription = "�������� ���������, ������� ������� 'Hello, World!'";
        codeTemplate = "using System;\n\npublic class HelloWorld\n{\n    public static void Main(string[] args)\n    {\n        // ��� ��� �����\n    }\n}";
        correctCodeSnippet = "Console.WriteLine(\"Hello, World!\");";
    }
}

// ������ ������ 2
public class Level2 : LevelScript
{
    public Level2()
    {
        taskDescription = "�������� ���������, ������� ���������� ��� ����� � ������� ���������.";
        codeTemplate = "using System;\n\npublic class AddNumbers\n{\n    public static void Main(string[] args)\n    {\n        int a = 5;\n        int b = 10;\n        int sum = 0;\n        // ��� ��� �����\n        Console.WriteLine(\"Sum: \" + sum);\n    }\n}";
        correctCodeSnippet = "sum = a + b;";
    }
}

// ������ ������ 3
public class Level3 : LevelScript
{
    public Level3()
    {
        taskDescription = "�������� ���������, ������� ������� �������� �� ���� �����.";
        codeTemplate = "using System;\n\npublic class MaxNumber\n{\n    public static void Main(string[] args)\n    {\n        int x = 5;\n        int y = 10;\n        int z = 15;\n        int max = 0;\n        // ��� ��� �����\n        Console.WriteLine(\"Max: \" + max);\n    }\n}";
        correctCodeSnippet = "max = Math.Max(x, Math.Max(y, z));";
    }
}

// ������ ������ 4
public class Level4 : LevelScript
{
    public Level4()
    {
        taskDescription = "�������� ���������, ������� ��������� ��������� �����.";
        codeTemplate = "using System;\n\npublic class Factorial\n{\n    public static void Main(string[] args)\n    {\n        int n = 5;\n        int result = 1;\n        // ��� ��� �����\n        Console.WriteLine(\"Factorial: \" + result);\n    }\n}";
        correctCodeSnippet = "for (int i = 1; i <= n; i++) { result *= i; }";
    }
}

// ������ ������ 5
public class Level5 : LevelScript
{
    public Level5()
    {
        taskDescription = "�������� ���������, ������� ���������, �������� �� ����� ������ ��� ��������.";
        codeTemplate = "using System;\n\npublic class EvenOdd\n{\n    public static void Main(string[] args)\n    {\n        int number = 8;\n        bool isEven = false;\n        // ��� ��� �����\n        Console.WriteLine(isEven ? \"Even\" : \"Odd\");\n    }\n}";
        correctCodeSnippet = "isEven = (number % 2 == 0);";
    }
}

// ������ ������ 6
public class Level6 : LevelScript
{
    public Level6()
    {
        taskDescription = "�������� ���������, ������� ������� ������� �������� ������� �����.";
        codeTemplate = "using System;\n\npublic class AverageArray\n{\n    public static void Main(string[] args)\n    {\n        int[] numbers = { 1, 2, 3, 4, 5 };\n        double average = 0;\n        // ��� ��� �����\n        Console.WriteLine(\"Average: \" + average);\n    }\n}";
        correctCodeSnippet = "average = numbers.Average();";
    }
}

// ������ ������ 7
public class Level7 : LevelScript
{
    public Level7()
    {
        taskDescription = "�������� ���������, ������� �������������� ������.";
        codeTemplate = "using System;\n\npublic class ReverseString\n{\n    public static void Main(string[] args)\n    {\n        string input = \"Hello\";\n        string reversed = \"\";\n        // ��� ��� �����\n        Console.WriteLine(\"Reversed: \" + reversed);\n    }\n}";
        correctCodeSnippet = "reversed = new string(input.Reverse().ToArray());";
    }
}

// ������ ������ 8
public class Level8 : LevelScript
{
    public Level8()
    {
        taskDescription = "�������� ���������, ������� ���������, �������� �� ����� �������.";
        codeTemplate = "using System;\n\npublic class PrimeCheck\n{\n    public static void Main(string[] args)\n    {\n        int number = 17;\n        bool isPrime = true;\n        // ��� ��� �����\n        Console.WriteLine(isPrime ? \"Prime\" : \"Not Prime\");\n    }\n}";
        correctCodeSnippet = "for (int i = 2; i <= Math.Sqrt(number); i++) { if (number % i == 0) { isPrime = false; break; } }";
    }
}

// ������ ������ 9
public class Level9 : LevelScript
{
    public Level9()
    {
        taskDescription = "�������� ���������, ������� ������� ����� ���� ��������� � �������.";
        codeTemplate = "using System;\n\npublic class SumArray\n{\n    public static void Main(string[] args)\n    {\n        int[] numbers = { 1, 2, 3, 4, 5 };\n        int sum = 0;\n        // ��� ��� �����\n        Console.WriteLine(\"Sum: \" + sum);\n    }\n}";
        correctCodeSnippet = "sum = numbers.Sum();";
    }
}

// ������ ������ 10
public class Level10 : LevelScript
{
    public Level10()
    {
        taskDescription = "�������� ���������, ������� ��������� ������ ����� �� �����������.";
        codeTemplate = "using System;\n\npublic class SortArray\n{\n    public static void Main(string[] args)\n    {\n        int[] numbers = { 5, 3, 1, 4, 2 };\n        // ��� ��� �����\n        Array.Sort(numbers);\n        Console.WriteLine(\"Sorted: \" + string.Join(\", \", numbers));\n    }\n}";
        correctCodeSnippet = "Array.Sort(numbers);";
    }
}

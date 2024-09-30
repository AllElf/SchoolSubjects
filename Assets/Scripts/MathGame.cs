using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MathGame : MonoBehaviour
{
    // UI ��������
    public Text problemText; // ����� ��� ����������� �������������� ������
    public InputField answerInput; // ���� ����� ��� ������ ������
    public Button submitButton; // ������ ��� �������� ������
    public Text feedbackText; // ����� ��� �������� ����� (����������/������������ �����)
    public Dropdown difficultyDropdown; // ���������� ������ ��� ������ ������ ���������
    public Dropdown problemTypeDropdown; // ���������� ������ ��� ������ ���� ������
    [SerializeField] Timer _timer;
    public int _scores = 0;
    [SerializeField] Text _scoreText;

    // ���������� ����������
    private int number1; // ������ ����� ������
    private int number2; // ������ ����� ������
    private int correctAnswer; // ���������� ����� �� ������
    private string selectedProblemType; // ��������� ��� ������ (��������, ��������� � �.�.)
    private string selectedDifficulty; // ��������� ������� ���������
    [SerializeField] bool _enable = false;

    void Start()
    {
        if (GameObject.FindObjectOfType<Timer>() != null)
        {
            _timer = GameObject.FindObjectOfType<Timer>();
        }
        _enable = false;
        // ���������� ����������� ������� ��� ��������� UI
        submitButton.onClick.AddListener(CheckAnswer); // ��������� ���������� ��� ������ �������� ������
        difficultyDropdown.onValueChanged.AddListener(delegate { UpdateDifficulty(); }); // ��������� ���������� ��� ��������� ������ ���������
        problemTypeDropdown.onValueChanged.AddListener(delegate { UpdateProblemType(); }); // ��������� ���������� ��� ��������� ���� ������

        // ������������� ��������
        UpdateDifficulty(); // ������������� ������ ���������
        UpdateProblemType(); // ������������� ���� ������
        GenerateProblem(); // ��������� ������ ������
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
            GenerateProblem(); // ��������� ����� ������
            
            feedbackText.text = "����� �����! ��������� �����!";
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

    // ���������� ������ ��������� �� ������ ������ � ���������� ������
    void UpdateDifficulty()
    {
        selectedDifficulty = difficultyDropdown.options[difficultyDropdown.value].text; // �������� ��������� ������� ���������
    }

    // ���������� ���� ������ �� ������ ������ � ���������� ������
    void UpdateProblemType()
    {
        selectedProblemType = problemTypeDropdown.options[problemTypeDropdown.value].text; // �������� ��������� ��� ������
    }

    // ��������� ����� ������ �� ������ ��������� ����������
    void GenerateProblem()
    {
        
        // ��������� ��������� ����� � ����������� �� ������ ���������
        int range = 10;
        switch (selectedDifficulty)
        {
            case "������":
                range = 10; // �������� ����� ��� ������� ������
                break;
            case "�������":
                range = 20; // �������� ����� ��� �������� ������
                break;
            case "�������":
                range = 50; // �������� ����� ��� �������� ������
                break;
        }

        // ��������� ��������� ����� � �������� ���������
        number1 = Random.Range(1, range);
        number2 = Random.Range(1, range);

        // ��������� ������ � ����������� �� ���������� ����
        switch (selectedProblemType)
        {
            case "��������":
                correctAnswer = number1 + number2; // ���������� ����� ��� ��������
                problemText.text = number1.ToString() + " + " + number2.ToString() + " = ?"; // ����� ������ ��� ��������
                break;
            case "���������":
                correctAnswer = number1 - number2; // ���������� ����� ��� ���������
                problemText.text = number1.ToString() + " - " + number2.ToString() + " = ?"; // ����� ������ ��� ���������
                break;
            case "���������":
                correctAnswer = number1 * number2; // ���������� ����� ��� ���������
                problemText.text = number1.ToString() + " * " + number2.ToString() + " = ?"; // ����� ������ ��� ���������
                break;
            case "�������":
                correctAnswer = number1; // ���������� �������� ��� ����������� ������
                number1 = number1 * number2; // �������� ������� � �������� ��� ��������� ��������
                problemText.text = number1.ToString() + " / " + number2.ToString() + " = ?"; // ����� ������ ��� �������
                break;
        }

        // ������� ����� ����� � �������� �����
        answerInput.text = ""; // ������� ���� �����
        
        //feedbackText.text = ""; // ������� ������ �������� �����
        if (_timer != null)
        {
            _timer.fillAmounT = 1f;
        }
    }

    // �������� ������ ������
   public void CheckAnswer()
    {
        int playerAnswer; // ���������� ��� �������� ������ ������
        if (int.TryParse(answerInput.text, out playerAnswer)) // ��������, ��� ��������� ����� �������� ������
        {
            if (playerAnswer == correctAnswer) // ��������� ������ ������ � ���������� �������
            {
                feedbackText.text = "���������!"; // ���� ����� ����������, ������� ���������
                _scores++;
                GenerateProblem(); // ��������� ����� ������
            }
            else if (playerAnswer != correctAnswer)
            {
                if (_scores > 0)
                {
                    _scores--;
                }
                feedbackText.text = "�������� �����."; // ���� ����� ������������, ������� ���������
            }
        }
        else
        {
            feedbackText.text = "����������, ������� �����."; // ���� ��������� �������� �� �������� ������, ������� ���������
        }

        //GenerateProblem(); // ��������� ����� ������
    }
}

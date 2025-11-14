using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TeamData
{
    public string name;
    public string color;
    public int points;
}

public class TeamInformation : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject InputGameobject;
    [SerializeField] GameObject QuantityPanel;
    [SerializeField] GameObject RoundOne;
    [SerializeField] GameObject buttonNext;
    [SerializeField] GameObject info;

    [Header("UI Elements")]
    [SerializeField] InputField inputText;
    [SerializeField] Text colorPromptText;
    [SerializeField] Text teamsProgress;
    [SerializeField] Image backgroundColor;

    [Header("Team Settings")]
    [SerializeField] int countCommand;
    [SerializeField] string[] color = { "красные", "синие", "зелёные", "жёлтые" };
    

    [Header("Runtime State")]
    [SerializeField] TeamData[] command;
    [SerializeField] int currentTeamIndex = 0;
    [SerializeField] bool nameEnabled = false;

    [Header("Info")]
    [SerializeField] Text redInfo;
    [SerializeField] Text blueInfo;
    [SerializeField] Text greenInfo;
    [SerializeField] Text yellowInfo;
    [SerializeField] GameObject[] strokePanel;

    private void Start()
    {
        if (strokePanel != null)
        {
            for (int i = 0; i < strokePanel.Length; i++)
            {
                strokePanel[i].SetActive(false);
            }
        }
        if (QuantityPanel != null && InputGameobject != null)
        {
            QuantityPanel.SetActive(true);
            InputGameobject.SetActive(false);
        }
        if (buttonNext != null)
        {
            buttonNext.SetActive(false);
        }
        if (RoundOne != null)
        {
            RoundOne.SetActive(false);
        }
        if (info != null)
        {
            info.SetActive(false);
        }
        Info();
    }
    private void UpdateBackgroundColor()
    {
        if (backgroundColor == null || command == null || command.Length == 0) return;

        string currentColor = command[currentTeamIndex].color.ToLower().Trim();

        switch (currentColor)
        {
            case "красные":
                backgroundColor.color = new Color32(255, 0, 0, 255); // ярко-красный
                break;
            case "синие":
                backgroundColor.color = new Color32(0, 0, 180, 255); // чуть темнее синий
                break;
            case "зелёные":
                backgroundColor.color = new Color32(0, 120, 0, 255); // тёмно-зелёный
                break;
            case "жёлтые":
                backgroundColor.color = new Color32(180, 180, 0, 255); // тёмно-жёлтый
                break;
            default:
                backgroundColor.color = Color.white;
                break;
        }
    }
    public void Info()
    {
        if (command == null) return;
        redInfo.text = "";
        blueInfo.text = "";
        greenInfo.text = "";
        yellowInfo.text = "";

        foreach (var panel in strokePanel)
        {
            if (panel != null) panel.SetActive(false);
        }

        Dictionary<string, (Text info, GameObject panel)> map = new Dictionary<string, (Text, GameObject)>()
    {
        { color[0], (redInfo, strokePanel[0]) },
        { color[1], (blueInfo, strokePanel[1]) },
        { color[2], (greenInfo, strokePanel[2]) },
        { color[3], (yellowInfo, strokePanel[3]) }
    };

        for (int i = 0; i < command.Length; i++)
        {
            if (map.TryGetValue(command[i].color, out var entry))
            {
                if (entry.info != null)
                    entry.info.text = $"{command[i].color.ToUpper()}: {command[i].points}";
                if (entry.panel != null)
                    entry.panel.SetActive(true);
            }
        }
    }
    public void CountCommand(int count)
    {
        countCommand = count;
        command = new TeamData[countCommand];

        for (int i = 0; i < countCommand; i++)
        {
            command[i] = new TeamData();
            command[i].color = color[i % color.Length];
        }

        currentTeamIndex = 0;
        nameEnabled = false;

        inputText.text = "";
        inputText.ActivateInputField();

        if (colorPromptText != null)
            colorPromptText.text = $"Введите класс команды: {command[currentTeamIndex].color}";
    }

    public void NameTeam()
    {
        if (nameEnabled)
        {
            Debug.Log("Названия уже введены. Ввод заблокирован.");
            return;
        }

        if (countCommand > 0 && inputText != null && !string.IsNullOrEmpty(inputText.text))
        {
            command[currentTeamIndex].name = inputText.text.ToUpper();
            inputText.text = "";
            inputText.ActivateInputField();

            currentTeamIndex++;
            Info();
            if (currentTeamIndex < command.Length)
            {
                if (colorPromptText != null)
                    colorPromptText.text = $"Введите класс команды: {command[currentTeamIndex].color}";
            }
            else
            {
                nameEnabled = true;
                currentTeamIndex = 0;
                if (colorPromptText != null)
                    colorPromptText.text = "Названия всех команд успешно введены.";
                if (teamsProgress != null)
                    teamsProgress.text = $"Отвечает команда {command[currentTeamIndex].color}";
                InputGameobject.SetActive(false);
                buttonNext.SetActive(true);
                Debug.Log("Названия всех команд успешно введены.");
                UpdateBackgroundColor();
            }
        }
        else
        {
            Debug.Log("Заполните название команды");
        }
    }

    public void PointTeamPlus()
    {
        if (command == null || command.Length == 0) return;

        command[currentTeamIndex].points++;
        Info();
        currentTeamIndex = (currentTeamIndex + 1) % command.Length;

        if (teamsProgress != null)
        {
            teamsProgress.text = $"Отвечает команда {command[currentTeamIndex].color}";
            UpdateBackgroundColor();
        }
    }

    public void PointTeamNoPlus()
    {
        if (command == null || command.Length == 0) return;

        currentTeamIndex = (currentTeamIndex + 1) % command.Length;
        if (teamsProgress != null)
        {
            teamsProgress.text = $"Отвечает команда {command[currentTeamIndex].color}";
            UpdateBackgroundColor();
        }
    }
}
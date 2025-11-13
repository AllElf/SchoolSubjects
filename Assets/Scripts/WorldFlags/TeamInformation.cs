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

    [Header("UI Elements")]
    [SerializeField] InputField inputText;
    [SerializeField] Text colorPromptText;
    [SerializeField] Text teamsProgress;



    [Header("Team Settings")]
    [SerializeField] int countCommand;
    [SerializeField] string[] color = { "красный", "синий", "зелёный", "жёлтый" };

    [Header("Runtime State")]
    [SerializeField] TeamData[] command;
    [SerializeField] int currentTeamIndex = 0;
    [SerializeField] bool nameEnabled = false;

    private void Start()
    {
        if (QuantityPanel != null && InputGameobject != null)
        {
            QuantityPanel.SetActive(true);
            InputGameobject.SetActive(false);
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

            if (currentTeamIndex < command.Length)
            {
                if (colorPromptText != null)
                    colorPromptText.text = $"Введите сласс команды: {command[currentTeamIndex].color}";
            }
            else
            {
                nameEnabled = true;
                if (colorPromptText != null)
                    colorPromptText.text = "Названия всех команд успешно введены.";
                currentTeamIndex = 0;
                if(teamsProgress != null) { teamsProgress.text = $"Отвечает команда {command[currentTeamIndex].color}"; }
                InputGameobject.SetActive(false);
                Debug.Log("Названия всех команд успешно введены.");
            }
        }
        else
        {
            Debug.Log("Заполните название команды");
        }
    }
    public void PointTeamPlus()
    {
        if (command == null || command.Length == 0)
        {
            return;
        }
        command[currentTeamIndex].points++;
        currentTeamIndex++;
        if (teamsProgress != null) { teamsProgress.text = $"Отвечает команда {command[currentTeamIndex].color}"; }
        if (currentTeamIndex >= command.Length)
        {
            currentTeamIndex = 0; 
            Debug.Log("Цикл по командам завершён. Начинаем заново.");
        }
    }
    public void PointTeamNoPlus()
    {
        if (command == null || command.Length == 0)
        {
            return;
        }
        currentTeamIndex++;
        if (teamsProgress != null) { teamsProgress.text = $"Отвечает команда {command[currentTeamIndex].color}"; }
        if (currentTeamIndex >= command.Length)
        {
            currentTeamIndex = 0; 
            Debug.Log("Цикл по командам завершён. Начинаем заново.");
        }
    }
}
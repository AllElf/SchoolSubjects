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
    [SerializeField] public TeamData[] command;
    [SerializeField] int currentTeamIndex = 0;
    [SerializeField] bool nameEnabled = false;
    [SerializeField] bool LevelUP = false;

    [Header("Info")]
    [SerializeField] public Text redInfo;
    [SerializeField] public Text blueInfo;
    [SerializeField] public Text greenInfo;
    [SerializeField] public Text yellowInfo;
    [SerializeField] GameObject[] strokePanel;
    [SerializeField] AudioSource correctly;
    [SerializeField] AudioSource wrong;

    private void Start()
    {
        if (strokePanel != null)
        {
            for (int i = 0; i < strokePanel.Length; i++)
                strokePanel[i].SetActive(false);
        }

        if (QuantityPanel != null && InputGameobject != null)
        {
            QuantityPanel.SetActive(true);
            InputGameobject.SetActive(false);
        }
        if (buttonNext != null) buttonNext.SetActive(false);
        if (RoundOne != null) RoundOne.SetActive(false);
        if (info != null) info.SetActive(false);

        // 🔒 Анти-блок кликов: фон и неинтерактивные панели не должны ловить рейкаст
        SafeDisableRaycasts(backgroundColor);             // фон-картинка
        SafeDisableRaycasts(QuantityPanel);               // панель выбора количества
        SafeDisableRaycasts(RoundOne);                    // панель 1 раунда (если это просто инфо)
        SafeDisableRaycasts(info);                        // панель информационная
        SafeDisableRaycastsArray(strokePanel);            // рамки/подсветки команд

        Info();
    }

    private void UpdateBackgroundColor()
    {
        if (backgroundColor == null || command == null || command.Length == 0) return;

        string currentColor = command[currentTeamIndex].color.ToLower().Trim();

        switch (currentColor)
        {
            case "красные": backgroundColor.color = new Color32(255, 0, 0, 255); break;
            case "синие": backgroundColor.color = new Color32(0, 0, 180, 255); break;
            case "зелёные": backgroundColor.color = new Color32(0, 120, 0, 255); break;
            case "жёлтые": backgroundColor.color = new Color32(180, 180, 0, 255); break;
            default: backgroundColor.color = Color.white; break;
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
            if (panel != null) panel.SetActive(false);

        var map = new Dictionary<string, (Text info, GameObject panel)>()
        {
            { color[0], (redInfo,   strokePanel.Length > 0 ? strokePanel[0] : null) },
            { color[1], (blueInfo,  strokePanel.Length > 1 ? strokePanel[1] : null) },
            { color[2], (greenInfo, strokePanel.Length > 2 ? strokePanel[2] : null) },
            { color[3], (yellowInfo,strokePanel.Length > 3 ? strokePanel[3] : null) }
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

    private void FixedUpdate()
    {
        ComandProgress();
    }

    public void ComandProgress()
    {
        if (teamsProgress != null && LevelUP)
            teamsProgress.text = $"Отвечает команда {command[currentTeamIndex].color}";
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

        if (inputText != null)
        {
            inputText.text = "";
            inputText.ActivateInputField();
        }

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

                LevelUP = true;

                // 🔒 делаем панель ввода неинтерактивной, чтобы она точно не ловила клики
                if (InputGameobject != null)
                {
                    SetInteractableRecursively(InputGameobject, false);
                    InputGameobject.SetActive(false);
                }

                if (buttonNext != null) buttonNext.SetActive(true);

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
        if (correctly != null) correctly.Play();
        Info();
        currentTeamIndex = (currentTeamIndex + 1) % command.Length;

        if (teamsProgress != null)
            UpdateBackgroundColor();
    }

    public void PointTeamNoPlus()
    {
        if (command == null || command.Length == 0) return;

        currentTeamIndex = (currentTeamIndex + 1) % command.Length;
        if (wrong != null) wrong.Play();

        if (teamsProgress != null)
            UpdateBackgroundColor();
    }

    // ==================== АНТИ-ПЕРЕКРЫТИЯ UI ====================

    // Отключаем raycastTarget у одиночного Image/Text
    private void SafeDisableRaycasts(Graphic g)
    {
        if (g != null) g.raycastTarget = false;
    }

    // Отключаем raycastTarget у всех Graphics внутри GameObject (и детей)
    private void SafeDisableRaycasts(GameObject root)
    {
        if (root == null) return;
        var graphics = root.GetComponentsInChildren<Graphic>(true);
        foreach (var gr in graphics)
        {
            // не трогаем сами кнопки
            if (gr.GetComponent<Button>() != null) continue;
            gr.raycastTarget = false;
        }
        var cg = root.GetComponent<CanvasGroup>();
        if (cg != null && !root.activeInHierarchy)
        {
            // если панель всё равно скрыта — оставим как есть
        }
    }

    private void SafeDisableRaycastsArray(GameObject[] roots)
    {
        if (roots == null) return;
        foreach (var go in roots) SafeDisableRaycasts(go);
    }

    // Делает/снимает интерактивность у всей панели (InputField/кнопки и т.п.)
    private void SetInteractableRecursively(GameObject root, bool interactable)
    {
        if (root == null) return;

        var cg = root.GetComponent<CanvasGroup>();
        if (cg == null) cg = root.AddComponent<CanvasGroup>();
        cg.interactable = interactable;
        cg.blocksRaycasts = interactable;

        // На всякий случай — для графики, которая не должна ловить клики, выключим таргет
        if (!interactable)
        {
            var graphics = root.GetComponentsInChildren<Graphic>(true);
            foreach (var gr in graphics)
            {
                if (gr.GetComponent<Button>() != null) continue; // кнопки на этой панели всё равно не активны
                gr.raycastTarget = false;
            }
        }
    }
}

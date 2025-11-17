using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Викторина по флагам.
/// </summary>
public class QuisisFlag : MonoBehaviour
{
    // ----- РЕЖИМЫ -----
    public enum LabelMode
    {
        Country,
        Capital,
        CountryWithCapital,
        CapitalWithCountry
    }

    private const int MaxLevel = 4;

    // ----- ДАННЫЕ -----
    [Header("ДАННЫЕ")]
[SerializeField] private CountriesDatabaseRU countryList;

    [Tooltip("Путь в Resources до TeamDataCountryListRU, например: Data/TeamDataCountryListRU")]
    [SerializeField] private string countryListResourcePath = "Data/TeamDataCountryListRU";

    [SerializeField] private bool onlyWithFlags = true;
    [SerializeField] private GameObject panelWin;

    // --- Остальные твои переменные остаются ниже ---


    [Header("Ручное добавление стран")]
    [SerializeField] private bool useManualSubset = false;       // true — работать только с вручную выбранным списком
    [SerializeField] private List<TeamDataCountry> manualSubset; // перетащи сюда нужные записи

    // ----- КОМАНДЫ / УРОВНИ -----
    [Header("КОМАНДЫ / УРОВНИ")]
    [SerializeField] private TeamInformation teamInfo;
    [SerializeField] private Text level;                         // "Уровень N"
    [SerializeField] private Text countAnswer;                   // "N вопрос уровня"

    // ----- UI -----
    [Header("UI")]
    [SerializeField] private GameObject[] button;                // кнопки-ответы (каждая: Button + Text)
    [SerializeField] private Image targetImage;                  // флаг
    [SerializeField] private Text questionTypeText;              // подсказка над вопросом
    [SerializeField] private Text hintText;                      // текстовая подсказка (Верно/Неверно/Время вышло)
    [SerializeField] private Text timerText;                     // (опц.) отображение секунд таймера

    [Header("Debug")]
    [SerializeField] private Text debugText;

    [Header("Timer Arrow")]
    [SerializeField] private RectTransform arrow;    // стрелка таймера (например, UI-изображение)
    [SerializeField] private bool invertRotation = false; // если хочешь вращать в другую сторону
    [SerializeField] private AudioSource clock;

    // ----- ПОВЕДЕНИЕ -----
    [Header("ПОВЕДЕНИЕ")]
    [SerializeField] private LabelMode labelMode = LabelMode.Country; // синхронизируется с уровнем
    [SerializeField] private bool randomFlags = false;           // брать записи случайно или по порядку
    [SerializeField] private bool autoNextAfterAnswer = false;   // сразу переходить после подсказки (не используется, но оставлен для расширения)

    // ----- ПРОГРЕСС -----
    [Header("ПРОГРЕСС")]
    [SerializeField] private int countIndex = 0;                 // индекс по пулу (для последовательного режима)
    [SerializeField] private int countIndexLevel = 0;            // оставшиеся вопросы на текущем уровне (устанавливается программно)
    [SerializeField] private int currentLevelNumber = 1;         // текущий уровень (1..4)

    // ----- ЧИСЛО ВОПРОСОВ НА УРОВЕНЬ -----
    [Header("ЧИСЛО ВОПРОСОВ НА УРОВЕНЬ")]
    [SerializeField] private int[] questionsPerLevel = new int[] { 20, 20, 20, 20 }; // индекс 0 => уровень 1 и т.д.
    [SerializeField] private int defaultPerLevel = 20;           // запасное значение, если элемент массива = 0/отриц.

    // ----- ОБРАТНАЯ СВЯЗЬ -----
    [Header("ОБРАТНАЯ СВЯЗЬ)")]
    [SerializeField] private bool showHints = true;              // показывать текстовую подсказку
    [SerializeField] private float hintSeconds = 1.5f;           // сколько секунд держать подсказку
    [SerializeField] private bool colorizeButtons = true;        // подсветка кнопок
    [SerializeField] private Color correctColor = new Color(0.3f, 0.85f, 0.3f); // зелёный
    [SerializeField] private Color wrongColor = new Color(0.95f, 0.35f, 0.35f); // красный


    // ----- ТАЙМЕР ВОПРОСА -----
    [Header("ТАЙМЕР ВОПРОСА")]
    [SerializeField] private bool useQuestionTimer = false;      // включить таймер вопроса
    [SerializeField] private float questionSeconds = 10f;        // длительность таймера
    [SerializeField] private bool showTimerUI = true;            // показывать текст таймера

    // ----- ФИНИШ -----
    [Header("Finish")]
    [SerializeField] private string finishMessage = "Игра закончилась!";
    public UnityEvent OnGameFinished;                            // событие конца игры (можно повесить окно/панель)

    // ----- RUNTIME -----
    [SerializeField] private string currentLabel = "";           // отладка: текущая "подпись"
    private List<TeamDataCountry> _pool = new List<TeamDataCountry>();
    private TeamDataCountry _current;
    private System.Random _rng = new System.Random();
    private bool _inputLocked = false;
    private Coroutine _timerRoutine;

    // ======================== UNITY ЖИЗНЕННЫЙ ЦИКЛ ========================
    private void Awake()
    {
        Debug_CheckEmptyCountries();
        Debug_CheckMissingFlags();
        // 1) ПРИОРИТЕТ: если countryList назначен в инспекторе — просто используем его
        if (countryList != null)
        {
            int total = countryList.Countries != null ? countryList.Countries.Count : 0;
            SetDebugMessage(
                $"OK: countryList взят из инспектора.\n" +
                $"Стран в списке: {total}"
            );
            return;
        }

        // 2) Если в инспекторе не назначен — пробуем загрузить из Resources по строке
        if (!string.IsNullOrEmpty(countryListResourcePath))
        {
            countryList = Resources.Load<CountriesDatabaseRU>(countryListResourcePath);
        }

        // 3) Диагностика: есть ли вообще такие SO в Resources
        var allCountryLists = Resources.LoadAll<CountriesDatabaseRU>("");
        SetDebugMessage($"DIAG: Найдено ScriptableObject TeamDataCountryListRU в Resources: {allCountryLists.Length} шт.");
        for (int i = 0; i < allCountryLists.Length; i++)
        {
            var so = allCountryLists[i];
            SetDebugMessage($"DIAG: SO[{i}] name = '{so.name}'");
        }

        // 4) Финальный статус
        if (countryList == null)
        {
            SetDebugMessage(
                "❌ ERROR: ScriptableObject TeamDataCountryListRU НЕ найден!\n" +
                "countryList = null\n" +
                $"Ожидался по пути: Resources/{countryListResourcePath}"
            );
        }
        else
        {
            int total = countryList.Countries != null ? countryList.Countries.Count : 0;
            SetDebugMessage(
                "✔ OK: ScriptableObject TeamDataCountryListRU найден через Resources.\n" +
                "countryList НЕ пустой.\n" +
                $"Стран в списке: {total}"
            );
        }
    }






    /// <summary>
    /// Пишет сообщение в debugText (если есть) и в консоль.
    /// </summary>
    private void SetDebugMessage(string msg)
    {
        string time = System.DateTime.Now.ToString("HH:mm:ss");
        string entry = $"[{time}] {msg}";

        if (debugText != null)
            debugText.text += "\n" + entry;

        Debug.Log("[QuisisFlag] " + entry);
    }
    /// <summary>
    /// Проверка пустых стран и столиц.
    /// </summary>
    private void Debug_CheckEmptyCountries()
    {
        if (countryList == null || countryList.Countries == null)
        {
            SetDebugMessage("❌ Debug_CheckEmptyCountries: countryList = NULL");
            return;
        }

        int emptyName = 0;
        int emptyCapital = 0;

        foreach (var c in countryList.Countries)
        {
            if (c == null)
                continue;

            if (string.IsNullOrWhiteSpace(c.nameCountry))
            {
                emptyName++;
                SetDebugMessage($"⚠ Пустое имя страны! (capital='{c.capitalCountry}')");
            }

            if (string.IsNullOrWhiteSpace(c.capitalCountry))
            {
                emptyCapital++;
                SetDebugMessage($"⚠ Пустая столица! (country='{c.nameCountry}')");
            }
        }

        SetDebugMessage($"ИТОГ: Пустых имён стран: {emptyName}, пустых столиц: {emptyCapital}");
    }
    /// <summary>
    /// Проверка стран без флага.
    /// </summary>
    private void Debug_CheckMissingFlags()
    {
        if (countryList == null || countryList.Countries == null)
        {
            SetDebugMessage("❌ Debug_CheckMissingFlags: countryList = NULL");
            return;
        }

        int missingFlags = 0;

        foreach (var c in countryList.Countries)
        {
            if (c == null)
                continue;

            if (c.flag == null)
            {
                missingFlags++;
                SetDebugMessage($"⚠ Нет флага: {c.nameCountry}");
            }
        }

        SetDebugMessage($"ИТОГ: Стран без флага: {missingFlags}");
    }

    private void Start()
    {
        if (panelWin != null)
            panelWin.SetActive(false);

        // Если countryList так и остался null – даже не пытаемся запускаться
        if (countryList == null)
        {
            SetDebugMessage("CRITICAL: countryList = null в Start(). Инициализация викторины остановлена.");
            enabled = false;
            return;
        }

        // сброс индекса
        countIndex = 0;

        // собираем пул
        BuildPool();

        int poolCount = (_pool != null) ? _pool.Count : 0;
        int buttonsCount = (button != null) ? button.Length : 0;

        // Чёткий лог по состоянию на старт
        SetDebugMessage(
            "Инициализация викторины завершена.\n" +
            $"Элементов в пуле стран: {poolCount}\n" +
            $"Кнопок-ответов: {buttonsCount}\n" +
            $"Текущий уровень: {currentLevelNumber}"
        );

        if (poolCount == 0)
        {
            SetDebugMessage("ERROR: Пул стран пуст после BuildPool(). Игра не может начаться.");
            enabled = false;
            return;
        }

        if (buttonsCount == 0)
        {
            SetDebugMessage("ERROR: Не назначены кнопки (button[] пуст). Игра не может начаться.");
            enabled = false;
            return;
        }

        // ограничиваем уровень
        currentLevelNumber = Mathf.Clamp(currentLevelNumber, 1, MaxLevel);

        // режим по уровню
        ApplyLevelToMode(currentLevelNumber);

        // количество вопросов
        countIndexLevel = GetQuestionsForLevel(currentLevelNumber);

        // UI
        UpdateQuestionTypeText();
        UpdateLevelText();

        if (hintText) hintText.gameObject.SetActive(false);
        if (timerText) timerText.gameObject.SetActive(false);

        // первый вопрос
        Next();
    }





    private void Update()
    {
        if (countAnswer != null)
            countAnswer.text = $"{countIndexLevel.ToString()} вопрос уровня";

        if (_current != null)
            currentLabel = GetLabelForDebug(_current);
    }

    // ======================== ПОСТРОЕНИЕ ПУЛА ========================

    /// <summary>
    /// Формирует пул стран для викторины.
    /// Работает как с полным списком, так и с ручным поднабором manualSubset.
    /// </summary>
    private void BuildPool()
    {
        // базовая проверка ассета
        if (countryList == null || countryList.Countries == null || countryList.Countries.Count == 0)
        {
            _pool = new List<TeamDataCountry>();
            Debug.LogWarning("[QuisisFlag] countryList пуст или не назначен.");
            return;
        }

        // --- РУЧНОЙ ПОДНАБОР ---
        if (useManualSubset && manualSubset != null && manualSubset.Count > 0)
        {
            // копируем список без сортировки, чтобы сохранить ПОРЯДОК как в инспекторе
            _pool = new List<TeamDataCountry>(manualSubset.Where(c => c != null));

            // не удаляем дубликаты и не применяем Distinct, чтобы не нарушать порядок
            // только фильтрация по флагам (если включена)
            if (onlyWithFlags)
                _pool = _pool.Where(c => c.flag != null).ToList();

            // если после фильтрации ничего не осталось
            if (_pool.Count == 0)
                Debug.LogWarning("[QuisisFlag] Manual Subset пуст после фильтрации (проверь наличие флагов).");

            // всегда начинаем с нуля при ручном поднаборе
            countIndex = 0;
            return;
        }

        // --- ПОЛНЫЙ ПУЛ ---
        _pool = onlyWithFlags
            ? countryList.Countries.Where(c => c.flag != null).ToList()
            : countryList.Countries.ToList();

        if (_pool.Count == 0)
            _pool = countryList.Countries.ToList();

        // фильтр по заполненным данным (страна/столица)
        _pool = _pool.Where(HasNeededDataForAnyMode).ToList();

        if (_pool.Count == 0)
            Debug.LogWarning("[QuisisFlag] Пул записей пуст после фильтрации.");

        // сбрасываем индекс для корректного старта
        countIndex = 0;
    }


    private void OnValidate()
    {
        if (manualSubset != null)
        {
            manualSubset = manualSubset.Where(c => c != null).ToList();
            var seen = new HashSet<TeamDataCountry>();
            var cleaned = new List<TeamDataCountry>();
            foreach (var c in manualSubset)
                if (seen.Add(c)) cleaned.Add(c);
            manualSubset = cleaned;
        }

        // нормализуем настройки количества вопросов
        if (questionsPerLevel != null)
        {
            for (int i = 0; i < questionsPerLevel.Length; i++)
                if (questionsPerLevel[i] < 0) questionsPerLevel[i] = 0; // 0 => возьмём defaultPerLevel
        }
        if (defaultPerLevel < 1) defaultPerLevel = 1;
    }

    // ======================== ОСНОВНОЙ ЦИКЛ ВОПРОСОВ ========================

    public void Next()
    {
        SetDebugMessage($"Next(): level={currentLevelNumber}, remaining={countIndexLevel}");

        if (_pool == null || _pool.Count == 0 || button == null || button.Length == 0)
            return;

        StopTimerIfAny();
        _inputLocked = false;
        if (hintText) hintText.gameObject.SetActive(false);

        // выбрать текущий элемент
        _current = randomFlags ? _pool[_rng.Next(_pool.Count)] : _pool[countIndex % _pool.Count];

        // отрисовать вопрос
        RenderCurrentQuestion();

        // шаг по индексу (для последовательного режима)
        if (!randomFlags)
            countIndex = (countIndex + 1) % _pool.Count;

        // запустить таймер (если включён)
        StartTimerIfEnabled();
    }

    private void DecrementAndMaybeAdvanceLevel()
    {
        countIndexLevel--;

        if (countIndexLevel <= 0)
        {
            if (currentLevelNumber < MaxLevel)
            {
                currentLevelNumber++;
                ApplyLevelToMode(currentLevelNumber);
                UpdateLevelText();
                UpdateQuestionTypeText();

                // сбросить счётчик на значение для нового уровня
                countIndexLevel = GetQuestionsForLevel(currentLevelNumber);

                // мгновенно обновить вопрос под новый режим/уровень
                _current = randomFlags ? _pool[_rng.Next(_pool.Count)] : _pool[countIndex % _pool.Count];
                RenderCurrentQuestion();
                StartTimerIfEnabled();
            }
            else
            {
                FinishGame();
            }
        }
        else
        {
            Next();
        }
    }

    // ======================== ОТРИСОВКА ВОПРОСА ========================

    private void RenderCurrentQuestion()
    {
        if (_current == null) return;

        if (targetImage != null)
        {
            targetImage.sprite = _current.flag;
            targetImage.enabled = (_current.flag != null);
        }

        switch (labelMode)
        {
            case LabelMode.Country:
            case LabelMode.Capital:
                RenderSimpleCountryOrCapital();
                break;
            case LabelMode.CountryWithCapital:
                RenderCountryWithCapitalMixed();
                break;
            case LabelMode.CapitalWithCountry:
                RenderCapitalWithCountryMixed();
                break;
        }
    }

    private void RenderSimpleCountryOrCapital()
    {
        string correctText = GetLabelByMode(_current, labelMode);
        if (string.IsNullOrEmpty(correctText))
        {
            Debug.LogWarning("[QuisisFlag] Не удалось получить корректный текст ответа.");
            return;
        }

        int correctIndex = Random.Range(0, button.Length);
        var used = new HashSet<string>(System.StringComparer.OrdinalIgnoreCase) { correctText };

        Button correctBtn = null;
        var allButtons = new List<Button>(button.Length);

        for (int i = 0; i < button.Length; i++)
        {
            var btnGO = button[i];
            if (!btnGO) continue;

            var btn = btnGO.GetComponent<Button>();
            var txt = btnGO.GetComponentInChildren<Text>(true);
            if (btn == null) continue;

            allButtons.Add(btn);
            btn.onClick.RemoveAllListeners();

            if (i == correctIndex)
            {
                correctBtn = btn;
                if (txt) txt.text = correctText;

                btn.onClick.AddListener(() =>
                {
                    if (_inputLocked) return;
                    StopTimerIfAny();
                    OnAnswerSelected(
                        isCorrect: true,
                        correctDisplay: BuildCorrectDisplayForMode(_current, labelMode),
                        clickedButton: btn,
                        correctButton: correctBtn,
                        allButtons: allButtons
                    );
                });
            }
            else
            {
                // дистрактор
                string randomLabel;
                int safety = 0;
                do
                {
                    var rndItem = _pool[_rng.Next(_pool.Count)];
                    randomLabel = GetLabelByMode(rndItem, labelMode);
                    safety++;
                    if (safety > 2000) break;
                } while (string.IsNullOrEmpty(randomLabel) || used.Contains(randomLabel));

                used.Add(randomLabel);
                if (txt) txt.text = randomLabel;

                btn.onClick.AddListener(() =>
                {
                    if (_inputLocked) return;
                    StopTimerIfAny();
                    OnAnswerSelected(
                        isCorrect: false,
                        correctDisplay: BuildCorrectDisplayForMode(_current, labelMode),
                        clickedButton: btn,
                        correctButton: correctBtn,
                        allButtons: allButtons
                    );
                });
            }
        }
    }

    private void RenderCountryWithCapitalMixed()
    {
        string country = _current?.nameCountry;
        string correctCapital = _current?.capitalCountry;
        if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(correctCapital))
        {
            Debug.LogWarning("[QuisisFlag] Для режима CountryWithCapital требуется и страна, и столица.");
            return;
        }

        int correctIndex = Random.Range(0, button.Length);

        var candidateCapitals = _pool
            .Where(c => !string.IsNullOrEmpty(c.capitalCountry) &&
                        !string.Equals(c.capitalCountry, correctCapital, System.StringComparison.OrdinalIgnoreCase))
            .Select(c => c.capitalCountry)
            .Distinct(System.StringComparer.OrdinalIgnoreCase)
            .ToList();

        Shuffle(candidateCapitals);
        var distractorCapitals = TakeDistinct(candidateCapitals, button.Length - 1);
        while (distractorCapitals.Count < button.Length - 1 && candidateCapitals.Count > 0)
            distractorCapitals.Add(candidateCapitals[_rng.Next(candidateCapitals.Count)]);

        int di = 0;
        Button correctBtn = null;
        var allButtons = new List<Button>(button.Length);

        for (int i = 0; i < button.Length; i++)
        {
            var btnGO = button[i]; if (!btnGO) continue;
            var btn = btnGO.GetComponent<Button>(); if (btn == null) continue;
            var txt = btnGO.GetComponentInChildren<Text>(true);

            allButtons.Add(btn);
            btn.onClick.RemoveAllListeners();

            if (i == correctIndex)
            {
                correctBtn = btn;
                if (txt) txt.text = ComposeCountryCapital(country, correctCapital);
                btn.onClick.AddListener(() =>
                {
                    if (_inputLocked) return;
                    StopTimerIfAny();
                    OnAnswerSelected(true, ComposeCountryCapital(country, correctCapital), btn, correctBtn, allButtons);
                });
            }
            else
            {
                string wrongCapital = (di < distractorCapitals.Count) ? distractorCapitals[di++] : correctCapital;
                if (txt) txt.text = ComposeCountryCapital(country, wrongCapital);
                btn.onClick.AddListener(() =>
                {
                    if (_inputLocked) return;
                    StopTimerIfAny();
                    OnAnswerSelected(false, ComposeCountryCapital(country, correctCapital), btn, correctBtn, allButtons);
                });
            }
        }
    }

    private void RenderCapitalWithCountryMixed()
    {
        string correctCountry = _current?.nameCountry;
        string capital = _current?.capitalCountry;
        if (string.IsNullOrEmpty(correctCountry) || string.IsNullOrEmpty(capital))
        {
            Debug.LogWarning("[QuisisFlag] Для режима CapitalWithCountry требуется и страна, и столица.");
            return;
        }

        int correctIndex = Random.Range(0, button.Length);

        var candidateCountries = _pool
            .Where(c => !string.IsNullOrEmpty(c.nameCountry) &&
                        !string.Equals(c.nameCountry, correctCountry, System.StringComparison.OrdinalIgnoreCase))
            .Select(c => c.nameCountry)
            .Distinct(System.StringComparer.OrdinalIgnoreCase)
            .ToList();

        Shuffle(candidateCountries);
        var distractorCountries = TakeDistinct(candidateCountries, button.Length - 1);
        while (distractorCountries.Count < button.Length - 1 && candidateCountries.Count > 0)
            distractorCountries.Add(candidateCountries[_rng.Next(candidateCountries.Count)]);

        int di = 0;
        Button correctBtn = null;
        var allButtons = new List<Button>(button.Length);

        for (int i = 0; i < button.Length; i++)
        {
            var btnGO = button[i]; if (!btnGO) continue;
            var btn = btnGO.GetComponent<Button>(); if (btn == null) continue;
            var txt = btnGO.GetComponentInChildren<Text>(true);

            allButtons.Add(btn);
            btn.onClick.RemoveAllListeners();

            if (i == correctIndex)
            {
                correctBtn = btn;
                if (txt) txt.text = ComposeCountryCapital(correctCountry, capital);
                btn.onClick.AddListener(() =>
                {
                    if (_inputLocked) return;
                    StopTimerIfAny();
                    OnAnswerSelected(true, ComposeCountryCapital(correctCountry, capital), btn, correctBtn, allButtons);
                });
            }
            else
            {
                string wrongCountry = (di < distractorCountries.Count) ? distractorCountries[di++] : correctCountry;
                if (txt) txt.text = ComposeCountryCapital(wrongCountry, capital);
                btn.onClick.AddListener(() =>
                {
                    if (_inputLocked) return;
                    StopTimerIfAny();
                    OnAnswerSelected(false, ComposeCountryCapital(correctCountry, capital), btn, correctBtn, allButtons);
                });
            }
        }
    }

    // ======================== ОТВЕТ/ПОДСКАЗКИ/ПЕРЕХОД ========================

    private void OnAnswerSelected(bool isCorrect, string correctDisplay, Button clickedButton, Button correctButton, List<Button> allButtons)
    {
        _inputLocked = true;

        // очки команде
        if (teamInfo != null)
        {
            if (isCorrect) teamInfo.PointTeamPlus();
            else teamInfo.PointTeamNoPlus();
        }

        // подсветка кнопок
        var originals = new Dictionary<Button, Color>();
        if (colorizeButtons)
        {
            foreach (var b in allButtons)
            {
                var colors = b.colors;
                originals[b] = colors.normalColor;
            }

            if (!isCorrect && clickedButton)
            {
                var c = clickedButton.colors;
                c.normalColor = wrongColor;
                c.selectedColor = wrongColor;
                c.highlightedColor = wrongColor;
                clickedButton.colors = c;
            }

            if (correctButton)
            {
                var c = correctButton.colors;
                c.normalColor = correctColor;
                c.selectedColor = correctColor;
                c.highlightedColor = correctColor;
                correctButton.colors = c;
            }
        }

        // текстовая подсказка
        if (showHints && hintText)
        {
            hintText.text = isCorrect ? "Верно!" : $"Неверно. Правильно: {correctDisplay}";
            hintText.gameObject.SetActive(true);
        }

        // подождать hintSeconds и перейти дальше/вернуть цвета
        StartCoroutine(AfterAnswerRoutine(originals));
    }

    private IEnumerator AfterAnswerRoutine(Dictionary<Button, Color> originals)
    {
        float wait = Mathf.Max(0f, showHints ? hintSeconds : 0f);
        if (wait > 0f) yield return new WaitForSeconds(wait);

        if (hintText) hintText.gameObject.SetActive(false);

        if (colorizeButtons)
        {
            foreach (var kv in originals)
            {
                var b = kv.Key; if (!b) continue;
                var c = b.colors;
                c.normalColor = kv.Value;
                c.selectedColor = kv.Value;
                c.highlightedColor = kv.Value;
                b.colors = c;
            }
        }

        _inputLocked = false;

        // уменьшить счётчик вопросов и, если надо, повысить уровень/завершить игру
        DecrementAndMaybeAdvanceLevel();
    }

    // ======================== ТАЙМЕР ВОПРОСА ========================

    public void UseQuestionTimer()
    {
        useQuestionTimer = !useQuestionTimer;
        if (useQuestionTimer)
        {
            if (clock != null) { clock.Play(); }
            StartTimerIfEnabled();
        }
        else
        {
            if (clock != null) { clock.Stop(); }
            StopTimerIfAny();
        }
    }

    public void StartTimerIfEnabled()
    {
        if (!useQuestionTimer) return;

        StopTimerIfAny();

        if (showTimerUI && timerText)
            timerText.gameObject.SetActive(true);

        _timerRoutine = StartCoroutine(QuestionTimerRoutine(questionSeconds));
    }

    public void StopTimerIfAny()
    {
        if (_timerRoutine != null)
        {
            StopCoroutine(_timerRoutine);
            _timerRoutine = null;
        }
        if (timerText) timerText.gameObject.SetActive(false);
        ResetArrowRotation();
    }

    /// <summary>
    /// Вращает стрелку во время активного отсчёта таймера.
    /// Делает один полный оборот (360°) за секунду.
    /// </summary>
    private void RotateArrowDuringTimer(float elapsedTime, float totalTime)
    {
        if (arrow == null) return;

        if (totalTime <= 0f)
        {
            ResetArrowRotation();
            return;
        }

        // вычисляем прогресс текущей секунды (0..1)
        float cycleProgress = (elapsedTime % 1f); // каждый оборот = 1 секунда
        float angle = (invertRotation ? 360f : -360f) * cycleProgress;

        // применяем поворот по оси Z
        arrow.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    /// <summary>
    /// Сбрасывает стрелку в исходное положение (0° по Z)
    /// </summary>
    private void ResetArrowRotation()
    {
        if (arrow != null)
            arrow.localRotation = Quaternion.identity;
    }

    private IEnumerator QuestionTimerRoutine(float seconds)
    {
        if (clock != null) { clock.Play(); }
        float t = Mathf.Max(0.01f, seconds);
        while (t > 0f && !_inputLocked)
        {
            if (showTimerUI && timerText)
                timerText.text = Mathf.CeilToInt(t).ToString();

            // --- ВРАЩЕНИЕ СТРЕЛКИ ---
            float elapsed = seconds - t;
            RotateArrowDuringTimer(elapsed, seconds);
            // -------------------------

            yield return null;
            t -= Time.deltaTime;
        }
        ResetArrowRotation();
        if (_inputLocked) yield break; // уже ответили

        // время вышло — считаем неверным
        _inputLocked = true;
        if (clock != null) { clock.Stop(); }
        Button correctButton = FindCorrectButtonForCurrentMode(out string correctDisplay);
        var allButtons = CollectAllButtons();
        var originals = new Dictionary<Button, Color>();

        if (colorizeButtons)
        {
            foreach (var b in allButtons)
            {
                var colors = b.colors;
                originals[b] = colors.normalColor;
            }

            if (correctButton)
            {
                var c = correctButton.colors;
                c.normalColor = correctColor;
                c.selectedColor = correctColor;
                c.highlightedColor = correctColor;
                correctButton.colors = c;
            }
        }

        if (showHints && hintText)
        {
            hintText.text = $"Время вышло. Правильно: {correctDisplay}";
            hintText.gameObject.SetActive(true);
        }

        if (teamInfo != null) teamInfo.PointTeamNoPlus(); // штраф как за неверный

        float wait = Mathf.Max(0f, showHints ? hintSeconds : 0f);
        if (wait > 0f) yield return new WaitForSeconds(wait);

        if (hintText) hintText.gameObject.SetActive(false);

        if (colorizeButtons)
        {
            foreach (var kv in originals)
            {
                var b = kv.Key; if (!b) continue;
                var c = b.colors;
                c.normalColor = kv.Value;
                c.selectedColor = kv.Value;
                c.highlightedColor = kv.Value;
                b.colors = c;
            }
        }

        _inputLocked = false;
        DecrementAndMaybeAdvanceLevel();
    }

    // ======================== ПОИСК КНОПКИ/СПИСКИ КНОПОК ========================

    private Button FindCorrectButtonForCurrentMode(out string correctDisplay)
    {
        correctDisplay = BuildCorrectDisplayForMode(_current, labelMode);

        foreach (var go in button)
        {
            if (!go) continue;
            var txt = go.GetComponentInChildren<Text>(true);
            var btn = go.GetComponent<Button>();
            if (!btn || !txt) continue;

            if (labelMode == LabelMode.Country || labelMode == LabelMode.Capital)
            {
                if (txt.text == GetLabelByMode(_current, labelMode))
                    return btn;
            }
            else
            {
                if (txt.text == correctDisplay)
                    return btn;
            }
        }
        return null;
    }

    private List<Button> CollectAllButtons()
    {
        var list = new List<Button>();
        foreach (var go in button)
        {
            if (!go) continue;
            var b = go.GetComponent<Button>();
            if (b) list.Add(b);
        }
        return list;
    }

    // ======================== ХЕЛПЕРЫ ФОРМАТА/ТЕКСТА/ФИЛЬТРОВ ========================

    private string BuildCorrectDisplayForMode(TeamDataCountry item, LabelMode mode)
    {
        if (item == null) return "";
        switch (mode)
        {
            case LabelMode.Country: return item.nameCountry;
            case LabelMode.Capital: return item.capitalCountry;
            case LabelMode.CountryWithCapital:
            case LabelMode.CapitalWithCountry:
                return ComposeCountryCapital(item.nameCountry, item.capitalCountry);
            default: return "";
        }
    }

    private void UpdateQuestionTypeText()
    {
        if (!questionTypeText) return;

        switch (labelMode)
        {
            case LabelMode.Country:
                questionTypeText.text = "Выберите страну";
                break;
            case LabelMode.Capital:
                questionTypeText.text = "Выберите столицу";
                break;
            case LabelMode.CountryWithCapital:
                questionTypeText.text = "Страна — столица (верна лишь одна столица)";
                break;
            case LabelMode.CapitalWithCountry:
                questionTypeText.text = "Столица — страна (верна лишь одна страна)";
                break;
        }
    }

    private void UpdateLevelText()
    {
        if (!level) return;
        level.text = $"Уровень {currentLevelNumber}";
    }

    private string GetLabelForDebug(TeamDataCountry item)
    {
        if (item == null) return "";
        switch (labelMode)
        {
            case LabelMode.Country: return item.nameCountry;
            case LabelMode.Capital: return item.capitalCountry;
            case LabelMode.CountryWithCapital:
            case LabelMode.CapitalWithCountry:
                return ComposeCountryCapital(item.nameCountry, item.capitalCountry);
            default: return "";
        }
    }

    private string GetLabelByMode(TeamDataCountry item, LabelMode mode)
    {
        if (item == null) return "";
        if (mode == LabelMode.Country) return item.nameCountry;
        if (mode == LabelMode.Capital) return item.capitalCountry;
        return "";
    }

    private static string ComposeCountryCapital(string country, string capital)
    {
        if (string.IsNullOrEmpty(country)) country = "?";
        if (string.IsNullOrEmpty(capital)) capital = "?";
        return $"{country} — {capital}";
    }

    private static void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    private static List<T> TakeDistinct<T>(IList<T> src, int count)
    {
        var result = new List<T>(count);
        var used = new HashSet<T>();
        for (int i = 0; i < src.Count && result.Count < count; i++)
        {
            if (used.Add(src[i])) result.Add(src[i]);
        }
        return result;
    }

    private bool HasNeededDataForAnyMode(TeamDataCountry c)
    {
        if (c == null) return false;
        bool okCountry = !string.IsNullOrEmpty(c.nameCountry);
        bool okCapital = !string.IsNullOrEmpty(c.capitalCountry);
        bool okMixed = okCountry && okCapital;
        return okCountry || okCapital || okMixed;
    }

    // ======================== УРОВНИ/РЕЖИМЫ/ФИНИШ ========================

    private void ApplyLevelToMode(int lvl)
    {
        switch (Mathf.Clamp(lvl, 1, MaxLevel))
        {
            case 1: labelMode = LabelMode.Country; break;            // 1 = Country
            case 2: labelMode = LabelMode.Capital; break;            // 2 = Capital
            case 3: labelMode = LabelMode.CountryWithCapital; break; // 3 = страна фикс, столица верна одна
            case 4: labelMode = LabelMode.CapitalWithCountry; break; // 4 = столица фикс, страна верна одна
        }
    }

    private int GetQuestionsForLevel(int lvl)
    {
        int i = Mathf.Clamp(lvl, 1, MaxLevel) - 1;
        if (questionsPerLevel != null && i < questionsPerLevel.Length && questionsPerLevel[i] > 0)
            return questionsPerLevel[i];
        return Mathf.Max(1, defaultPerLevel);
    }

    private void FinishGame()
    {
        StopTimerIfAny();
        _inputLocked = true;

        if (clock != null)
            clock.Stop();

        if (hintText && showHints)
        {
            hintText.text = finishMessage;
            hintText.gameObject.SetActive(true);
        }

        if (panelWin != null)
            panelWin.SetActive(true);

        if (OnGameFinished != null)
            OnGameFinished.Invoke();
    }

    // ======================== ПУБЛИЧНЫЕ УТИЛИТЫ ========================

    public void JumpToLevel(int lvl)
    {
        lvl = Mathf.Clamp(lvl, 1, MaxLevel);
        currentLevelNumber = lvl;
        ApplyLevelToMode(currentLevelNumber);
        UpdateLevelText();
        UpdateQuestionTypeText();
        countIndexLevel = GetQuestionsForLevel(currentLevelNumber);
        Next();
    }

    public void AdvanceLevel()
    {
        if (currentLevelNumber < MaxLevel)
        {
            currentLevelNumber++;
            ApplyLevelToMode(currentLevelNumber);
            UpdateLevelText();
            UpdateQuestionTypeText();
            countIndexLevel = GetQuestionsForLevel(currentLevelNumber);
            Next();
        }
        else
        {
            FinishGame();
        }
    }

    public void SetLabelMode(LabelMode mode)
    {
        labelMode = mode;
        // выровнять уровень под режим (1=Country, 2=Capital, 3=CountryWithCapital, 4=CapitalWithCountry)
        switch (mode)
        {
            case LabelMode.Country: currentLevelNumber = 1; break;
            case LabelMode.Capital: currentLevelNumber = 2; break;
            case LabelMode.CountryWithCapital: currentLevelNumber = 3; break;
            case LabelMode.CapitalWithCountry: currentLevelNumber = 4; break;
        }
        UpdateQuestionTypeText();
        UpdateLevelText();
        countIndexLevel = GetQuestionsForLevel(currentLevelNumber);
        Next();
    }

    public void ToggleLabelMode()
    {
        AdvanceLevel();
    }

    public void EnableManualSubset(bool enable, bool rebuildNow = true)
    {
        useManualSubset = enable;
        if (rebuildNow)
        {
            BuildPool();
            countIndex = 0;
            Next();
        }
    }

    public void SetManualSubset(List<TeamDataCountry> subset, bool enable = true, bool rebuildNow = true)
    {
        manualSubset = subset ?? new List<TeamDataCountry>();
        useManualSubset = enable;
        if (rebuildNow)
        {
            BuildPool();
            countIndex = 0;
            Next();
        }
    }

    /// <summary>
    /// Пропускает текущий вопрос без начисления очков и без уменьшения оставшихся ходов.
    /// Просто показывает следующий вопрос.
    /// </summary>
    public void SkipQuestion()
    {
        if (_pool == null || _pool.Count == 0 || button == null || button.Length == 0)
            return;

        StopTimerIfAny();
        _inputLocked = false;
        if (hintText) hintText.gameObject.SetActive(false);

        // просто выбираем новый элемент без изменения уровня/счётчиков
        _current = randomFlags ? _pool[_rng.Next(_pool.Count)] : _pool[countIndex % _pool.Count];
        RenderCurrentQuestion();

        // увеличиваем индекс только если не случайный порядок
        if (!randomFlags)
            countIndex = (countIndex + 1) % _pool.Count;

        // перезапускаем таймер если он включён
        StartTimerIfEnabled();
    }
}

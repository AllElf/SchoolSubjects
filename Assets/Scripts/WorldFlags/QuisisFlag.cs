using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#if TMP_PRESENT || TEXTMESHPRO
using TMPro;
#endif

public class QuisisFlag : MonoBehaviour
{
    // ======================== РЕЖИМЫ ========================
    public enum LabelMode
    {
        Country,                 // спрашиваем страну по флагу (Уровень 1)
        Capital,                 // спрашиваем столицу по флагу (Уровень 2)
        CountryWithCapital,      // страна фикс, столица верна лишь на одной (Уровень 3)
        CapitalWithCountry       // столица фикс, страна верна лишь на одной (Уровень 4)
    }
    private const int MaxLevel = 4;

    // ======================== ДАННЫЕ ========================
    [Header("ДАННЫЕ")]
    [SerializeField] private TeamDataCountryListRU countryList;        // ассет со странами/столицами/флагами
    [SerializeField] private bool onlyWithFlags = true;                 // брать только те, у кого есть флаг (ручной или из Resources)
    [SerializeField] private GameObject panelWin;

    [Header("Ручной поднабор")]
    [SerializeField] private bool useManualSubset = false;
    [SerializeField] private List<TeamDataCountry> manualSubset;

    // ======================== КОМАНДЫ / УРОВНИ ========================
    [Header("КОМАНДЫ / УРОВНИ")]
    [SerializeField] private TeamInformation teamInfo;
    [SerializeField] private Text level;        // "Уровень N"
    [SerializeField] private Text countAnswer;  // "N вопрос уровня"

    // ======================== UI ========================
    [Header("UI")]
    [SerializeField] private GameObject[] button;      // массив кнопок (Button + Text/TMP_Text)
    [SerializeField] private Image targetImage;        // флаг
    [SerializeField] private Text questionTypeText;    // надпись над вопросом
    [SerializeField] private Text hintText;            // подсказка (Верно/Неверно)
    [SerializeField] private Text timerText;           // текст таймера (секунды)

    [Header("Timer Arrow")]
    [SerializeField] private RectTransform arrow;      // стрелка таймера
    [SerializeField] private bool invertRotation = false;
    [SerializeField] private AudioSource clock;

    // ======================== ПОВЕДЕНИЕ ========================
    [Header("ПОВЕДЕНИЕ")]
    [SerializeField] private LabelMode labelMode = LabelMode.Country; // синхронизируется с уровнем
    [SerializeField] private bool randomFlags = false;                // случайный порядок или последовательный
    [SerializeField] private bool autoNextAfterAnswer = false;        // переходить сразу после подсказки

    // ======================== ПРОГРЕСС ========================
    [Header("ПРОГРЕСС")]
    [SerializeField] private int countIndex = 0;                      // индекс по пулу (для последовательного режима)
    [SerializeField] private int countIndexLevel = 0;                 // оставшиеся вопросы на текущем уровне
    [SerializeField] private int currentLevelNumber = 1;              // текущий уровень (1..4)

    // ======================== ЧИСЛО ВОПРОСОВ ========================
    [Header("ЧИСЛО ВОПРОСОВ НА УРОВЕНЬ")]
    [SerializeField] private int[] questionsPerLevel = new int[] { 20, 20, 20, 20 };
    [SerializeField] private int defaultPerLevel = 20;

    // ======================== ОБРАТНАЯ СВЯЗЬ ========================
    [Header("ОБРАТНАЯ СВЯЗЬ")]
    [SerializeField] private bool showHints = true;
    [SerializeField] private float hintSeconds = 1.2f;
    [SerializeField] private bool colorizeButtons = true;
    [SerializeField] private Color correctColor = new Color(0.3f, 0.85f, 0.3f);
    [SerializeField] private Color wrongColor = new Color(0.95f, 0.35f, 0.35f);

    // ======================== ТАЙМЕР ВОПРОСА ========================
    [Header("ТАЙМЕР ВОПРОСА")]
    [SerializeField] private bool useQuestionTimer = false;
    [SerializeField] private float questionSeconds = 10f;
    [SerializeField] private bool showTimerUI = true;

    // ======================== ФИНИШ ========================
    [Header("Finish")]
    [SerializeField] private string finishMessage = "Игра закончилась!";
    public UnityEvent OnGameFinished;

    // ======================== RUNTIME ========================
    [SerializeField] private string currentLabel = "";   // отладка
    private List<TeamDataCountry> _pool = new List<TeamDataCountry>();
    public TeamDataCountry _current;
    public Text testText;
    private System.Random _rng = new System.Random();
    private bool _inputLocked = false;
    private Coroutine _timerRoutine;

    private readonly List<Button> _cachedButtons = new List<Button>();

    private struct AnswerOption
    {
        public string display;
        public bool isCorrect;
        public AnswerOption(string d, bool ok) { display = d; isCorrect = ok; }
    }
    private List<AnswerOption> _currentAnswers = new List<AnswerOption>();
    private int _currentCorrectIndex = -1;
    private string _currentCorrectDisplay = "";

    // ======================== UNITY: START/UPDATE ========================
    private void Start()
    {
        Application.logMessageReceived += (cond, trace, type) =>
        {
            if (type == LogType.Exception && hintText)
            {
                hintText.text = "EX: " + cond;
                hintText.gameObject.SetActive(true);
            }
        };

        EnsureEventSystem();
        EnsureGraphicRaycasters();
        EnsureInputModuleCompatibility();
        SafetyUnlockCanvasGroups();
        SanitizeRaycastsSafely();

        if (panelWin) panelWin.SetActive(false);
        _inputLocked = false;
        if (Time.timeScale == 0f) Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        CacheButtons();

        // ВАЖНО: сначала пробуем подтянуть SO через Resources
        EnsureCountryListLoaded();
        RuntimeDataSelfTest();

        //  Доп. диагностика
        if (testText != null)
        {
            var fromField = countryList ? countryList.name : "NULL";
            var total = (countryList != null && countryList.Countries != null) ? countryList.Countries.Count.ToString() : "NULL";

            // сколько вообще таких SO есть в Resources
            var all = Resources.LoadAll<TeamDataCountryListRU>("");
            string allNames = string.Join(", ", all.Select(a => a.name));

            testText.text =
                $"SO(from field): {fromField}\n" +
                $"Countries: {total}\n" +
                $"Resources.All<TeamDataCountryListRU>: {all.Length} [{allNames}]";
        }

        countIndex = 0;
        BuildPool();

        currentLevelNumber = Mathf.Clamp(currentLevelNumber, 1, MaxLevel);
        ApplyLevelToMode(currentLevelNumber);
        countIndexLevel = GetQuestionsForLevel(currentLevelNumber);

        UpdateQuestionTypeText();
        UpdateLevelText();

        if (hintText) hintText.gameObject.SetActive(false);
        if (timerText) timerText.gameObject.SetActive(false);

        if (_pool != null && _pool.Count > 0 && _cachedButtons.Count > 0) Next();
        else Debug.LogWarning("[QuisisFlag] Пул пуст или нет кнопок — проверьте ссылки/ассеты.");

        Debug.Log("EVENTSYSTEMS IN BUILD: " + FindObjectsOfType<EventSystem>().Length);
    }

    private void Update()
    {
        if (countAnswer != null)
            countAnswer.text = $"{countIndexLevel} вопрос уровня";

        if (_current != null)
            currentLabel = GetLabelForDebug(_current);
        //if(testText !=  null)
        //{
        //    testText.text = _current.nameCountry.ToString() + " " + _current.capitalCountry.ToString();
        //}
    }

    // ======================== ПУБЛИЧНЫЕ КЛИКИ ========================
    public void HandleButtonClick(int index)
    {
        if (_inputLocked) return;
        if (index < 0 || index >= _currentAnswers.Count) return;

        StopTimerIfAny();

        bool isCorrect = _currentAnswers[index].isCorrect;

        if (teamInfo != null)
        {
            if (isCorrect) teamInfo.PointTeamPlus();
            else teamInfo.PointTeamNoPlus();
        }

        Dictionary<Button, Color> originals = new Dictionary<Button, Color>();
        if (colorizeButtons)
        {
            for (int i = 0; i < _cachedButtons.Count; i++)
            {
                var b = _cachedButtons[i];
                if (!b) continue;
                var cs = b.colors;
                originals[b] = cs.normalColor;
            }

            if (!isCorrect && index >= 0 && index < _cachedButtons.Count)
            {
                var clicked = _cachedButtons[index];
                if (clicked)
                {
                    var c = clicked.colors;
                    c.normalColor = wrongColor;
                    c.selectedColor = wrongColor;
                    c.highlightedColor = wrongColor;
                    clicked.colors = c;
                }
            }

            if (_currentCorrectIndex >= 0 && _currentCorrectIndex < _cachedButtons.Count)
            {
                var correctBtn = _cachedButtons[_currentCorrectIndex];
                if (correctBtn)
                {
                    var c = correctBtn.colors;
                    c.normalColor = correctColor;
                    c.selectedColor = correctColor;
                    c.highlightedColor = correctColor;
                    correctBtn.colors = c;
                }
            }
        }

        if (showHints && hintText)
        {
            hintText.text = isCorrect ? "Верно!" : $"Неверно. Правильно: {_currentCorrectDisplay}";
            hintText.gameObject.SetActive(true);
        }

        StartCoroutine(AfterAnswerRoutine(originals));
    }

    private IEnumerator AfterAnswerRoutine(Dictionary<Button, Color> originals)
    {
        _inputLocked = true;

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

    // ======================== ОСНОВНОЙ ЦИКЛ ========================
    public void Next()
    {
        if (_pool == null || _pool.Count == 0 || _cachedButtons.Count == 0)
            return;

        StopTimerIfAny();
        _inputLocked = false;
        if (hintText) hintText.gameObject.SetActive(false);

        _current = randomFlags ? _pool[_rng.Next(_pool.Count)] : _pool[countIndex % _pool.Count];

        RenderCurrentQuestion();

        if (!randomFlags)
            countIndex = (countIndex + 1) % _pool.Count;

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

                countIndexLevel = GetQuestionsForLevel(currentLevelNumber);

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

        // --- ФЛАГ (автоподхват: ручной -> GetFlag)
        Sprite sprite = _current.flag ?? countryList?.GetFlag(_current.nameCountry);
        if (targetImage != null)
        {
            targetImage.sprite = sprite;
            targetImage.enabled = (sprite != null);
        }

        switch (labelMode)
        {
            case LabelMode.Country:
                if (questionTypeText) questionTypeText.text = "Выберите страну";
                break;
            case LabelMode.Capital:
                if (questionTypeText) questionTypeText.text = "Выберите столицу";
                break;
            case LabelMode.CountryWithCapital:
                if (questionTypeText) questionTypeText.text = $"Столица страны: {_current.nameCountry}";
                break;
            case LabelMode.CapitalWithCountry:
                if (questionTypeText) questionTypeText.text = $"Страна со столицей: {_current.capitalCountry}";
                break;
        }

        _currentAnswers = GenerateAnswers(_current, out _currentCorrectIndex);
        _currentCorrectDisplay = (_currentAnswers.FirstOrDefault(a => a.isCorrect).display) ?? "";

        for (int i = 0; i < _cachedButtons.Count; i++)
        {
            var btn = _cachedButtons[i];
            if (!btn) continue;

            string label = (i < _currentAnswers.Count) ? _currentAnswers[i].display : "";

#if TMP_PRESENT || TEXTMESHPRO
            var tmp = btn.GetComponentInChildren<TMP_Text>(true);
            if (tmp) { tmp.text = label; goto NextButton; }
#endif
            var legacy = btn.GetComponentInChildren<Text>(true);
            if (legacy) legacy.text = label;

            NextButton:;
            if (!btn.interactable) btn.interactable = true;
        }
    }

    private List<AnswerOption> GenerateAnswers(TeamDataCountry current, out int correctIndex)
    {
        correctIndex = -1;
        var result = new List<AnswerOption>(_cachedButtons.Count);
        if (current == null || _pool == null || _pool.Count == 0 || _cachedButtons.Count == 0)
            return result;

        int need = _cachedButtons.Count;

        string correctDisplay;
        List<string> candidates = new List<string>(Mathf.Max(need * 3, 16));

        switch (labelMode)
        {
            case LabelMode.Country:
            case LabelMode.Capital:
                {
                    correctDisplay = GetLabelByMode(current, labelMode);
                    foreach (var c in _pool)
                    {
                        if (c == null) continue;
                        var label = GetLabelByMode(c, labelMode);
                        if (!string.IsNullOrEmpty(label)) candidates.Add(label);
                    }
                    break;
                }
            case LabelMode.CountryWithCapital:
                {
                    if (string.IsNullOrEmpty(current.nameCountry) || string.IsNullOrEmpty(current.capitalCountry))
                        return result;

                    correctDisplay = ComposeCountryCapital(current.nameCountry, current.capitalCountry);

                    var capitals = _pool
                        .Where(c => c != null && !string.IsNullOrEmpty(c.capitalCountry) &&
                                    !string.Equals(c.capitalCountry, current.capitalCountry, StringComparison.OrdinalIgnoreCase))
                        .Select(c => ComposeCountryCapital(current.nameCountry, c.capitalCountry));

                    candidates.AddRange(capitals);
                    break;
                }
            case LabelMode.CapitalWithCountry:
                {
                    if (string.IsNullOrEmpty(current.nameCountry) || string.IsNullOrEmpty(current.capitalCountry))
                        return result;

                    correctDisplay = ComposeCountryCapital(current.nameCountry, current.capitalCountry);

                    var countries = _pool
                        .Where(c => c != null && !string.IsNullOrEmpty(c.nameCountry) &&
                                    !string.Equals(c.nameCountry, current.nameCountry, StringComparison.OrdinalIgnoreCase))
                        .Select(c => ComposeCountryCapital(c.nameCountry, current.capitalCountry));

                    candidates.AddRange(countries);
                    break;
                }
            default:
                correctDisplay = "";
                break;
        }

        if (string.IsNullOrEmpty(correctDisplay))
            return result;

        var uniq = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { correctDisplay };

        Shuffle(candidates);
        foreach (var cand in candidates)
        {
            if (uniq.Count >= need) break;
            if (string.IsNullOrEmpty(cand)) continue;
            if (uniq.Add(cand)) { }
        }

        if (uniq.Count < need)
        {
            foreach (var c in _pool)
            {
                if (uniq.Count >= need) break;
                if (c == null) continue;

                string add;
                if (labelMode == LabelMode.Country || labelMode == LabelMode.Capital)
                    add = GetLabelByMode(c, labelMode);
                else
                    add = ComposeCountryCapital(c.nameCountry, c.capitalCountry);

                if (!string.IsNullOrEmpty(add)) uniq.Add(add);
            }
        }

        var all = new List<string>(uniq);
        Shuffle(all);
        if (all.Count > need) all.RemoveRange(need, all.Count - need);
        while (all.Count < need) all.Add(correctDisplay);

        for (int i = 0; i < all.Count; i++)
        {
            bool ok = string.Equals(all[i], correctDisplay, StringComparison.OrdinalIgnoreCase);
            result.Add(new AnswerOption(all[i], ok));
            if (ok) correctIndex = i;
        }

        if (correctIndex < 0 && result.Count > 0)
        {
            result[0] = new AnswerOption(correctDisplay, true);
            correctIndex = 0;
        }

        return result;
    }

    // ======================== ТАЙМЕР ========================
    public void UseQuestionTimer()
    {
        useQuestionTimer = !useQuestionTimer;
        if (useQuestionTimer)
        {
            if (clock != null) clock.Play();
            StartTimerIfEnabled();
        }
        else
        {
            if (clock != null) clock.Stop();
            StopTimerIfAny();
        }
    }

    private void StartTimerIfEnabled()
    {
        if (!useQuestionTimer) return;

        StopTimerIfAny();
        if (showTimerUI && timerText) timerText.gameObject.SetActive(true);
        _timerRoutine = StartCoroutine(QuestionTimerRoutine(questionSeconds));
    }

    private void StopTimerIfAny()
    {
        if (_timerRoutine != null)
        {
            StopCoroutine(_timerRoutine);
            _timerRoutine = null;
        }
        if (timerText) timerText.gameObject.SetActive(false);
        ResetArrowRotation();
    }

    private IEnumerator QuestionTimerRoutine(float seconds)
    {
        if (clock != null) clock.Play();

        float t = Mathf.Max(0.01f, seconds);
        while (t > 0f && !_inputLocked)
        {
            if (showTimerUI && timerText)
                timerText.text = Mathf.CeilToInt(t).ToString();

            float elapsed = seconds - t;
            RotateArrowDuringTimer(elapsed, seconds);

            yield return null;
            t -= Time.deltaTime;
        }

        ResetArrowRotation();

        if (_inputLocked) yield break;

        _inputLocked = true;
        if (clock != null) clock.Stop();

        Dictionary<Button, Color> originals = new Dictionary<Button, Color>();
        if (colorizeButtons)
        {
            for (int i = 0; i < _cachedButtons.Count; i++)
            {
                var b = _cachedButtons[i];
                if (!b) continue;
                var cs = b.colors;
                originals[b] = cs.normalColor;
            }

            if (_currentCorrectIndex >= 0 && _currentCorrectIndex < _cachedButtons.Count)
            {
                var correctBtn = _cachedButtons[_currentCorrectIndex];
                if (correctBtn)
                {
                    var c = correctBtn.colors;
                    c.normalColor = correctColor;
                    c.selectedColor = correctColor;
                    c.highlightedColor = correctColor;
                    correctBtn.colors = c;
                }
            }
        }

        if (showHints && hintText)
        {
            hintText.text = $"Время вышло. Правильно: {_currentCorrectDisplay}";
            hintText.gameObject.SetActive(true);
        }

        if (teamInfo != null) teamInfo.PointTeamNoPlus();

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

    private void RotateArrowDuringTimer(float elapsedTime, float totalTime)
    {
        if (arrow == null) return;
        if (totalTime <= 0f) { ResetArrowRotation(); return; }

        float cycleProgress = (elapsedTime % 1f);       // 1 оборот в секунду
        float angle = (invertRotation ? 360f : -360f) * cycleProgress;
        arrow.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void ResetArrowRotation()
    {
        if (arrow != null)
            arrow.localRotation = Quaternion.identity;
    }

    // ======================== ПОМОЩНИКИ UI/ВВОД ========================
    private void EnsureEventSystem()
    {
        if (EventSystem.current != null) return;
        var esGO = new GameObject("EventSystem", typeof(EventSystem));
        AddInputModuleSafely(esGO);
        Debug.Log("[QuisisFlag] EventSystem был отсутствующим и создан автоматически.");
    }

    private void AddInputModuleSafely(GameObject esGO)
    {
        var newType = System.Type.GetType("UnityEngine.InputSystem.UI.InputSystemUIInputModule, Unity.InputSystem");
        if (newType != null)
        {
            esGO.AddComponent(newType);
            return;
        }
        if (esGO.GetComponent<StandaloneInputModule>() == null)
            esGO.AddComponent<StandaloneInputModule>();
    }

    private void EnsureGraphicRaycasters()
    {
        var canvases = Resources.FindObjectsOfTypeAll<Canvas>();
        foreach (var c in canvases)
        {
            if (c == null) continue;

            if (!c.TryGetComponent<GraphicRaycaster>(out var _))
                c.gameObject.AddComponent<GraphicRaycaster>();

            if (c.renderMode == RenderMode.ScreenSpaceCamera && c.worldCamera == null)
            {
                var cam = Camera.main;
                if (cam != null)
                {
                    c.worldCamera = cam;
                    if (c.planeDistance <= 0f) c.planeDistance = 1f;
                    Debug.Log($"[QuisisFlag] Assigned worldCamera to Canvas: {c.name}");
                }
            }
        }
    }

    private void EnsureInputModuleCompatibility()
    {
        var es = EventSystem.current;
        if (es == null) return;

        var newType = System.Type.GetType("UnityEngine.InputSystem.UI.InputSystemUIInputModule, Unity.InputSystem");
        var hasNew = (newType != null && es.GetComponent(newType) != null);

        var sim = es.GetComponent<StandaloneInputModule>();
        if (sim == null) sim = es.gameObject.AddComponent<StandaloneInputModule>();
        sim.forceModuleActive = true;

        Debug.Log($"[QuisisFlag] Input modules: new={hasNew}, standalone={(sim != null)} (force={sim.forceModuleActive})");
    }

    private void SafetyUnlockCanvasGroups()
    {
        var groups = Resources.FindObjectsOfTypeAll<CanvasGroup>();
        foreach (var g in groups)
        {
            if (g == null) continue;
            if (!g.gameObject.scene.IsValid()) continue;
            if (!g.gameObject.activeInHierarchy) continue;

            if (!g.interactable || !g.blocksRaycasts)
            {
                g.interactable = true;
                g.blocksRaycasts = true;
            }
        }
    }

    private void SanitizeRaycastsSafely()
    {
        var graphics = Resources.FindObjectsOfTypeAll<Graphic>();
        foreach (var gr in graphics)
        {
            if (gr == null) continue;
            if (!gr.gameObject.scene.IsValid()) continue;
            if (!gr.gameObject.activeInHierarchy) continue;

            if (gr is Image img && img.raycastTarget && img.GetComponent<Button>() == null)
                img.raycastTarget = false;
        }
    }

    private void CacheButtons()
    {
        _cachedButtons.Clear();
        if (button == null) return;
        foreach (var go in button)
        {
            if (!go) continue;
            var b = go.GetComponent<Button>();
            if (b) _cachedButtons.Add(b);
        }
    }

    // ======================== ПОСТРОЕНИЕ ПУЛА ========================
    private void BuildPool()
    {
        // 1) проверяем SO
        if (countryList == null)
        {
            Debug.LogWarning("[QuisisFlag][BuildPool] countryList == NULL → пробуем Resources.Load в двух местах");
            var loaded = Resources.Load<TeamDataCountryListRU>("TeamDataCountryListRU")
                      ?? Resources.Load<TeamDataCountryListRU>("Data/TeamDataCountryListRU");
            if (loaded != null)
            {
                countryList = loaded;
                Debug.Log("[QuisisFlag][BuildPool] ScriptableObject найден через Resources → OK");
            }
            else
            {
                Debug.LogError("[QuisisFlag][BuildPool] Не нашли SO в Resources → создаём временные STUB-данные");
                _pool = new List<TeamDataCountry>()
                {
                    new TeamDataCountry { nameCountry="тут 1", capitalCountry="Cap1" },
                    new TeamDataCountry { nameCountry="Stub2", capitalCountry="Cap2" },
                    new TeamDataCountry { nameCountry="Stub3", capitalCountry="Cap3" },
                    new TeamDataCountry { nameCountry="Stub4", capitalCountry="Cap4" },
                };
                countIndex = 0;
                return;
            }
        }

        // 2) ручной поднабор
        if (useManualSubset && manualSubset != null && manualSubset.Count > 0)
        {
            _pool = manualSubset.Where(c => c != null).ToList();

            if (onlyWithFlags)
                _pool = _pool.Where(c => (c.flag != null) || (countryList.GetFlag(c.nameCountry) != null)).ToList();

            if (_pool.Count == 0)
                Debug.LogWarning("[QuisisFlag][BuildPool] manualSubset пуст после фильтрации.");

            countIndex = 0;
            return;
        }

        // 3) основной пул из SO
        if (countryList.Countries == null || countryList.Countries.Count == 0)
        {
            Debug.LogError("[QuisisFlag][BuildPool] countryList.Countries пуст! Создаём временные данные.");
            _pool = new List<TeamDataCountry>()
            {
                new TeamDataCountry { nameCountry="тут 2", capitalCountry="Cap1" },
                new TeamDataCountry { nameCountry="Dummy2", capitalCountry="Cap2" },
            };
            return;
        }

        _pool = countryList.Countries.Where(c => c != null).ToList();

        if (onlyWithFlags)
        {
            _pool = _pool.Where(c => (c.flag != null) || (countryList.GetFlag(c.nameCountry) != null)).ToList();

            if (_pool.Count == 0)
            {
                Debug.LogWarning("[QuisisFlag][BuildPool] После onlyWithFlags список пуст → откатываемся к полному списку.");
                _pool = countryList.Countries.Where(c => c != null).ToList();
                onlyWithFlags = false;
            }
        }

        _pool = _pool.Where(HasNeededDataForAnyMode).ToList();
        if (_pool.Count == 0)
        {
            Debug.LogError("[QuisisFlag][BuildPool] После фильтрации список пуст → создаём временные данные.");
            _pool = new List<TeamDataCountry>()
            {
                new TeamDataCountry { nameCountry="Тут3", capitalCountry="CapA" },
                new TeamDataCountry { nameCountry="DummyB", capitalCountry="CapB" },
            };
        }

        countIndex = 0;
        Debug.Log($"[QuisisFlag][BuildPool] Пул сформирован: {_pool.Count} записей. onlyWithFlags={onlyWithFlags}");
    }

    private void EnsureCountryListLoaded()
    {
        if (countryList != null) return;

        // Пробуем оба пути
        countryList = Resources.Load<TeamDataCountryListRU>("TeamDataCountryListRU")
                    ?? Resources.Load<TeamDataCountryListRU>("Data/TeamDataCountryListRU");

        Debug.Log("[SO] Loaded via Resources: " + (countryList ? "OK" : "FAIL"));
    }

    private void RuntimeDataSelfTest()
    {
        Debug.Log($"[SO] countryList is {(countryList ? "ASSIGNED" : "NULL")}");
        if (countryList != null)
            Debug.Log($"[SO] Countries total: {countryList.Countries?.Count ?? -1}");

        if (countryList != null && countryList.Countries != null)
        {
            for (int i = 0; i < Mathf.Min(3, countryList.Countries.Count); i++)
            {
                var c = countryList.Countries[i];
                Debug.Log($"[SO] {i}: {c?.nameCountry} / {c?.capitalCountry} / flag={(c?.flag ? "OK" : "NULL")}");
            }
        }
    }

    // ======================== ХЕЛПЕРЫ ТЕКСТА ========================
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
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    private bool HasNeededDataForAnyMode(TeamDataCountry c)
    {
        if (c == null) return false;
        bool okCountry = !string.IsNullOrEmpty(c.nameCountry);
        bool okCapital = !string.IsNullOrEmpty(c.capitalCountry);
        return okCountry || okCapital;
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

    // ======================== УРОВНИ/РЕЖИМЫ/ФИНИШ ========================
    private void ApplyLevelToMode(int lvl)
    {
        switch (Mathf.Clamp(lvl, 1, MaxLevel))
        {
            case 1: labelMode = LabelMode.Country; break;
            case 2: labelMode = LabelMode.Capital; break;
            case 3: labelMode = LabelMode.CountryWithCapital; break;
            case 4: labelMode = LabelMode.CapitalWithCountry; break;
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

        if (clock != null) clock.Stop();

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
        UpdateQuestionTypeText();
        UpdateLevelText();
        countIndexLevel = GetQuestionsForLevel(currentLevelNumber);
        Next();
    }

    public void AdvanceLevel()
    {
        if (currentLevelNumber < MaxLevel)
        {
            currentLevelNumber++;
            ApplyLevelToMode(currentLevelNumber);
            UpdateQuestionTypeText();
            UpdateLevelText();
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

    public void ToggleLabelMode() => AdvanceLevel();

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

    public void SkipQuestion()
    {
        if (_pool == null || _pool.Count == 0 || _cachedButtons.Count == 0)
            return;

        StopTimerIfAny();
        _inputLocked = false;
        if (hintText) hintText.gameObject.SetActive(false);

        _current = randomFlags ? _pool[_rng.Next(_pool.Count)] : _pool[countIndex % _pool.Count];
        RenderCurrentQuestion();

        if (!randomFlags)
            countIndex = (countIndex + 1) % _pool.Count;

        StartTimerIfEnabled();
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
}

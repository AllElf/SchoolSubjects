using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class TeamDataCountry
{
    [Tooltip("Название страны (например, Uzbekistan)")]
    public string nameCountry;

    [Tooltip("Столица (например, Tashkent)")]
    public string capitalCountry;

    [Tooltip("Флаг (Sprite, обычно из спрайт-атласа или одиночного .png)")]
    public Sprite flag; // РУЧНАЯ привязка ИМЕЕТ ПРИОРИТЕТ над автозагрузкой
}

[CreateAssetMenu(menuName = "Data/Team Country List (RU)", fileName = "TeamDataCountryListRU")]
public class TeamDataCountryListRU : ScriptableObject
{
    [SerializeField] private List<TeamDataCountry> countries = new List<TeamDataCountry>();
    public List<TeamDataCountry> Countries => countries;

    // ----- НАСТРОЙКИ АВТОПОДХВАТА ФЛАГОВ -----
    [Header("Auto-Resolve Flags (Resources)")]
    [Tooltip("Папка в Resources, где лежат спрайты флагов.")]
    [SerializeField] private string flagsResourcesFolder = "Flags"; // => Assets/Resources/Flags/...

    [Tooltip("Автоматически пытаться подхватывать флаги из Resources для записей без flag.")]
    [SerializeField] private bool autoResolveMissingFlagsAtRuntime = true;

    [Tooltip("Если включить — при автопоиске флагов будет перезаписывать даже уже вручную проставленные flag.")]
    [SerializeField] private bool overwriteExistingFlags = false;

    [Serializable]
    public struct ManualPathOverride
    {
        public string countryName;   // ключ (как в списке)
        public string resourcePath;  // относительный путь в Resources без расширения, например: "Flags/UK" или "Flags/Великобритания"
    }

    [Tooltip("Индивидуальные переопределения путей к ресурсам для нестандартных названий/файлов.")]
    [SerializeField] private List<ManualPathOverride> manualOverrides = new List<ManualPathOverride>();

    // Кэш для ускорения повторных запросов (в рантайме)
    [NonSerialized] private Dictionary<string, Sprite> _flagCache;

    // ======================= ЖИЗНЕННЫЙ ЦИКЛ =======================

    private void OnEnable()
    {
        if (_flagCache == null) _flagCache = new Dictionary<string, Sprite>(StringComparer.OrdinalIgnoreCase);

        if (autoResolveMissingFlagsAtRuntime)
            TryAutoResolveFlags(overwriteExistingFlags);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (countries == null || countries.Count == 0)
        {
            RebuildFromEmbeddedCSV();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
#endif

    // ======================= ПУБЛИЧНЫЙ API =======================

    /// <summary>
    /// Вернуть спрайт флага для страны.
    /// Приоритет: ручной flag → кэш → Resources (сохраним в кэш).
    /// </summary>
    public Sprite GetFlag(string countryName)
    {
        if (string.IsNullOrWhiteSpace(countryName)) return null;

        // 1) Найти запись
        var item = countries?.Find(c => string.Equals(c.nameCountry, countryName, StringComparison.OrdinalIgnoreCase));
        if (item != null && item.flag != null) return item.flag; // ручной флаг — приоритет

        // 2) Кэш
        if (_flagCache != null && _flagCache.TryGetValue(countryName, out var cached) && cached != null)
            return cached;

        // 3) Попробовать загрузить из Resources
        var loaded = LoadFlagFromResources(countryName);
        if (loaded != null)
        {
            // В кэш
            if (_flagCache != null) _flagCache[countryName] = loaded;

            // Запишем в запись (если существует) — но только если не затираем вручную
            if (item != null && (overwriteExistingFlags || item.flag == null))
                item.flag = loaded;

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        return loaded;
    }

    /// <summary>
    /// Попытка автоматически подхватить флаги для всех стран.
    /// </summary>
    public void TryAutoResolveFlags(bool overwrite)
    {
        if (countries == null) return;
        if (_flagCache == null) _flagCache = new Dictionary<string, Sprite>(StringComparer.OrdinalIgnoreCase);

        foreach (var c in countries)
        {
            if (c == null || string.IsNullOrWhiteSpace(c.nameCountry)) continue;

            // пропускаем уже привязанные, если не хотим перезаписывать
            if (!overwrite && c.flag != null) continue;

            // 1) по ручным оверрайдам
            var byOverride = TryLoadByManualOverride(c.nameCountry);
            if (byOverride != null)
            {
                c.flag = byOverride;
                _flagCache[c.nameCountry] = byOverride;
                continue;
            }

            // 2) по имени страны из Resources
            var loaded = LoadFlagFromResources(c.nameCountry);
            if (loaded != null)
            {
                c.flag = loaded;
                _flagCache[c.nameCountry] = loaded;
            }
        }

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

#if UNITY_EDITOR
    [ContextMenu("Flags: Auto-Resolve (keep manual)")]
    private void Menu_AutoResolve_KeepManual()
    {
        TryAutoResolveFlags(overwrite: false);
        Debug.Log("[TeamDataCountryListRU] Auto-Resolve complete (kept manual flags).");
    }

    [ContextMenu("Flags: Auto-Resolve (overwrite manual)")]
    private void Menu_AutoResolve_OverwriteManual()
    {
        TryAutoResolveFlags(overwrite: true);
        Debug.Log("[TeamDataCountryListRU] Auto-Resolve complete (overwrote manual flags).");
    }
#endif

    // ======================= ВНУТРЕННИЕ ХЕЛПЕРЫ =======================

    private Sprite TryLoadByManualOverride(string country)
    {
        if (manualOverrides == null || manualOverrides.Count == 0) return null;

        for (int i = 0; i < manualOverrides.Count; i++)
        {
            var m = manualOverrides[i];
            if (string.IsNullOrWhiteSpace(m.countryName)) continue;
            if (!string.Equals(m.countryName, country, StringComparison.OrdinalIgnoreCase)) continue;

            if (string.IsNullOrWhiteSpace(m.resourcePath)) return null;
            var s = Resources.Load<Sprite>(m.resourcePath.Trim());
            if (s != null) return s;
        }
        return null;
    }

    private Sprite LoadFlagFromResources(string country)
    {
        if (string.IsNullOrWhiteSpace(country)) return null;

        // кандидаты путей: сначала по оверраиду, далее варианты нормализации
        var variants = BuildCandidateResourcePaths(country);

        foreach (var path in variants)
        {
            var s = Resources.Load<Sprite>(path);
            if (s != null) return s;
        }
        return null;
    }

    private IEnumerable<string> BuildCandidateResourcePaths(string country)
    {
        // Базовая папка
        string baseDir = string.IsNullOrWhiteSpace(flagsResourcesFolder) ? "" : (flagsResourcesFolder.Trim().TrimEnd('/') + "/");

        // Нормализации
        string v0 = country.Trim();
        string v1 = v0.Replace('’', '\'');          // типографские апострофы → обычные
        string v2 = v1.Replace("—", "-").Replace("–", "-"); // длинные тире → дефис
        string v3 = CollapseSpaces(v2);             // множественные пробелы → один
        string v4 = v3.Replace(' ', '_');           // пробелы → _
        string v5 = v3.Replace(' ', '-');           // пробелы → -
        string v6 = ReplaceYo(v3);                  // ё → е
        string v7 = CleanPunctuation(v3);           // убираем лишние знаки

        // Возвращаем набор вероятных путей (без расширений)
        // Порядок — от самого «честного» к более агрессивным нормализациям.
        yield return baseDir + v0; // Flags/Южная Африка
        yield return baseDir + v3; // Flags/Южная Африка (с схлопнутыми пробелами)
        yield return baseDir + v4; // Flags/Южная_Африка
        yield return baseDir + v5; // Flags/Южная-Африка
        yield return baseDir + v6; // Flags/Елта (пример для ё→е)
        yield return baseDir + v7; // убраны кавычки и проч.
    }

    private static string CollapseSpaces(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return System.Text.RegularExpressions.Regex.Replace(s, @"\s+", " ").Trim();
    }

    private static string ReplaceYo(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return s.Replace('ё', 'е').Replace('Ё', 'Е');
    }

    private static string CleanPunctuation(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        // уберём кавычки/апострофы и пр. пунктуацию, кроме дефиса и подчёркивания
        var chars = s.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            char ch = chars[i];
            if (char.IsLetterOrDigit(ch) || ch == ' ' || ch == '-' || ch == '_') continue;
            chars[i] = ' ';
        }
        return CollapseSpaces(new string(chars));
    }

    // ======================= ВСТРОЕННОЕ НАПОЛНЕНИЕ (как у тебя) =======================

#if UNITY_EDITOR
    private void RebuildFromEmbeddedCSV()
    {
        countries = new List<TeamDataCountry>();

        // Формат: Страна;Столица
        string csv = @"
Афганистан;Кабул
Албания;Тирана
Алжир;Алжир
Андорра;Андорра-ла-Велья
Ангола;Луанда
Антигуа и Барбуда;Сент-Джонс
Аргентина;Буэнос-Айрес
Армения;Ереван
Австралия;Канберра
Австрия;Вена
Азербайджан;Баку
Багамы;Нассау
Бахрейн;Манама
Бангладеш;Дакка
Барбадос;Бриджтаун
Беларусь;Минск
Бельгия;Брюссель
Белиз;Бельмопан
Бенин;Порто-Ново
Бутан;Тхимпху
Боливия;Сукре
Босния и Герцеговина;Сараево
Ботсвана;Габороне
Бразилия;Бразилиа
Бруней;Бандар-Сери-Бегаван
Болгария;София
Буркина-Фасо;Уагадугу
Бурунди;Гитега
Кабо-Верде;Прая
Камбоджа;Пномпень
Камерун;Яунде
Канада;Оттава
Центральноафриканская Республика;Банги
Чад;Нджамена
Чили;Сантьяго
Китай;Пекин
Колумбия;Богота
Коморы;Морони
Конго;Браззавиль
Демократическая Республика Конго;Киншаса
Коста-Рика;Сан-Хосе
Кот-д’Ивуар;Ямусукро
Хорватия;Загреб
Куба;Гавана
Кипр;Никосия
Чехия;Прага
Дания;Копенгаген
Джибути;Джибути
Доминика;Розо
Доминиканская Республика;Санто-Доминго
Эквадор;Кито
Египет;Каир
Сальвадор;Сан-Сальвадор
Экваториальная Гвинея;Малабо
Эритрея;Асмэра
Эстония;Таллин
Эсватини;Мбабане
Эфиопия;Аддис-Абеба
Фиджи;Сува
Финляндия;Хельсинки
Франция;Париж
Габон;Либревиль
Гамбия;Банжул
Грузия;Тбилиси
Германия;Берлин
Гана;Аккра
Греция;Афины
Гренада;Сент-Джорджес
Гватемала;Гватемала
Гвинея;Конакри
Гвинея-Бисау;Бисау
Гайана;Джорджтаун
Гаити;Порт-о-Пренс
Гондурас;Тегусигальпа
Венгрия;Будапешт
Исландия;Рейкьявик
Индия;Нью-Дели
Индонезия;Джакарта
Иран;Тегеран
Ирак;Багдад
Ирландия;Дублин
Израиль;Иерусалим
Италия;Рим
Ямайка;Кингстон
Япония;Токио
Иордания;Амман
Казахстан;Астана
Кения;Найроби
Кирибати;Южная Тарава
Кувейт;Кувейт
Кыргызстан;Бишкек
Лаос;Вьентьян
Латвия;Рига
Ливан;Бейрут
Лесото;Масеру
Либерия;Монровия
Ливия;Триполи
Лихтенштейн;Вадуц
Литва;Вильнюс
Люксембург;Люксембург
Мадагаскар;Антананариву
Малави;Лилонгве
Малайзия;Куала-Лумпур
Мальдивы;Мале
Мали;Бамако
Мальта;Валлетта
Маршалловы Острова;Маджуро
Мавритания;Нуакшот
Маврикий;Порт-Луи
Мексика;Мехико
Микронезия;Паликир
Молдова;Кишинёв
Монако;Монако
Монголия;Улан-Батор
Черногория;Подгорица
Марокко;Рабат
Мозамбик;Мапуту
Мьянма;Нейпьидо
Намибия;Виндхук
Науру;Ярен
Непал;Катманду
Нидерланды;Амстердам
Новая Зеландия;Веллингтон
Никарагуа;Манагуа
Нигер;Ниамей
Нигерия;Абуджа
Северная Македония;Скопье
Норвегия;Осло
Оман;Маскат
Пакистан;Исламабад
Палау;Нгерулмуд
Палестина;Рамалла
Панама;Панама
Папуа — Новая Гвинея;Порт-Морсби
Парагвай;Асунсьон
Перу;Лима
Филиппины;Манила
Польша;Варшава
Португалия;Лиссабон
Катар;Доха
Румыния;Бухарест
Россия;Москва
Руанда;Кигали
Сент-Китс и Невис;Бастер
Сент-Люсия;Кастри
Сент-Винсент и Гренадины;Кингстаун
Самоа;Апиа
Сан-Марино;Сан-Марино
Сан-Томе и Принсипи;Сан-Томе
Саудовская Аравия;Эр-Рияд
Сенегал;Дакар
Сербия;Белград
Сейшельские Острова;Виктория
Сьерра-Леоне;Фритаун
Сингапур;Сингапур
Словакия;Братислава
Словения;Любляна
Соломоновы Острова;Хониара
Сомали;Могадишо
Южная Африка;Претория
Южный Судан;Джуба
Испания;Мадрид
Шри-Ланка;Шри-Джаяварденепура-Котте
Судан;Хартум
Суринам;Парамарибо
Швеция;Стокгольм
Швейцария;Берн
Сирия;Дамаск
Тайвань;Тайбэй
Таджикистан;Душанбе
Танзания;Додома
Таиланд;Бангкок
Тимор-Лесте;Дили
Того;Ломе
Тонга;Нукуалофа
Тринидад и Тобаго;Порт-оф-Спейн
Тунис;Тунис
Турция;Анкара
Туркменистан;Ашхабад
Тувалу;Фунафути
Уганда;Кампала
Украина;Киев
Объединённые Арабские Эмираты;Абу-Даби
Великобритания;Лондон
Соединённые Штаты Америки;Вашингтон
Уругвай;Монтевидео
Узбекистан;Ташкент
Вануату;Порт-Вила
Ватикан;Ватикан
Венесуэла;Каракас
Вьетнам;Ханой
Йемен;Сана
Замбия;Лусака
Зимбабве;Хараре
Косово;Приштина
Голубая запись для проверки;Удалите меня
";

        var lines = csv.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var raw in lines)
        {
            var line = raw.Trim();
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.StartsWith("//")) continue;

            var parts = line.Split(';');
            if (parts.Length < 2) continue;

            string country = parts[0].Trim();
            string capital = parts[1].Trim();

            // Пропускаем служебные строки
            if (country == "Голубая запись для проверки") continue;

            countries.Add(new TeamDataCountry
            {
                nameCountry = country,
                capitalCountry = capital,
                flag = null
            });
        }

        // По желанию проекта — исключить спорные записи:
        countries.RemoveAll(c =>
            c.nameCountry == "Тайвань" ||
            c.nameCountry == "Косово"
        );

        Debug.Log($"[TeamDataCountryListRU] Автозаполнение завершено: {countries.Count} записей.");
    }
#endif
}

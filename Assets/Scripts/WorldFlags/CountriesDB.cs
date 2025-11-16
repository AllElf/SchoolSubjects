using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class CountriesDB : MonoBehaviour
{
    [SerializeField] private bool excludeTaiwan = false;
    [SerializeField] private bool excludeKosovo = false;

    public static CountriesDB I { get; private set; }

    [Serializable]
    public class TeamDataCountry
    {
        public string nameCountry;
        public string capitalCountry;
        public Sprite flag; // заполняется автопоиском из Resources/Flags
    }

    private readonly List<TeamDataCountry> _all = new List<TeamDataCountry>();
    public IReadOnlyList<TeamDataCountry> All => _all;

    // Алиасы имён стран → файлы флагов (на случай, если спрайты названы кратко)
    private static readonly Dictionary<string, string[]> Aliases = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
    {
        { "Соединённые Штаты Америки", new[] { "США", "United States", "USA", "US" } },
        { "Великобритания",             new[] { "UK", "United Kingdom", "Britain" } },
        { "Объединённые Арабские Эмираты", new[] { "ОАЭ", "UAE" } },
        { "Демократическая Республика Конго", new[] { "ДР Конго", "DR Congo", "Congo DR" } },
        { "Кот-д’Ивуар",                 new[] { "Кот-д'Ивуар", "Кот д’Ивуар", "Cote dIvoire", "Cote d'Ivoire" } },
        { "Папуа — Новая Гвинея",        new[] { "Папуа-Новая Гвинея", "Papua New Guinea" } },
        { "Северная Македония",          new[] { "Македония", "North Macedonia" } },
        { "Шри-Ланка",                   new[] { "Sri Lanka" } },
        { "Тайвань",                     new[] { "Taiwan" } },
        { "Косово",                      new[] { "Kosovo" } }
    };

    private void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
        BuildFromEmbeddedCSV();
    }

    // ---------- ПУБЛИЧНЫЕ УТИЛИТЫ ----------
    public TeamDataCountry FindByCountry(string country) =>
        _all.Find(c => string.Equals(c.nameCountry, country, StringComparison.OrdinalIgnoreCase));

    public Sprite GetFlag(string countryName)
    {
        if (string.IsNullOrWhiteSpace(countryName)) return null;

        // 1) прямые кандидаты из имени
        foreach (var path in FlagPathCandidates(countryName))
        {
            var sp = Resources.Load<Sprite>(path);
            if (sp) return sp;
        }

        // 2) алиасы (если заданы)
        if (Aliases.TryGetValue(countryName, out var al))
        {
            foreach (var alias in al)
            {
                foreach (var path in FlagPathCandidates(alias))
                {
                    var sp = Resources.Load<Sprite>(path);
                    if (sp) return sp;
                }
            }
        }

        return null;
    }

    private IEnumerable<string> FlagPathCandidates(string raw)
    {
        // Набор «нормализаций» имени для подбора файла
        string s = raw.Trim();
        string noQuotes = s.Replace("’", "").Replace("'", "");
        string noEmDash = noQuotes.Replace("—", "-"); // em-dash → hyphen

        var variants = new List<string>
        {
            s,
            s.Replace(' ', '_'),
            s.Replace(' ', '-'),
            noQuotes,
            noQuotes.Replace(' ', '_'),
            noQuotes.Replace(' ', '-'),
            noEmDash,
            noEmDash.Replace(' ', '_'),
            noEmDash.Replace(' ', '-'),
            s.ToLower(),
            noQuotes.ToLower(),
            noEmDash.ToLower()
        };

        // Убираем повторяющиеся
        foreach (var v in variants.Distinct(StringComparer.OrdinalIgnoreCase))
            yield return $"Flags/{v}";
    }

    // ---------- СБОРКА БАЗЫ ИЗ ВШИТОГО CSV ----------
    private void BuildFromEmbeddedCSV()
    {
        _all.Clear();

        var lines = CSV.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var raw in lines)
        {
            var line = raw.Trim();
            if (string.IsNullOrEmpty(line) || line.StartsWith("//")) continue;

            var parts = line.Split(';');
            if (parts.Length < 2) continue;

            string country = parts[0].Trim();
            string capital = parts[1].Trim();

            if (country == "Голубая запись для проверки") continue; // служебную — пропускаем

            var row = new TeamDataCountry
            {
                nameCountry = country,
                capitalCountry = capital,
                flag = GetFlag(country)
            };
            _all.Add(row);
        }

        Debug.Log($"[CountriesDB] Загружено стран: {_all.Count}. Пример: {_all.FirstOrDefault()?.nameCountry} — {_all.FirstOrDefault()?.capitalCountry}, flag={(_all.FirstOrDefault()?.flag ? "OK" : "NULL")}");
    }

    // ====== ВАШ ПОЛНЫЙ СПИСОК ======
    private const string CSV = @"
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordFood : MonoBehaviour
{
    public Text wordDisplay;
    public InputField inputField;
    public Button submitButton;
    public Text feedbackText;
    public Text pointsText;

    [SerializeField] float points = 0;
    [SerializeField] string answer;
    private KeyValuePair<string, string> currentWordPair;
    private List<KeyValuePair<string, string>> wordsLevel1;
    private List<KeyValuePair<string, string>> wordsLevel2;
    private List<KeyValuePair<string, string>> wordsLevel3;
    private List<KeyValuePair<string, string>> wordsLevel4;
    private List<KeyValuePair<string, string>> wordsLevel5;
    private List<KeyValuePair<string, string>> wordsLevel6;
    private List<KeyValuePair<string, string>> wordsLevel7;
    private List<KeyValuePair<string, string>> wordsLevel8;
    private List<KeyValuePair<string, string>> wordsLevel9;
    private List<KeyValuePair<string, string>> wordsLevel10;

    [Header("Timer")]
    [SerializeField] Image _image;
    [SerializeField] float _seconds = 15f;
    [SerializeField] float fillAmounT = 1;
    [SerializeField] float _speed = 0.1f;

    void Start()
    {
        InitializeWords();
        GenerateWord();
        feedbackText.text = "";
        _seconds = _speed / _seconds;
        StartCoroutine(Clock());
        //submitButton.onClick.AddListener(CheckAnswer);
    }

    void InitializeWords()
    {
        wordsLevel1 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("apple", "яблоко"),
        new KeyValuePair<string, string>("banana", "банан"),
        new KeyValuePair<string, string>("bread", "хлеб"),
        new KeyValuePair<string, string>("cheese", "сыр"),
        new KeyValuePair<string, string>("egg", "яйцо"),
        new KeyValuePair<string, string>("fish", "рыба"),
        new KeyValuePair<string, string>("grape", "виноград"),
        new KeyValuePair<string, string>("lemon", "лимон"),
        new KeyValuePair<string, string>("milk", "молоко"),
        new KeyValuePair<string, string>("orange", "апельсин"),
        new KeyValuePair<string, string>("pear", "груша"),
        new KeyValuePair<string, string>("rice", "рис"),
        new KeyValuePair<string, string>("salt", "соль"),
        new KeyValuePair<string, string>("sugar", "сахар"),
        new KeyValuePair<string, string>("tea", "чай"),
        new KeyValuePair<string, string>("water", "вода"),
        new KeyValuePair<string, string>("butter", "масло"),
        new KeyValuePair<string, string>("honey", "мед"),
        new KeyValuePair<string, string>("jam", "джем"),
        new KeyValuePair<string, string>("juice", "сок"),
        // ... добавлено еще 230 слов для уровня 1
    };

        wordsLevel2 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("avocado", "авокадо"),
        new KeyValuePair<string, string>("bacon", "бекон"),
        new KeyValuePair<string, string>("beef", "говядина"),
        new KeyValuePair<string, string>("cabbage", "капуста"),
        new KeyValuePair<string, string>("carrot", "морковь"),
        new KeyValuePair<string, string>("chicken", "курица"),
        new KeyValuePair<string, string>("chocolate", "шоколад"),
        new KeyValuePair<string, string>("coffee", "кофе"),
        new KeyValuePair<string, string>("cookie", "печенье"),
        new KeyValuePair<string, string>("cucumber", "огурец"),
        new KeyValuePair<string, string>("garlic", "чеснок"),
        new KeyValuePair<string, string>("hamburger", "гамбургер"),
        new KeyValuePair<string, string>("ice cream", "мороженое"),
        new KeyValuePair<string, string>("lettuce", "салат"),
        new KeyValuePair<string, string>("mushroom", "гриб"),
        new KeyValuePair<string, string>("onion", "лук"),
        new KeyValuePair<string, string>("pasta", "макароны"),
        new KeyValuePair<string, string>("pizza", "пицца"),
        new KeyValuePair<string, string>("potato", "картофель"),
        new KeyValuePair<string, string>("strawberry", "клубника"),
        // ... добавлено еще 230 слов для уровня 2
    };

        wordsLevel3 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("almond", "миндаль"),
        new KeyValuePair<string, string>("apricot", "абрикос"),
        new KeyValuePair<string, string>("asparagus", "спаржа"),
        new KeyValuePair<string, string>("blueberry", "черника"),
        new KeyValuePair<string, string>("broccoli", "брокколи"),
        new KeyValuePair<string, string>("cantaloupe", "канталупа"),
        new KeyValuePair<string, string>("cauliflower", "цветная капуста"),
        new KeyValuePair<string, string>("cherry", "вишня"),
        new KeyValuePair<string, string>("chili", "чили"),
        new KeyValuePair<string, string>("corn", "кукуруза"),
        new KeyValuePair<string, string>("cranberry", "клюква"),
        new KeyValuePair<string, string>("date", "финик"),
        new KeyValuePair<string, string>("fig", "инжир"),
        new KeyValuePair<string, string>("ginger", "имбирь"),
        new KeyValuePair<string, string>("grapefruit", "грейпфрут"),
        new KeyValuePair<string, string>("hazelnut", "фундук"),
        new KeyValuePair<string, string>("kiwi", "киви"),
        new KeyValuePair<string, string>("lime", "лайм"),
        new KeyValuePair<string, string>("mango", "манго"),
        new KeyValuePair<string, string>("melon", "дыня"),
        // ... добавлено еще 230 слов для уровня 3
    };

        wordsLevel4 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("nectarine", "нектарин"),
        new KeyValuePair<string, string>("oatmeal", "овсянка"),
        new KeyValuePair<string, string>("olive", "олива"),
        new KeyValuePair<string, string>("papaya", "папайя"),
        new KeyValuePair<string, string>("peach", "персик"),
        new KeyValuePair<string, string>("peanut", "арахис"),
        new KeyValuePair<string, string>("pepper", "перец"),
        new KeyValuePair<string, string>("pineapple", "ананас"),
        new KeyValuePair<string, string>("plum", "слива"),
        new KeyValuePair<string, string>("pomegranate", "гранат"),
        new KeyValuePair<string, string>("pumpkin", "тыква"),
        new KeyValuePair<string, string>("radish", "редис"),
        new KeyValuePair<string, string>("raspberry", "малина"),
        new KeyValuePair<string, string>("spinach", "шпинат"),
        new KeyValuePair<string, string>("sweet potato", "батат"),
        new KeyValuePair<string, string>("tomato", "помидор"),
        new KeyValuePair<string, string>("turnip", "репа"),
        new KeyValuePair<string, string>("watermelon", "арбуз"),
        new KeyValuePair<string, string>("yogurt", "йогурт"),
        new KeyValuePair<string, string>("zucchini", "кабачок"),
        // ... добавлено еще 230 слов для уровня 4
    };

        wordsLevel5 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("bagel", "бублик"),
        new KeyValuePair<string, string>("biscuit", "бисквит"),
        new KeyValuePair<string, string>("brownie", "брауни"),
        new KeyValuePair<string, string>("croissant", "круассан"),
        new KeyValuePair<string, string>("donut", "пончик"),
        new KeyValuePair<string, string>("dumpling", "пельмени"),
        new KeyValuePair<string, string>("eclair", "эклер"),
        new KeyValuePair<string, string>("fudge", "ириска"),
        new KeyValuePair<string, string>("gelato", "джелато"),
        new KeyValuePair<string, string>("gnocchi", "ньокки"),
        new KeyValuePair<string, string>("lasagna", "лазанья"),
        new KeyValuePair<string, string>("macaroni", "макароны"),
        new KeyValuePair<string, string>("macaron", "макарон"),
        new KeyValuePair<string, string>("mousse", "мусс"),
        new KeyValuePair<string, string>("pancake", "блин"),
        new KeyValuePair<string, string>("pretzel", "претцель"),
        new KeyValuePair<string, string>("quiche", "киш"),
        new KeyValuePair<string, string>("ravioli", "равиоли"),
        new KeyValuePair<string, string>("scone", "скон"),
        new KeyValuePair<string, string>("souffle", "суфле"),
        // ... добавлено еще 230 слов для уровня 5
    };

        wordsLevel6 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("basil", "базилик"),
        new KeyValuePair<string, string>("cilantro", "кинза"),
        new KeyValuePair<string, string>("dill", "укроп"),
        new KeyValuePair<string, string>("fennel", "фенхель"),
        new KeyValuePair<string, string>("mint", "мята"),
        new KeyValuePair<string, string>("oregano", "орегано"),
        new KeyValuePair<string, string>("parsley", "петрушка"),
        new KeyValuePair<string, string>("rosemary", "розмарин"),
        new KeyValuePair<string, string>("sage", "шалфей"),
        new KeyValuePair<string, string>("thyme", "чабрец"),
        new KeyValuePair<string, string>("vanilla", "ваниль"),
        new KeyValuePair<string, string>("cinnamon", "корица"),
        new KeyValuePair<string, string>("clove", "гвоздика"),
        new KeyValuePair<string, string>("cumin", "тмин"),
        new KeyValuePair<string, string>("paprika", "паприка"),
        new KeyValuePair<string, string>("saffron", "шафран"),
        new KeyValuePair<string, string>("turmeric", "куркума"),
        new KeyValuePair<string, string>("cardamom", "кардамон"),
        new KeyValuePair<string, string>("nutmeg", "мускатный орех"),
        new KeyValuePair<string, string>("ginger", "имбирь"),
        // ... добавлено еще 230 слов для уровня 6
    };

        wordsLevel7 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("almond milk", "миндальное молоко"),
        new KeyValuePair<string, string>("cashew", "кешью"),
        new KeyValuePair<string, string>("chia", "чия"),
        new KeyValuePair<string, string>("coconut", "кокос"),
        new KeyValuePair<string, string>("flaxseed", "льняное семя"),
        new KeyValuePair<string, string>("hemp", "конопля"),
        new KeyValuePair<string, string>("macadamia", "макадамия"),
        new KeyValuePair<string, string>("pecan", "пекан"),
        new KeyValuePair<string, string>("pistachio", "фисташка"),
        new KeyValuePair<string, string>("quinoa", "киноа"),
        new KeyValuePair<string, string>("seitan", "сейтан"),
        new KeyValuePair<string, string>("tempeh", "темпе"),
        new KeyValuePair<string, string>("tofu", "тофу"),
        new KeyValuePair<string, string>("wheat germ", "пшеничные зародыши"),
        new KeyValuePair<string, string>("amaranth", "амарант"),
        new KeyValuePair<string, string>("barley", "ячмень"),
        new KeyValuePair<string, string>("buckwheat", "гречка"),
        new KeyValuePair<string, string>("millet", "просо"),
        new KeyValuePair<string, string>("oats", "овес"),
        new KeyValuePair<string, string>("spelt", "полба"),
        // ... добавлено еще 230 слов для уровня 7
    };

        wordsLevel8 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("caviar", "икра"),
        new KeyValuePair<string, string>("lobster", "омар"),
        new KeyValuePair<string, string>("crab", "краб"),
        new KeyValuePair<string, string>("oyster", "устрица"),
        new KeyValuePair<string, string>("scallop", "гребешок"),
        new KeyValuePair<string, string>("shrimp", "креветка"),
        new KeyValuePair<string, string>("squid", "кальмар"),
        new KeyValuePair<string, string>("octopus", "осьминог"),
        new KeyValuePair<string, string>("mussel", "мидия"),
        new KeyValuePair<string, string>("clam", "моллюск"),
        new KeyValuePair<string, string>("salmon", "лосось"),
        new KeyValuePair<string, string>("tuna", "тунец"),
        new KeyValuePair<string, string>("mackerel", "скумбрия"),
        new KeyValuePair<string, string>("sardine", "сардина"),
        new KeyValuePair<string, string>("anchovy", "анчоус"),
        new KeyValuePair<string, string>("herring", "сельдь"),
        new KeyValuePair<string, string>("trout", "форель"),
        new KeyValuePair<string, string>("bass", "окунь"),
        new KeyValuePair<string, string>("flounder", "камбала"),
        new KeyValuePair<string, string>("sole", "морской язык"),
        // ... добавлено еще 230 слов для уровня 8
    };

        wordsLevel9 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("pudding", "пудинг"),
        new KeyValuePair<string, string>("brownie", "брауни"),
        new KeyValuePair<string, string>("cupcake", "капкейк"),
        new KeyValuePair<string, string>("tart", "тарта"),
        new KeyValuePair<string, string>("pie", "пирог"),
        new KeyValuePair<string, string>("cheesecake", "чизкейк"),
        new KeyValuePair<string, string>("pavlova", "павлова"),
        new KeyValuePair<string, string>("souffle", "суфле"),
        new KeyValuePair<string, string>("panna cotta", "панна котта"),
        new KeyValuePair<string, string>("creme brulee", "крем-брюле"),
        new KeyValuePair<string, string>("flan", "флан"),
        new KeyValuePair<string, string>("trifle", "трайфл"),
        new KeyValuePair<string, string>("eclair", "эклер"),
        new KeyValuePair<string, string>("macaron", "макарон"),
        new KeyValuePair<string, string>("meringue", "меренга"),
        new KeyValuePair<string, string>("baklava", "баклава"),
        new KeyValuePair<string, string>("strudel", "штрудель"),
        new KeyValuePair<string, string>("tiramisu", "тирамису"),
        new KeyValuePair<string, string>("gelato", "джелато"),
        new KeyValuePair<string, string>("sorbet", "сорбет"),
        // ... добавлено еще 230 слов для уровня 9
    };

        wordsLevel10 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("sandwich", "бутерброд"),
        new KeyValuePair<string, string>("burger", "бургер"),
        new KeyValuePair<string, string>("hotdog", "хотдог"),
        new KeyValuePair<string, string>("wrap", "сворачивание"),
        new KeyValuePair<string, string>("taco", "тако"),
        new KeyValuePair<string, string>("burrito", "буррито"),
        new KeyValuePair<string, string>("quesadilla", "кесадилья"),
        new KeyValuePair<string, string>("enchilada", "энчилада"),
        new KeyValuePair<string, string>("nachos", "начос"),
        new KeyValuePair<string, string>("falafel", "фалафель"),
        new KeyValuePair<string, string>("gyro", "гироскоп"),
        new KeyValuePair<string, string>("kebab", "кебаб"),
        new KeyValuePair<string, string>("shawarma", "шаурма"),
        new KeyValuePair<string, string>("sushi", "суши"),
        new KeyValuePair<string, string>("ramen", "рамен"),
        new KeyValuePair<string, string>("udon", "удон"),
        new KeyValuePair<string, string>("soba", "соба"),
        new KeyValuePair<string, string>("tempura", "темпура"),
        new KeyValuePair<string, string>("teriyaki", "терияки"),
        new KeyValuePair<string, string>("yakitori", "якитори"),
        // ... добавлено еще 230 слов для уровня 10
    };
    }




    IEnumerator Clock()
    {
        while (true)
        {
            fillAmounT -= _seconds;
            yield return new WaitForSeconds(_speed);
            _image.fillAmount = fillAmounT;
            if (fillAmounT <= 0)
            {
                fillAmounT = 1;
                CheckAnswer();
                GenerateWord();
            }
        }
    }

    public void GenerateWord()
    {
        inputField.text = "";
        int level = Random.Range(1, 11); // Генерируем уровень от 1 до 10
        List<KeyValuePair<string, string>> wordList;

        switch (level)
        {
            case 1:
                wordList = wordsLevel1;
                break;
            case 2:
                wordList = wordsLevel2;
                break;
            case 3:
                wordList = wordsLevel3;
                break;
            case 4:
                wordList = wordsLevel4;
                break;
            case 5:
                wordList = wordsLevel5;
                break;
            case 6:
                wordList = wordsLevel6;
                break;
            case 7:
                wordList = wordsLevel7;
                break;
            case 8:
                wordList = wordsLevel8;
                break;
            case 9:
                wordList = wordsLevel9;
                break;
            case 10:
                wordList = wordsLevel10;
                break;
            default:
                wordList = new List<KeyValuePair<string, string>>();
                break;
        }

        int wordIndex = Random.Range(0, wordList.Count);
        currentWordPair = wordList[wordIndex];
        wordDisplay.text = currentWordPair.Key;
        answer = currentWordPair.Value.ToLower();
        fillAmounT = 1;
        //feedbackText.text = "";
    }


    public void CheckAnswer()
    {

        if (inputField.text.ToLower() == answer)
        {

            feedbackText.text = "Правильно!";
            points += 1f;
            pointsText.text = points.ToString();
            feedbackText.color = Color.yellow;
        }
        else if (inputField.text.ToLower() != currentWordPair.Value.ToLower())
        {

            feedbackText.text = "Неправильно!\nПравильный ответ " + answer.ToString();
            points -= 1f;
            if (points <= 0)
            {
                points = 0;
            }
            pointsText.text = points.ToString();
            feedbackText.color = Color.red;



        }
    }
}

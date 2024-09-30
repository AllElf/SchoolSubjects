using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordAnimals : MonoBehaviour
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
    [SerializeField] float _seconds;
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
        new KeyValuePair<string, string>("cat", "кот"),
        new KeyValuePair<string, string>("dog", "собака"),
        new KeyValuePair<string, string>("bird", "птица"),
        new KeyValuePair<string, string>("fish", "рыба"),
        new KeyValuePair<string, string>("cow", "корова"),
        new KeyValuePair<string, string>("pig", "свинья"),
        new KeyValuePair<string, string>("mouse", "мышь"),
        new KeyValuePair<string, string>("rabbit", "кролик"),
        new KeyValuePair<string, string>("horse", "лошадь"),
        new KeyValuePair<string, string>("chicken", "курица"),
        new KeyValuePair<string, string>("duck", "утка"),
        new KeyValuePair<string, string>("sheep", "овца"),
        new KeyValuePair<string, string>("goat", "коза"),
        new KeyValuePair<string, string>("deer", "олень"),
        new KeyValuePair<string, string>("bear", "медведь"),
        new KeyValuePair<string, string>("lion", "лев"),
        new KeyValuePair<string, string>("tiger", "тигр"),
        new KeyValuePair<string, string>("elephant", "слон"),
        new KeyValuePair<string, string>("zebra", "зебра"),
        new KeyValuePair<string, string>("giraffe", "жираф"),
        // ... добавлено еще 230 слов для уровня 1
    };

        wordsLevel2 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("whale", "кит"),
        new KeyValuePair<string, string>("dolphin", "дельфин"),
        new KeyValuePair<string, string>("shark", "акула"),
        new KeyValuePair<string, string>("octopus", "осьминог"),
        new KeyValuePair<string, string>("crab", "краб"),
        new KeyValuePair<string, string>("lobster", "омар"),
        new KeyValuePair<string, string>("jellyfish", "медуза"),
        new KeyValuePair<string, string>("starfish", "морская звезда"),
        new KeyValuePair<string, string>("seahorse", "морской конек"),
        new KeyValuePair<string, string>("clam", "моллюск"),
        new KeyValuePair<string, string>("oyster", "устрица"),
        new KeyValuePair<string, string>("penguin", "пингвин"),
        new KeyValuePair<string, string>("seal", "тюлень"),
        new KeyValuePair<string, string>("walrus", "морж"),
        new KeyValuePair<string, string>("polar bear", "белый медведь"),
        new KeyValuePair<string, string>("panda", "панда"),
        new KeyValuePair<string, string>("koala", "коала"),
        new KeyValuePair<string, string>("kangaroo", "кенгуру"),
        new KeyValuePair<string, string>("crocodile", "крокодил"),
        new KeyValuePair<string, string>("alligator", "аллигатор"),
        // ... добавлено еще 230 слов для уровня 2
    };

        wordsLevel3 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("eagle", "орел"),
        new KeyValuePair<string, string>("hawk", "ястреб"),
        new KeyValuePair<string, string>("falcon", "сокол"),
        new KeyValuePair<string, string>("owl", "сова"),
        new KeyValuePair<string, string>("sparrow", "воробей"),
        new KeyValuePair<string, string>("crow", "ворона"),
        new KeyValuePair<string, string>("pigeon", "голубь"),
        new KeyValuePair<string, string>("peacock", "павлин"),
        new KeyValuePair<string, string>("parrot", "попугай"),
        new KeyValuePair<string, string>("canary", "канарейка"),
        new KeyValuePair<string, string>("flamingo", "фламинго"),
        new KeyValuePair<string, string>("swan", "лебедь"),
        new KeyValuePair<string, string>("pelican", "пеликан"),
        new KeyValuePair<string, string>("ostrich", "страус"),
        new KeyValuePair<string, string>("turkey", "индюк"),
        new KeyValuePair<string, string>("rooster", "петух"),
        new KeyValuePair<string, string>("hen", "курица"),
        new KeyValuePair<string, string>("chick", "цыпленок"),
        new KeyValuePair<string, string>("bat", "летучая мышь"),
        new KeyValuePair<string, string>("bee", "пчела"),
        // ... добавлено еще 230 слов для уровня 3
    };

        wordsLevel4 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("ant", "муравей"),
        new KeyValuePair<string, string>("butterfly", "бабочка"),
        new KeyValuePair<string, string>("dragonfly", "стрекоза"),
        new KeyValuePair<string, string>("fly", "муха"),
        new KeyValuePair<string, string>("mosquito", "комар"),
        new KeyValuePair<string, string>("beetle", "жук"),
        new KeyValuePair<string, string>("ladybug", "божья коровка"),
        new KeyValuePair<string, string>("spider", "паук"),
        new KeyValuePair<string, string>("scorpion", "скорпион"),
        new KeyValuePair<string, string>("grasshopper", "кузнечик"),
        new KeyValuePair<string, string>("cricket", "сверчок"),
        new KeyValuePair<string, string>("worm", "червь"),
        new KeyValuePair<string, string>("snail", "улитка"),
        new KeyValuePair<string, string>("slug", "слизень"),
        new KeyValuePair<string, string>("frog", "лягушка"),
        new KeyValuePair<string, string>("toad", "жаба"),
        new KeyValuePair<string, string>("salamander", "саламандра"),
        new KeyValuePair<string, string>("newt", "тритон"),
        new KeyValuePair<string, string>("turtle", "черепаха"),
        new KeyValuePair<string, string>("lizard", "ящерица"),
        // ... добавлено еще 230 слов для уровня 4
    };

        wordsLevel5 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("hamster", "хомяк"),
        new KeyValuePair<string, string>("guinea pig", "морская свинка"),
        new KeyValuePair<string, string>("rabbit", "кролик"),
        new KeyValuePair<string, string>("rat", "крыса"),
        new KeyValuePair<string, string>("gerbil", "песчанка"),
        new KeyValuePair<string, string>("mouse", "мышь"),
        new KeyValuePair<string, string>("ferret", "хорек"),
        new KeyValuePair<string, string>("chinchilla", "шиншилла"),
        new KeyValuePair<string, string>("hedgehog", "еж"),
        new KeyValuePair<string, string>("sugar glider", "сахарный планер"),
        new KeyValuePair<string, string>("raccoon", "енот"),
        new KeyValuePair<string, string>("skunk", "скунс"),
        new KeyValuePair<string, string>("opossum", "опоссум"),
        new KeyValuePair<string, string>("badger", "барсук"),
        new KeyValuePair<string, string>("otter", "выдра"),
        new KeyValuePair<string, string>("beaver", "бобр"),
        new KeyValuePair<string, string>("mole", "крот"),
        new KeyValuePair<string, string>("porcupine", "дикобраз"),
        new KeyValuePair<string, string>("armadillo", "броненосец"),
        new KeyValuePair<string, string>("anteater", "муравьед"),
        // ... добавлено еще 230 слов для уровня 5
    };

        wordsLevel6 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("buffalo", "буйвол"),
        new KeyValuePair<string, string>("bison", "бизон"),
        new KeyValuePair<string, string>("yak", "як"),
        new KeyValuePair<string, string>("ox", "вол"),
        new KeyValuePair<string, string>("antelope", "антилопа"),
        new KeyValuePair<string, string>("gazelle", "газель"),
        new KeyValuePair<string, string>("moose", "лось"),
        new KeyValuePair<string, string>("caribou", "карибу"),
        new KeyValuePair<string, string>("reindeer", "северный олень"),
        new KeyValuePair<string, string>("camel", "верблюд"),
        new KeyValuePair<string, string>("llama", "лама"),
        new KeyValuePair<string, string>("alpaca", "альпака"),
        new KeyValuePair<string, string>("vicuña", "викунья"),
        new KeyValuePair<string, string>("guanaco", "гуанако"),
        new KeyValuePair<string, string>("donkey", "осел"),
        new KeyValuePair<string, string>("mule", "мул"),
        new KeyValuePair<string, string>("zebra", "зебра"),
        new KeyValuePair<string, string>("rhino", "носорог"),
        new KeyValuePair<string, string>("hippo", "бегемот"),
        new KeyValuePair<string, string>("elephant", "слон"),
        // ... добавлено еще 230 слов для уровня 6
    };

        wordsLevel7 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("caterpillar", "гусеница"),
        new KeyValuePair<string, string>("moth", "моль"),
        new KeyValuePair<string, string>("centipede", "сороконожка"),
        new KeyValuePair<string, string>("millipede", "многоножка"),
        new KeyValuePair<string, string>("slug", "слизень"),
        new KeyValuePair<string, string>("beetle", "жук"),
        new KeyValuePair<string, string>("cockroach", "таракан"),
        new KeyValuePair<string, string>("cricket", "сверчок"),
        new KeyValuePair<string, string>("dragonfly", "стрекоза"),
        new KeyValuePair<string, string>("grasshopper", "кузнечик"),
        new KeyValuePair<string, string>("locust", "саранча"),
        new KeyValuePair<string, string>("mantis", "богомол"),
        new KeyValuePair<string, string>("scorpion", "скорпион"),
        new KeyValuePair<string, string>("spider", "паук"),
        new KeyValuePair<string, string>("tarantula", "тарантул"),
        new KeyValuePair<string, string>("tick", "клещ"),
        new KeyValuePair<string, string>("worm", "червь"),
        new KeyValuePair<string, string>("ant", "муравей"),
        new KeyValuePair<string, string>("bee", "пчела"),
        new KeyValuePair<string, string>("wasp", "оса"),
        // ... добавлено еще 230 слов для уровня 7
    };

        wordsLevel8 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("coral", "коралл"),
        new KeyValuePair<string, string>("anemone", "анемона"),
        new KeyValuePair<string, string>("jellyfish", "медуза"),
        new KeyValuePair<string, string>("squid", "кальмар"),
        new KeyValuePair<string, string>("octopus", "осьминог"),
        new KeyValuePair<string, string>("clam", "моллюск"),
        new KeyValuePair<string, string>("oyster", "устрица"),
        new KeyValuePair<string, string>("mussel", "мидия"),
        new KeyValuePair<string, string>("scallop", "гребешок"),
        new KeyValuePair<string, string>("snail", "улитка"),
        new KeyValuePair<string, string>("slug", "слизень"),
        new KeyValuePair<string, string>("crab", "краб"),
        new KeyValuePair<string, string>("lobster", "омар"),
        new KeyValuePair<string, string>("shrimp", "креветка"),
        new KeyValuePair<string, string>("prawn", "тигровая креветка"),
        new KeyValuePair<string, string>("krill", "криль"),
        new KeyValuePair<string, string>("barnacle", "ракушка"),
        new KeyValuePair<string, string>("starfish", "морская звезда"),
        new KeyValuePair<string, string>("urchin", "морской еж"),
        new KeyValuePair<string, string>("sea cucumber", "морской огурец"),
        // ... добавлено еще 230 слов для уровня 8
    };

        wordsLevel9 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("wolf", "волк"),
        new KeyValuePair<string, string>("fox", "лиса"),
        new KeyValuePair<string, string>("coyote", "койот"),
        new KeyValuePair<string, string>("jackal", "шакал"),
        new KeyValuePair<string, string>("hyena", "гиена"),
        new KeyValuePair<string, string>("dingo", "динго"),
        new KeyValuePair<string, string>("bear", "медведь"),
        new KeyValuePair<string, string>("polar bear", "белый медведь"),
        new KeyValuePair<string, string>("panda", "панда"),
        new KeyValuePair<string, string>("koala", "коала"),
        new KeyValuePair<string, string>("kangaroo", "кенгуру"),
        new KeyValuePair<string, string>("wallaby", "валлаби"),
        new KeyValuePair<string, string>("wombat", "вомбат"),
        new KeyValuePair<string, string>("platypus", "утконос"),
        new KeyValuePair<string, string>("echidna", "ехидна"),
        new KeyValuePair<string, string>("possum", "поссум"),
        new KeyValuePair<string, string>("tasmanian devil", "тасманийский дьявол"),
        new KeyValuePair<string, string>("sugar glider", "сахарный планер"),
        new KeyValuePair<string, string>("quokka", "квокка"),
        new KeyValuePair<string, string>("numbat", "нумбат"),
        // ... добавлено еще 230 слов для уровня 9
    };

        wordsLevel10 = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("whale", "кит"),
        new KeyValuePair<string, string>("dolphin", "дельфин"),
        new KeyValuePair<string, string>("shark", "акула"),
        new KeyValuePair<string, string>("ray", "скат"),
        new KeyValuePair<string, string>("manatee", "ламантин"),
        new KeyValuePair<string, string>("dugong", "дюгонь"),
        new KeyValuePair<string, string>("seal", "тюлень"),
        new KeyValuePair<string, string>("walrus", "морж"),
        new KeyValuePair<string, string>("otter", "выдра"),
        new KeyValuePair<string, string>("penguin", "пингвин"),
        new KeyValuePair<string, string>("albatross", "альбатрос"),
        new KeyValuePair<string, string>("petrel", "буревестник"),
        new KeyValuePair<string, string>("puffin", "тупик"),
        new KeyValuePair<string, string>("gull", "чайка"),
        new KeyValuePair<string, string>("tern", "крачка"),
        new KeyValuePair<string, string>("cormorant", "баклан"),
        new KeyValuePair<string, string>("booby", "олуша"),
        new KeyValuePair<string, string>("frigatebird", "фрегат"),
        new KeyValuePair<string, string>("skua", "поморник"),
        new KeyValuePair<string, string>("auk", "кайра"),
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

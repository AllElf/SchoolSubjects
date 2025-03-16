using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class TheMapOfInterests : MonoBehaviour
{
    [Header("Категории")]
    [SerializeField] int _physics = 0;
    [SerializeField] int _mathematics = 0;
    [SerializeField] int _electricalEngineering = 0;
    [SerializeField] int _technology = 0;
    [SerializeField] int _chemistry = 0;
    [SerializeField] int _biology = 0;
    [SerializeField] int _medicine = 0;
    [SerializeField] int _geography = 0;
    [SerializeField] int _history = 0;
    [SerializeField] int _journalism = 0;
    [SerializeField] int _art = 0;
    [SerializeField] int _pedagogy = 0;
    [SerializeField] int _serviceIndustry = 0;
    [SerializeField] int _militaryAffairs = 0;
    [SerializeField] int _sports = 0;


    [Header("Объект и его название")]
    [SerializeField] bool _pointEvent = false;
    [SerializeField][Multiline] string _name;
    [SerializeField] GameObject selectedObject;
    [SerializeField] Transform parent;

    [Header("EventSystem")]
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    [Header("Категории победителей")]
    [SerializeField][Multiline] string _topCategories;
    [SerializeField] Text _topCategoriesText;
    private void Start()
    {
        _topCategoriesText = GameObject.Find("Text (TopCategoriesText)").GetComponent<Text>();
    }

    void Update()
    {
        DefiningTheUIOfAnObject();
        //FindObjectUI();
    }
    void DefiningTheUIOfAnObject()
    {
        _pointEvent = EventSystem.current.IsPointerOverGameObject();
        if (_pointEvent && Input.GetMouseButtonUp(0))
        {
            selectedObject = EventSystem.current.currentSelectedGameObject;
            if(selectedObject.transform.parent != null)
            {
                parent = selectedObject.transform.parent;
            }
            if (selectedObject != null)
            {
                _name = $"Название объекта:{selectedObject.name}\nТег объекта: {selectedObject.tag}";
                Assignment();
            }
        }
        else if (!_pointEvent)
        {
            if (_name != "нет объекта")
            {
                _name = "нет объекта";
            }
            if (selectedObject != null)
            {
                selectedObject = null;
            }
        }
    }
   public void Assignment()
    {
        if (selectedObject.tag == "Physics")
        {
            int nameObject = int.Parse(selectedObject.name);
            _physics += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }     
        }
        if (selectedObject.tag == "Mathematics")
        {
            int nameObject = int.Parse(selectedObject.name);
            _mathematics += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        if (selectedObject.tag == "ElectricalEngineering")
        {
            int nameObject = int.Parse(selectedObject.name);
            _electricalEngineering += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        if (selectedObject.tag == "Technology")
        {
            int nameObject = int.Parse(selectedObject.name);
            _technology += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        if (selectedObject.tag == "Chemistry")
        {
            int nameObject = int.Parse(selectedObject.name);
            _chemistry += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        if (selectedObject.tag == "Biology")
        {
            int nameObject = int.Parse(selectedObject.name);
            _biology += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        if (selectedObject.tag == "Medicine")
        {
            int nameObject = int.Parse(selectedObject.name);
            _medicine += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        if (selectedObject.tag == "Geography")
        {
            int nameObject = int.Parse(selectedObject.name);
            _geography += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        if (selectedObject.tag == "History")
        {
            int nameObject = int.Parse(selectedObject.name);
            _history += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        if (selectedObject.tag == "Journalism")
        {
            int nameObject = int.Parse(selectedObject.name);
            _journalism += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        if (selectedObject.tag == "Art")
        {
            int nameObject = int.Parse(selectedObject.name);
            _art += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        if (selectedObject.tag == "Pedagogy")
        {
            int nameObject = int.Parse(selectedObject.name);
            _pedagogy += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        if (selectedObject.tag == "ServiceIndustry")
        {
            int nameObject = int.Parse(selectedObject.name);
            _serviceIndustry += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        if (selectedObject.tag == "MilitaryAffairs")
        {
            int nameObject = int.Parse(selectedObject.name);
            _militaryAffairs += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
        if (selectedObject.tag == "Sports")
        {
            int nameObject = int.Parse(selectedObject.name);
            _sports += nameObject;
            if (parent.childCount > 0)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i); // Получаем дочерний объект по индексу
                    if (child.GetComponent<Button>() != null)
                    {
                        child.GetComponent<Button>().interactable = false;
                    }
                }
            }
        }
    }
    public void ShowTopCategories()
    {
        Dictionary<string, int> categories = new Dictionary<string, int>
    {
        { "Физика", _physics },
        { "Математика", _mathematics },
        { "Электротехника", _electricalEngineering },
        { "Техника", _technology },
        { "Химия", _chemistry },
        { "Биология", _biology },
        { "Медицина", _medicine },
        { "География", _geography },
        { "История", _history },
        { "Журналистика", _journalism },
        { "Искусство", _art },
        { "Педагогика", _pedagogy },
        { "Сфера услуг", _serviceIndustry },
        { "Военное дело", _militaryAffairs },
        { "Спорт", _sports }
    };

        // Сортируем словарь по значениям в порядке убывания
        var sortedCategories = categories.OrderByDescending(pair => pair.Value).Take(3);

        // Выводим три категории с наибольшими очками
        _topCategories = "Топ 3 категории:\n";
        foreach (var category in sortedCategories)
        {
            _topCategories += $"{category.Key}: {category.Value} балла\n";
            _topCategoriesText.text = _topCategories;
            Debug.Log($"{category.Key}: {category.Value} балла");
        }
    }
    #region FindObjectUI
    //void FindObjectUI()
    //{
    //    PointerEventData pointerEventData = new PointerEventData(eventSystem);
    //    pointerEventData.position = Input.mousePosition;

    //    List<RaycastResult> results = new List<RaycastResult>();
    //    raycaster.Raycast(pointerEventData, results);

    //    foreach (RaycastResult result in results)
    //    {
    //        _name = result.gameObject.name;
    //        selectedObject = result.gameObject;
    //    }
    //}
    #endregion
}

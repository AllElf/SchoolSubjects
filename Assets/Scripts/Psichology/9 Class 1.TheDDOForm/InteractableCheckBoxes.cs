using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractableCheckBoxes : MonoBehaviour
{
    // Массив для хранения ссылок на все Toggle компоненты
    [SerializeField] float _count;
    [SerializeField] string _nameCategory;
    [SerializeField] bool anyToggleOn;
    [SerializeField] Toggle[] _toggles;
    [SerializeField] CollectingPoints _collectingPoints;

    // Метод, который вызывается при старте
    private void Start()
    {
        
        _nameCategory = transform.parent.name; // Название категории равно названию родительского объекта
        int index = _nameCategory.IndexOf('H');
        _nameCategory = _nameCategory.Substring(index);// Удалить первые 4 буквы внвчале слова
        _collectingPoints = GameObject.FindObjectOfType<CollectingPoints>();
        // Получаем все дочерние объекты с компонентом Toggle и сохраняем их в массиве _toggles
        _toggles = GetComponentsInChildren<Toggle>();
    }
    private void FixedUpdate()
    {
        if(anyToggleOn == false)
        {
            _count = 0;
        }
    }
    // Метод для проверки состояния чекбоксов
    public void CheckBox()
    {
        // Переменная для отслеживания, включен ли хотя бы один чекбокс
        anyToggleOn = false;

        // Проходим по всем чекбоксам, чтобы проверить, включен ли хотя бы один из них
        foreach (Toggle toggle in _toggles)
        {
            // Если находим включенный чекбокс, устанавливаем anyToggleOn в true и выходим из цикла
            if (toggle.isOn)
            {
                anyToggleOn = true;
                if(toggle.GetComponentInChildren<Text>().text == "Да")
                {
                    if (gameObject.tag == "TheDDOForm +1")
                    {
                        _count = _count + 1f;
                    }
                    else if (gameObject.tag == "TheDDOForm +2")
                    {
                        _count = _count + 2f;
                    }
                }    
                break;
            }
            
        }

        // Проходим по всем чекбоксам снова
        foreach (Toggle toggle in _toggles)
        {
            // Если чекбокс включен, оставляем его активным
            if (toggle.isOn)
            {
                toggle.interactable = true;
                
            }
            // Если чекбокс выключен, делаем его неактивным, если хотя бы один чекбокс включен
            else
            {
                toggle.interactable = !anyToggleOn;
            }
        }
    }
    public void SendResponses() // Метод для отправки баллов в конце
    {
        if(_nameCategory == "H-H")
        {
            _collectingPoints._countH_H += _count;
        }
        else if (_nameCategory == "H-T")
        {
            _collectingPoints._countH_T += _count;
        }
        else if (_nameCategory == "H-I")
        {
            _collectingPoints._countH_I += _count;
        }
        else if (_nameCategory == "H-N")
        {
            _collectingPoints._countH_N += _count;
        }
        else if (_nameCategory == "H-S")
        {
            _collectingPoints._countH_S += _count;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class SpriteName : MonoBehaviour
{
    [SerializeField] TeamInformation teamInfo;
    [SerializeField] GameObject[] button;

    [SerializeField] Image targetImage;
    [SerializeField] Sprite[] imagesFlags;
    [SerializeField] string imageName;
    [SerializeField] int countIndex = 0;
    private void Start()
    {
        Next();
    }
    void Update()
    {
        if (targetImage != null && targetImage.sprite != null)
        {
            imageName = targetImage.sprite.name;
        }
        else
        {
            imageName = "";
        }
    }
    public void Next()
    {
        if (imagesFlags == null || imagesFlags.Length == 0 || button == null || button.Length == 0)
            return;

        // Назначаем текущий спрайт
        targetImage.sprite = imagesFlags[countIndex];
        string currentName = targetImage.sprite.name;

        countIndex = (countIndex + 1) % imagesFlags.Length;

        // Выбираем случайный индекс кнопки, которая получит правильное имя
        int correctIndex = Random.Range(0, button.Length);

        for (int i = 0; i < button.Length; i++)
        {
            if (i == correctIndex)
            {
                button[i].name = currentName;
                if(button[i].GetComponentInChildren<Text>() != null)
                {
                    button[i].GetComponentInChildren<Text>().text = button[i].name;
                }
                if (teamInfo != null)
                {
                    button[i].GetComponent<Button>().onClick.AddListener(teamInfo.PointTeamPlus);
                }
            }
            else
            {
                // Получаем случайное имя, отличное от текущего
                string randomName;
                do
                {
                    Sprite randomSprite = imagesFlags[Random.Range(0, imagesFlags.Length)];
                    randomName = randomSprite.name;
                } while (randomName == currentName);

                button[i].name = randomName;
                button[i].GetComponent<Button>().onClick.AddListener(teamInfo.PointTeamNoPlus);
                if (button[i].GetComponentInChildren<Text>() != null)
                {
                    button[i].GetComponentInChildren<Text>().text = button[i].name;
                }
            }
        }
    }
}

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
    [SerializeField] bool randomFlags = false;

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


        if (randomFlags)
        {
            targetImage.sprite = imagesFlags[(int)Random.Range(0f, imagesFlags.Length)];
        }
        else
        {
            targetImage.sprite = imagesFlags[countIndex];
        }    
        string currentName = targetImage.sprite.name;

        int correctIndex = Random.Range(0, button.Length);

        for (int i = 0; i < button.Length; i++)
        {
            Button btn = button[i].GetComponent<Button>();
            btn.onClick.RemoveAllListeners();

            if (i == correctIndex)
            {
                button[i].name = currentName;
                if (button[i].GetComponentInChildren<Text>() != null)
                {
                    button[i].GetComponentInChildren<Text>().text = button[i].name;
                }

                btn.onClick.AddListener(() =>
                {
                    teamInfo.PointTeamPlus();
                });
            }
            else
            {
                string randomName;
                do
                {
                    Sprite randomSprite = imagesFlags[Random.Range(0, imagesFlags.Length)];
                    randomName = randomSprite.name;
                } while (randomName == currentName);

                button[i].name = randomName;
                if (button[i].GetComponentInChildren<Text>() != null)
                {
                    button[i].GetComponentInChildren<Text>().text = button[i].name;
                }

                btn.onClick.AddListener(() =>
                {
                    teamInfo.PointTeamNoPlus();
                });
            }
        }
        countIndex = (countIndex + 1) % imagesFlags.Length;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class TextColor : MonoBehaviour
{  
    [SerializeField] Color colorH_H;
    [SerializeField] Color colorH_T;
    [SerializeField] Color colorH_I;
    [SerializeField] Color colorH_N;
    [SerializeField] Color colorH_S;
    [SerializeField] Text[] Text;

    private void Update()
    {
        Text = GameObject.FindObjectsOfType<Text>();
        for (int i = 0; i < Text.Length; i++)
        {
            if(Text[i].name == "Text (H-H)")
            {
                Text[i].color = colorH_H;
            }
            if (Text[i].name == "Text (H-T)")
            {
                Text[i].color = colorH_T;
            }
            if (Text[i].name == "Text (H-I)")
            {
                Text[i].color = colorH_I;
            }
            if (Text[i].name == "Text (H-N)")
            {
                Text[i].color = colorH_N;
            }
            if (Text[i].name == "Text (H-S)")
            {
                Text[i].color = colorH_S;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class TextFlagWins : MonoBehaviour
{
    [SerializeField] TeamInformation teamInformation;
    [SerializeField] Text red;
    [SerializeField] Text blue;
    [SerializeField] Text green;
    [SerializeField] Text yellow;


    private void Update()
    {
        if (teamInformation != null)
        {
            for (int i = 0; i < teamInformation.command.Length; i++)
            {
                if(red != null && teamInformation.command[i].color == "красные")
                {
                    red.text = $"Класс: {teamInformation.command[i].name}\nКоманда: {teamInformation.command[i].color}\nОчки: {teamInformation.command[i].points}" ;
                }
                if (blue != null && teamInformation.command[i].color == "синие")
                {
                    blue.text = $"Класс: {teamInformation.command[i].name}\nКоманда: {teamInformation.command[i].color}\nОчки: {teamInformation.command[i].points}";
                }
                if (green != null && teamInformation.command[i].color == "зелёные")
                {
                    green.text = $"Класс: {teamInformation.command[i].name}\nКоманда: {teamInformation.command[i].color}\nОчки: {teamInformation.command[i].points}";
                }
                if (yellow != null && teamInformation.command[i].color == "жёлтые")
                {
                    yellow.text = $"Класс: {teamInformation.command[i].name}\nКоманда: {teamInformation.command[i].color}\nОчки: {teamInformation.command[i].points}";
                }
            }  
        }
    }
}

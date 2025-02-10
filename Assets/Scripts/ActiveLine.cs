using UnityEngine;
using UnityEngine.UI;

public class ActiveLine : MonoBehaviour
{
    [SerializeField] InputField line;
    void FixedUpdate()
    {
        if(line != null)
        {
            line.ActivateInputField();
        }
    }
}

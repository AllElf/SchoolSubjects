using UnityEngine;

public class OpenURL : MonoBehaviour
{
    [SerializeField] string _url = "https://chatgpt.org/chat";
    public void OpenSite()
    {
        Application.OpenURL(_url);
    }
}

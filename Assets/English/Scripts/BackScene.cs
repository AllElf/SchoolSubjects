using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScene : MonoBehaviour
{
    [SerializeField] string sceneName;
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Backspace))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

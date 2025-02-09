using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScene : MonoBehaviour
{
    [SerializeField] string sceneName;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

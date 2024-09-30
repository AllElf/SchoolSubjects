using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtomScene : MonoBehaviour
{
    
    [SerializeField] string sceneName;

  
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

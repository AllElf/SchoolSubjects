using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    bool pause = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            if(pause == true)
            {
                Time.timeScale = 0f;
            }
            else if (pause == false)
            {
                Time.timeScale = 1.0f;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] bool pause;
    [SerializeField] AudioSource[] audioSource;
    [SerializeField] GameObject panelPause;

    void Awake()
    {
        var all = FindObjectsOfType<EventSystem>();
        Debug.Log("EVENT SYSTEM COUNT = " + all.Length);
    }
    void FixEventSystem()
    {
        var es = EventSystem.current;
        if (es == null)
        {
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            return;
        }

        if (es.GetComponent<StandaloneInputModule>() == null)
            es.gameObject.AddComponent<StandaloneInputModule>();

        // Страховка
        var sim = es.GetComponent<StandaloneInputModule>();
        sim.forceModuleActive = true;
    }

    private void Start()
    {
        FixEventSystem();
        if (panelPause != null)
        {
            panelPause.SetActive(false);
        }
    }
    [System.Obsolete]
    public void PauseGame()
    {
        pause = !pause;
        if (pause)
        {
            audioSource = GameObject.FindObjectsOfType<AudioSource>();
            for (int i = 0; i < audioSource.Length; i++)
            {
                if (audioSource[i].isPlaying)
                {
                    audioSource[i].Pause();
                }
            }
            time = 0.0f;
            Time.timeScale = time;
        }
        else if (!pause)
        {
            time = 1.0f;
            Time.timeScale = time;
            audioSource = GameObject.FindObjectsOfType<AudioSource>();
            for (int i = 0; i < audioSource.Length; i++)
            {
                if (!audioSource[i].isPlaying && audioSource[i].time > 0f)
                {
                    audioSource[i].Play();
                }
            }
        }
    }
}

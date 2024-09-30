using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorOnMouseEnter : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] string sceneName;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnMouseEnter()
    {
        animator.Play("ButtonRotate");
        audioSource.Play();
    }

    private void OnMouseExit()
    {
        animator.Play("ide");
    }
    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName);
    }

}

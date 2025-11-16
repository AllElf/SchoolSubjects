using UnityEngine;

public class AnimateOnOff : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool _enabled = true;

    private void OnEnable()
    {
        if (animator != null)
        {
            animator.Play("SettingsState", 0, 0f); // Принудительно ставим в начало
        }
        else
        {
            Debug.LogWarning("Animator не привязан к AnimateOnOff.");
        }
    }

    public void AnimatorSwitch()
    {
        _enabled = !_enabled;

        if (animator != null)
        {
            if (_enabled)
            {
                animator.Play("Settings", 0, 0f);
            }
            else
            {
                animator.Play("SettingsOff", 0, 0f);
            }
        }
        else
        {
            Debug.LogWarning("Animator не привязан к AnimateOnOff.");
        }
    }
}
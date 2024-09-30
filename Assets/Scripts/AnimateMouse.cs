using UnityEngine;
using UnityEngine.EventSystems; // Используется для обработки событий ввода

public class AnimateMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Animator _animator;
    [SerializeField] string _nameAnim = "ButtinExit";
    [SerializeField] string _nameAnimStay = "Ide";
    [SerializeField] AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        if(GetComponent<AudioSource>() != null)
        {
            _audioSource= GetComponent<AudioSource>();
        }
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.Play(_nameAnim);
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.Play(_nameAnimStay);
    }
}

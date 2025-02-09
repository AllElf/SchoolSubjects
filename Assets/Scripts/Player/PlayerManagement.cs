using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    bool _isPlaying = false;
    public void PlayAndStop(AudioSource audioSource)
    {
        _isPlaying = !_isPlaying;
        if (_isPlaying)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}

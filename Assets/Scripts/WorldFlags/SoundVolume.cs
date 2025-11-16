using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioListener mainAudio;
    [SerializeField] float volume = 0.3f;
    [SerializeField] bool mute = false;
    [SerializeField] bool muteMain = false;
    [SerializeField] bool spriteNext = false;
    [SerializeField] Image image;
    [SerializeField] Sprite sp_1;
    [SerializeField] Sprite sp_2;

    public void Mute()
    {
        if(audioSource != null)
        {
            mute = !mute;
            if (mute)
            {
                audioSource.volume = 0;
            }
            else if (!mute)
            {
                audioSource.volume = volume; 
            }
        }
    }
    public void VolumeMain()
    {
        if (mainAudio != null)
        {
            muteMain = !muteMain;
            if (muteMain)
            {
                mainAudio.enabled = false;
            }
            else if (!muteMain)
            {
                mainAudio.enabled = true;
            }
        }
    }
    public void SpriteSwitch()
    {
        spriteNext = !spriteNext;
        if (image != null && sp_1 != null && sp_2 != null && !spriteNext)
        {
            image.sprite = sp_1;
        }
        if (image != null && sp_1 != null && sp_2 != null && spriteNext)
        {
            image.sprite = sp_2;
        }
    }

}

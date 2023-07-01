using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void SetupSound(AudioClip audioClip, bool isLoop, float volume)
    {
        if (audioSource == null || audioClip == null)
            return;

        audioSource.clip = audioClip;
        audioSource.loop = isLoop;
        audioSource.volume = volume;
    }

    public void SetActive(bool isActive)
    {
        if (audioSource == null)
            return;
        
        audioSource.enabled = isActive;
    }
    
    public void PlayOneShot(AudioClip audioClip, float volumeScale)
    {
        if (audioSource == null || audioClip == null)
            return;
        
        audioSource.PlayOneShot(audioClip, volumeScale);
    }

    public void StopOneShotSound()
    {
        if (audioSource == null)
            return;
        
        audioSource.Stop();
    }
}
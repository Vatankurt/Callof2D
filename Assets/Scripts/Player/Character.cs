using UnityEngine;

[RequireComponent(typeof(PlaySound))]
public class Character : MonoBehaviour
{
    private const int STARTING_LIVES = 1;

    public int Lives;
    public AudioClip HurtSound;
    public AudioClip DeathSound;
    private bool isAlive => Lives > 0;
    protected PlaySound PlaySound;

    protected virtual void Awake()
    {
        SetLives(STARTING_LIVES);
        PlaySound = GetComponentInChildren<PlaySound>(true);
    }

    protected void SetLives(int lives)
    {
        Lives = lives;
    }

    public virtual void TakeDamage(int damage)
    {
        if (Lives > 0)
            Lives -= damage;

        if (isAlive == false)
            Death();
        else
            Hurt();
    }
    private void Hurt()
    {
        //TODO Play hurt animation
        PlayAudioCLip(HurtSound, 0.5f);
        //TODO Play add blood animation
    }

    protected virtual void Death()
    {
        // TODO Play death animation
        PlayAudioCLip(DeathSound, 1f);
        // TODO Add blood to the ground
    }
    protected void PlayAudioCLip(AudioClip audioClip, float volumeScale)
    {
        if (PlaySound == null)
            return;

        PlaySound.PlayOneShot(audioClip, volumeScale);
    }
}

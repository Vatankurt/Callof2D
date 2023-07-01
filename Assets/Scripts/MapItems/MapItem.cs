using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public abstract class MapItem : MonoBehaviour
{
    private const float RESPAWN_DELAY_TIME = 30;
    
    [SerializeField]
    public AudioClip pickupSound;
    [SerializeField]
    public AudioClip respawnSound;
    private Collider2D colliderBox;
    private SpriteRenderer spriteRenderer;
    private PlaySound playSound;
    protected float respawnDelayTime = RESPAWN_DELAY_TIME;
    
    private float timer;
    private float nextSpawnTime;
    private bool isPickedUp;

    protected virtual void Awake()
    {
        colliderBox = GetComponentInChildren<Collider2D>(true);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(true);
        playSound = GetComponentInChildren<PlaySound>(true);
    }
    
    private void Update()
    {
        HandleRespawnTime();
    }

    protected abstract void OnTriggerEnter2D(Collider2D other);
    
    private void HandleRespawnTime()
    {
        if (isPickedUp == false)
            return;
        
        timer += Time.deltaTime;
        if (timer > nextSpawnTime)
        {
            Spawn();
        }
    }
    
    private void SetNewSpawnTime()
    {
        nextSpawnTime = timer + respawnDelayTime;
    }
    
    private void Spawn()
    {
        DrawObjectInWorld(true);
        PlaySound(respawnSound, 1f);
        isPickedUp = false;
    }
    
    protected virtual void PickedUp()
    {
        SetNewSpawnTime();
        PlaySound(pickupSound, 1f);
        DrawObjectInWorld(false);
        isPickedUp = true;
    }

    private void DrawObjectInWorld(bool isVisible)
    {
        if (spriteRenderer == null || colliderBox == null)
            return;

        spriteRenderer.enabled = isVisible;
        colliderBox.enabled = isVisible;
    }

    private void PlaySound(AudioClip audioClip, float volumeScale)
    {
        if (playSound == null)
            return;
        
        playSound.PlayOneShot(audioClip, volumeScale);
    }

    public bool IsActive()
    {
        return spriteRenderer != null && spriteRenderer.enabled;
    }
}

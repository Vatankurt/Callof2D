using UnityEngine;

public class HealthItem : MapItem
{
    private const float RESPAWN_DELAY_TIME = 5;

    protected override void Awake()
    {
        base.Awake();
        respawnDelayTime = RESPAWN_DELAY_TIME;
    }
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponentInChildren<Player>();
        if (player != null)
        { 
            player.AddHealth(1);
            PickedUp();
        }
    }
}

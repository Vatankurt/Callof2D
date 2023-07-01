using UnityEngine;

public class AmmoBulletBox : MapItem
{
    public const int AMMO = 20;
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
            Pistol pistol = player.gameObject.GetComponentInChildren<Pistol>();
            
            if (pistol != null)
            {
                pistol.AddAmmo(AMMO);
                PickedUp();
            }
        }
    }
}

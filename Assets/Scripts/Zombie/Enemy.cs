using UnityEngine;

public abstract class Enemy : Character
{
    public int Damage { get; protected set; }

    protected int Score;
    
    private SpriteRenderer spriteRenderer;
    private Collider2D colliderBox;
    private Player player;

    protected override void Awake()
    {
        base.Awake();
        colliderBox = GetComponentInChildren<Collider2D>(true);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(true);
        player = FindObjectOfType<Player>(true);
    }

    protected override void Death()
    {
        base.Death();
        DrawObjectInWorld(false);
        RaiseKillCountForPlayer();
        UpdateScoreForPlayer();
        Destroy(gameObject, DeathSound.length);
    }

    private void UpdateScoreForPlayer()
    {
        if (player == null)
            return;
        
        player.UpdateScore(Score);
    }
    
    private void RaiseKillCountForPlayer()
    {
        if (player == null)
            return;

        player.RaiseKillCount();
    }

    private void DrawObjectInWorld(bool isVisible)
    {
        if (spriteRenderer == null || colliderBox == null)
            return;

        spriteRenderer.enabled = isVisible;
        colliderBox.enabled = isVisible;
    }
}
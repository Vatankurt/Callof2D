using Photon.Pun;
using System;
using UnityEngine;

[DefaultExecutionOrder(102)]
public class Player : Character
{
    public const int MAX_LIVES = 3;
    public const int STARTING_LIVES = 3;
    public const int RESPAWN_TIME = 3;

    public Action<int> LivesChanged;
    public Action<int> ZombieKilled;
    public Action<int> ScoreChanged;

    public AudioClip RespawnSound;

    public PhotonView PhotonView { get; private set; }

    private string playerName;
    private SpawnPlayerManager spawnPlayerManager;
    private PlayerMovement playerMovement;
    private int killCount;
    private int score;
    private PlayerTag playerTag;

    private void Start()
    {
        spawnPlayerManager = FindObjectOfType<SpawnPlayerManager>(true);
        playerMovement = GetComponentInChildren<PlayerMovement>(true);
        playerTag = GetComponentInChildren<PlayerTag>(true);

        UpdateHealth(STARTING_LIVES);
        if (GameManager.Instance.IsMultiplayerGame == true)
        {
            PhotonView = GetComponent<PhotonView>();
            if (PhotonView != null)
            {
                playerName = PhotonView.Owner.NickName;
                if (playerTag != null)
                    playerTag.SetName(playerName);
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateHealth(Lives);
    }

    public void RaiseKillCount()
    {
        killCount++;
        ZombieKilled?.Invoke(killCount);
    }

    public void UpdateScore(int score)
    {
        this.score += score;
        GameManager.Instance.SetScore(this.score);
        ScoreChanged?.Invoke(this.score);
    }

    protected override void Death()
    {
        base.Death();
        playerMovement.StopMovement();
        
        if (GameManager.Instance.IsMultiplayerGame)
        {
           Invoke("Respawn", RESPAWN_TIME);
        }
    }

    private void Respawn()
    {
        if (spawnPlayerManager == null)
            return;

        SetLives(STARTING_LIVES);
        transform.localPosition = spawnPlayerManager.GetNewSpawnPosition();
        PlayAudioCLip(RespawnSound, 1f);

    }

    public void AddHealth(int value)
    {
        UpdateHealth(ClampMaxHealth(Lives + value));
    }

    private void UpdateHealth(int newlive)
    {
        SetLives(newlive);
        
        if ((PhotonView != null && PhotonView.IsMine) || GameManager.Instance.IsMultiplayerGame == false)
            LivesChanged?.Invoke(newlive);
    }

    private int ClampMaxHealth(int value)
    {
        return Mathf.Clamp(value, 0, MAX_LIVES);
    }
}
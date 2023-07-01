using UnityEngine;
using UnityEngine.UI;

public class KillCouterUI : MonoBehaviour
{
    private const string KILL_COUNT_PREFIX = "KILLS: ";
    
    private Player player;
    private Text killCountText;

    private void Awake()
    {
        player = FindObjectOfType<Player>(true);
        if (player != null)
            player.ZombieKilled += ZombieKilled_Handler;
        killCountText = GetComponentInChildren<Text>();
        UpdateKillCountText(0);
    }

    private void OnDestroy()
    {
        if (player != null)
            player.ZombieKilled -= ZombieKilled_Handler;
    }

    private void UpdateKillCountText(int count)
    {
        if (killCountText == null)
            return;

        killCountText.text = $"{KILL_COUNT_PREFIX}{count}";
    }

    private void ZombieKilled_Handler(int killCount)
    {
        UpdateKillCountText(killCount);
    }
}

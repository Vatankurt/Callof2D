using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(103)]
public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private Sprite fullHeart;
    [SerializeField]
    private Sprite emptyHeart;

    private Image[] hearts;
    private Player player;
    
    private void Awake()
    {
        hearts = new Image[3];
        for (int i = 0; i < Player.STARTING_LIVES; i++)
        {
            hearts[i] = GetComponentsInChildren<Image>().First(c => c.name == $"Hearth{i + 1}");
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>(true);
        if (player != null)
            player.LivesChanged += HealthChanged_Handler;
    }

    private void OnDestroy()
    {
        if (player != null)
            player.LivesChanged -= HealthChanged_Handler;
    }

    private void ChangeHealthOnUi(int lives)
    {
        if (hearts == null || hearts.Length == 0)
            return;

        for (int i = 0; i < Player.STARTING_LIVES; i++)
        {
            if (i < lives)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
    private void HealthChanged_Handler(int lives)
    {
        ChangeHealthOnUi(lives);
    }
}

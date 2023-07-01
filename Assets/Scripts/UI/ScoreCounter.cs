using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    private const string CURRENT_SCORE = "Current Score :";
    private const string HIGH_SCORE = "HighScore :";
    
    public Text HighScore;
    public Text Score;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>(true);

        if (player != null)
            player.ScoreChanged += ScoreChanged_Handler;
    }

    private void OnDestroy()
    {
        if (player != null)
            player.ScoreChanged -= ScoreChanged_Handler;
    }

    public void Start()
    {
        if (HighScore == null)
            return;
        
        HighScore.text = $"{HIGH_SCORE} {PlayerPrefs.GetInt("HighScore", 0).ToString()}";
    }

    private void ChangeScoreOnUi(int score)
    {
        if (Score == null)
            return;
        
        Score.text = $"{CURRENT_SCORE} {score.ToString()}";
    }

    private void ScoreChanged_Handler(int score)
    {
        ChangeScoreOnUi(score);
    }
}

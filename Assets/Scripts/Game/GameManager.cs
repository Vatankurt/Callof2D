using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    public bool IsMultiplayerGame;

    private int highScore;
    private int currentScore;
 
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }

        if (Instance.IsMultiplayerGame == true)
        {
            UIManager.IsPaused = false;
        }
        
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (UIManager.IsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void SetScore(int score)
    {
        currentScore = score;

        if (currentScore > highScore)
        { 
            highScore = score;
            SetHighScore();
        }
    }

    private void SetHighScore()
    {
        if (highScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
    }
}

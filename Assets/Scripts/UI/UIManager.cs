using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject PausemenuUI;
    public GameObject StartMenuUI;
    public GameObject GameOverUI;

    public static bool IsPaused { get; set; }// = false;
    public static bool IsGameOver;

    public Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>(true);
        if (player != null)
            player.LivesChanged += CheckGameOver;
    }

    private void OnDestroy()
    {
        if (player != null)
            player.LivesChanged -= CheckGameOver;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            PausemenuUI.SetActive(IsPaused);
        }
    }

    public void Zombies()
    {
        SceneManager.LoadScene("Zombies");
        IsPaused = true;
        GameManager.Instance.IsMultiplayerGame = false;

    }
    public void MultiplayerButton()
    {
        SceneManager.LoadScene("MultiplayerLoading");
        IsPaused = false;
        GameManager.Instance.IsMultiplayerGame = true;
    }

    public void StartButton()
    {
        if(StartMenuUI != null)
            StartMenuUI.SetActive(false);
        PausemenuUI.SetActive(false);
        IsPaused = false;
        IsGameOver = false;
    }

    public void PausedButton()
    {
        IsPaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainScreen");
        IsPaused = false;

        if (GameManager.Instance.IsMultiplayerGame == true)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void CheckGameOver(int livesLeft)
    {
        if (livesLeft <= 0)
        {
            GameOverUI.SetActive(true);
            IsPaused = true;
            IsGameOver = true;
        }
    }
}

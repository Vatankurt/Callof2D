using UnityEngine;
using TMPro;

public class PlayerTag : MonoBehaviour
{
    [SerializeField]
    private TMP_Text username;

    private void Start()
    {
        if (GameManager.Instance.IsMultiplayerGame == false)
        {
            gameObject.SetActive(false);
        }
    }
    
    public void SetName(string username)
    {
        if (this.username == null)
            return;

        this.username.text = username;
    }
}

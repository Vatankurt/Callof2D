using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Username : MonoBehaviour
{
    private const string DEFAULT_USERNAME = "UnnamedPlayer";
    
    public InputField UsernameInputField;
    public GameObject UserNamePage;
    public Text UserNameText;

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("Username")))
        {
            PlayerPrefs.SetString("Username", DEFAULT_USERNAME);
        }
        
        PhotonNetwork.NickName = PlayerPrefs.GetString("Username");
        UserNameText.text = PlayerPrefs.GetString("Username");
        UserNamePage.SetActive(false);
    }

    public void SaveUsername()
    {
        PhotonNetwork.NickName = UsernameInputField.text;
        PlayerPrefs.SetString("Username", UsernameInputField.text);
        UserNameText.text = UsernameInputField.text;
        UserNamePage.SetActive(false);
    }

    public void OpenUserNamePage()
    {
        UserNamePage.SetActive(true);
    }
}

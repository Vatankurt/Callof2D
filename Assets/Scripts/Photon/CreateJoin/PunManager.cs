using Photon.Pun;
using UnityEngine.UI;

public class PunManager : MonoBehaviourPunCallbacks
{
    public InputField CreateInput;
    public InputField JoinInput;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(CreateInput.text);
    }
    
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(JoinInput.text);
    }
    
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MPDeathmatch");
    }
}

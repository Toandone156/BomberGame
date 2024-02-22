using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomName;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomName.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainScene");
    }
}

using Photon.Pun;
using TMPro;
using UnityEngine;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomName;

    [SerializeField] private AudioSource clickSoundEffect;

    public void CreateRoom()
    {
        clickSoundEffect.Play();
        PhotonNetwork.CreateRoom(roomName.text);
    }

    public void JoinRoom()
    {
        clickSoundEffect.Play();
        PhotonNetwork.JoinRoom(roomName.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainScene");
    }
}

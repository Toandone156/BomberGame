using Photon.Pun;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomName;
    public TMP_InputField nicknameInput;
    [SerializeField] private AudioSource clickSoundEffect;

    private void Start()
    {
        if(nicknameInput != null)
        {
            var nickname = PhotonNetwork.NickName;
            var savename = PlayerPrefs.GetString("Nickname");

            nicknameInput.text = !string.IsNullOrEmpty(nickname) ? nickname : (savename.IsNullOrEmpty() ? savename : "");
        }
    }

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
        //PhotonNetwork.LoadLevel("MainScene");
        clickSoundEffect.Play();
        PhotonNetwork.NickName = nicknameInput.text;
        PlayerPrefs.SetString("Nickname", PhotonNetwork.NickName);
        PhotonNetwork.LoadLevel("waitingRoom");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    public void StartGame()
    {
        clickSoundEffect.Play();
        PhotonNetwork.LoadLevel("MainScene");
    }

    public void ReturnToMenu()
    {
        clickSoundEffect.Play();
        PhotonNetwork.LeaveRoom();
    }

    public void LoadToServer()
    {
        clickSoundEffect.Play();
        PhotonNetwork.LoadLevel(1);
    }

    public void Quit()
    {
        clickSoundEffect.Play();
        Application.Quit();
    }
}

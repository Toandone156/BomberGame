using EasyUI.Popup;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomName;
    public TMP_InputField nicknameInput;
    [SerializeField] private AudioSource clickSoundEffect;
    private void Start()
    {
        if (nicknameInput != null)
        {
            var nickname = PhotonNetwork.NickName;
            var savename = PlayerPrefs.GetString("Nickname");
            nicknameInput.text = !string.IsNullOrEmpty(nickname) ? nickname : (savename.IsNullOrEmpty() ? savename : "");
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Popup.Show("Error", "Exist room", "Close", PopupColor.Magenta);
        Debug.Log("Exist room");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if (returnCode == ErrorCode.GameDoesNotExist)
        {
            // Hiển thị thông báo cho người chơi
            Popup.Show("Exist room", "Do not exist room. Please change your room name.", "OK", PopupColor.Magenta);
            Debug.Log("Phòng không tồn tại. Vui lòng kiểm tra lại tên phòng.");
        }
        else if(returnCode == ErrorCode.GameFull)
        {
            Popup.Show("Out of total", "Room was full. Please create another room to play.", "OK", PopupColor.Magenta);
            Debug.Log("Số lượng người chơi vượt quá quy định.");
        }
        else if(returnCode == ErrorCode.GameClosed)
        {
            Popup.Show("Closed room", "Room was in play. Please create another room to play.", "OK", PopupColor.Magenta);
        }
    }
    public void CreateRoom()
    {
        clickSoundEffect.Play();

        if (string.IsNullOrWhiteSpace(roomName.text))
        {
            Popup.Show("Empty room name", "Enter room name field", "OK", PopupColor.Magenta);
            Debug.Log("Vui lòng nhập tên phòng.");
            return;
        }
        if (string.IsNullOrWhiteSpace(nicknameInput.text))
        {
            Popup.Show("Empty nick name", "Enter nick name field", "OK", PopupColor.Magenta);
            Debug.Log("Vui lòng nhập nick name");
            return;

        }
        if (string.IsNullOrWhiteSpace(nicknameInput.text) && string.IsNullOrWhiteSpace(roomName.text))
        {
            Popup.Show("Empty field", "Enter all field", "OK", PopupColor.Magenta);
            return;
        }

        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 4 };

        PhotonNetwork.CreateRoom(roomName.text, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        clickSoundEffect.Play();
        PhotonNetwork.NickName = nicknameInput.text;
        PlayerPrefs.SetString("Nickname", PhotonNetwork.NickName);
        PhotonNetwork.LoadLevel("waitingRoom");
    }
    public void JoinRoom()
    {
        if (string.IsNullOrWhiteSpace(nicknameInput.text) && string.IsNullOrWhiteSpace(roomName.text))
        {
            Popup.Show("Empty field", "Enter all field", "OK", PopupColor.Magenta);
            return;
        }
        if (string.IsNullOrWhiteSpace(roomName.text))
        {
            Popup.Show("Empty room name", "Enter room name field", "OK", PopupColor.Magenta);
            Debug.Log("Vui lòng nhập tên phòng.");
            return;
        }
        if (string.IsNullOrWhiteSpace(nicknameInput.text))
        {
            Popup.Show("Empty nick name", "Enter nick name field", "OK", PopupColor.Magenta);
            Debug.Log("Vui lòng nhập nick name");
            return;

        }

        clickSoundEffect.Play();
        PhotonNetwork.JoinRoom(roomName.text);
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void StartGame()
    {
        clickSoundEffect.Play();
        PhotonNetwork.LoadLevel("MainScene");
        PhotonNetwork.CurrentRoom.IsOpen = false;
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

    public void GoToMenu()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.LeaveRoom();
    }
}

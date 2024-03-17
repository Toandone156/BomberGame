using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateGameMenu : MonoBehaviourPunCallbacks
{
    public Canvas endGameCanvas;
    public Canvas deathCanvas;
    public TMP_Text title;
    public TMP_Text body;

    // Start is called before the first frame update
    void Start()
    {
        endGameCanvas.enabled = false;
        deathCanvas.enabled = false;
        Time.timeScale = 1;
    }

    [System.Obsolete]
    public void CheckState(bool isMine)
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        var remainPlayers = players.Count(p => p.active);

        Debug.Log(remainPlayers);

        if (remainPlayers == 1)
        {
            var player = players.SingleOrDefault(p => p.active);
            if (player.GetPhotonView().IsMine)
            {
                WinGame();
            }
            else
            {
                LoseGame();
            }

            PhotonNetwork.CurrentRoom.IsOpen = true;
        }else if (isMine)
        {
            ShowDeathPopup();
        }
    }

    public void WinGame()
    {
        title.text = "WINNER WINNER";
        body.text = "Congrats! Your are chicken winner!\nPlay again to win again";
        endGameCanvas.enabled = true;
        deathCanvas.enabled = false;
        Time.timeScale = 0;
    }

    public void LoseGame()
    {
        title.text = "TRY HARD";
        body.text = "You are good player!\nPlay try harder to win";
        endGameCanvas.enabled = true;
        deathCanvas.enabled = false;
        Time.timeScale = 0;
    }

    public void ShowDeathPopup()
    {
        deathCanvas.enabled = true;
    }

    public void ClosePopup()
    {
        endGameCanvas.enabled = false;
        deathCanvas.enabled = false;
        Time.timeScale = 1;
    }
}

using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpawnWaiting : MonoBehaviourPunCallbacks
{
    public List<GameObject> playerObjects;
    public List<Vector2> spawnPosition;
    public GameObject startButton;
    // Start is called before the first frame update
    void Start()
    {
        var playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        PhotonNetwork.AutomaticallySyncScene = true;
        // so nguoi cho trong phong
        if (playerCount <= 4)
        {
            PhotonNetwork.Instantiate(playerObjects[playerCount - 1].name, spawnPosition[playerCount - 1], Quaternion.identity);
            var players = GameObject.FindGameObjectsWithTag("Player");
            //foreach (var player in players)
            //{
            //    player.transform.SetParent(GameObject.FindGameObjectWithTag("UI").transform, false);
            //}
            GetComponent<PhotonView>().RPC("MoveToCanvas", RpcTarget.All);
            PlayerPrefs.SetInt("Player", playerCount);

            IdentityMasterClient();
        }
    }
    [PunRPC]
    public void MoveToCanvas()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in players)
        {
            player.transform.SetParent(GameObject.FindGameObjectWithTag("UI").transform, false);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GetComponent<PhotonView>().RPC("MoveToCanvas", RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Invoke("CallSortPlayer", 0.2f);
    }

    public void CallSortPlayer()
    {
        GetComponent<PhotonView>().RPC("SortPlayer", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void SortPlayer()
    {
        var players = GameObject.FindGameObjectsWithTag("Player").OrderBy(o => o.GetComponent<PhotonView>().ViewID).ToArray();
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.SetLocalPositionAndRotation(spawnPosition[i], Quaternion.identity);
            players[i].GetComponent<Image>().sprite = playerObjects[i].GetComponent<Image>().sprite;
            if (players[i].GetComponent<PhotonView>().IsMine)
            {
                PlayerPrefs.SetInt("Player", i + 1);
            }
        }

        IdentityMasterClient();
    }

    void IdentityMasterClient()
    {
        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }
}

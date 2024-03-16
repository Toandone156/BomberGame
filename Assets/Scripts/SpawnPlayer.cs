using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public List<GameObject> players;
    public List<Vector2> spawnPositions;
    public LayerMask playerLayer;

    private void Start()
    {
        var playerCount = PlayerPrefs.GetInt("Player");
        GameObject playerObject = PhotonNetwork.Instantiate(players[playerCount - 1].name, spawnPositions[playerCount - 1], Quaternion.identity);
        GameObject.FindAnyObjectByType<CameraFollow>().setPlayer(playerObject);
    }
}
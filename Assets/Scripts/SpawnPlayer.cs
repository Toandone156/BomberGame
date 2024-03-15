﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Vector2> spawnPositions;
    public LayerMask playerLayer;

    private void Start()
    {
        var playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if(playerCount <= 4)
        {

            GameObject.FindAnyObjectByType<CameraFollow>().setPlayer(PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[playerCount - 1], Quaternion.identity));
        }
    }
}

/*
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Vector2> spawnPositions;
    public LayerMask playerLayer;

    private void Start()
    {
        var playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if(playerCount <= 4)
        {
            GameObject playerObject = PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[playerCount - 1], Quaternion.identity);
            playerObject.tag = "Player"; // Gắn tag "Player" cho đối tượng player.
        }
    }
}
*/
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    private Transform target;

    void Start()
    {
        
            // Tìm tất cả các đối tượng trong danh sách GameObject với tag "Player"
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            // Tìm player của mình trong danh sách và gán làm target
            foreach (GameObject player in players)
            {
                PhotonView playerPhotonView = player.GetComponent<PhotonView>();
                if (playerPhotonView != null && playerPhotonView.IsMine)
                {
                    target = player.transform;
                    break; // Kết thúc vòng lặp sau khi tìm thấy target
                }
            }

            // Nếu không tìm thấy player của mình trong danh sách, thông báo lỗi
            if (target == null)
            {
                Debug.LogError("Player object not found with tag 'Player'.");
            }
        
    }

    void Update()
    {
        if (target != null)
        {
            // Cập nhật vị trí camera
            Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
            transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
 
    }
    
    public void setPlayer(GameObject player)
    {
        target = player.transform;
    }

}

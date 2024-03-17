using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private RoomListing roomListing;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        foreach (RoomInfo roomInfo in roomList)
        {
            Debug.Log(roomInfo.Name);
            RoomListing listing = Instantiate(roomListing, _content);
            if (listing != null)
            {
                listing.SetRoomInfo(roomInfo);
            }
        }

    }
}

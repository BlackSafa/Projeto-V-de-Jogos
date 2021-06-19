using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    RoomListingScript roomListing;
    [SerializeField]
    Transform content;

    private int roomsOnLobby;

    private List<RoomListingScript> rooms = new List<RoomListingScript>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList)
        {
            if(info.RemovedFromList)
            {
                int index = rooms.FindIndex(x => x.room.Name == info.Name);
                if(index != -1)
                {
                    Destroy(rooms[index].gameObject);
                    rooms.RemoveAt(index);
                }
            }

            else if (roomList.Count == roomsOnLobby)
            {
                int index = rooms.FindIndex(x => x.room.Name == info.Name);
                if(index != -1)
                {
                    rooms[index].SetRoomInfo(info);
                }
            }
            else{
                RoomListingScript listing = (RoomListingScript)Instantiate(roomListing, content);
                if(listing != null)
                {
                    listing.SetRoomInfo(info);
                    rooms.Add(listing);
                }
            }
        }

        roomsOnLobby = roomList.Count;
    }
}

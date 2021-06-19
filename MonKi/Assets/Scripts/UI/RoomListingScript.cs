using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public class RoomListingScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI roomName, number, roomEnter;
    public RoomInfo room {get; private set;}

    private void Start() {
        roomEnter = GameObject.Find("RoomField").GetComponent<TextMeshProUGUI>();
    }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        room = roomInfo;
        roomName.text = roomInfo.Name;
        number.text = roomInfo.PlayerCount.ToString() + ( roomInfo.PlayerCount >= 2 ? "Jogadores" : " Jogador");

    }

    void SetRoom()
    {
        roomEnter.text = roomName.text;
    }
}

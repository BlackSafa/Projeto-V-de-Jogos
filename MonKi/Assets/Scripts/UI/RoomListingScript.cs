using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public class RoomListingScript : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI roomName, number;
    TMP_InputField Troom;

    public RoomInfo room {get; private set;}

    private void Start() {
        Troom = GameObject.Find("Sala campo de texto").GetComponent<TMP_InputField>();
    }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        room = roomInfo;
        roomName.text = roomInfo.Name;
        number.text = roomInfo.PlayerCount.ToString() + ( roomInfo.PlayerCount >= 2 ? "Jogadores" : " Jogador");

    }

    public void SetRoom()
    {
        string sender= string.Empty;

        for (int i = 0; i < (roomName.text.Length - 1); i++)
        {
            sender+= roomName.text.ToCharArray()[i]; 
        }
        
        Troom.text = sender;
    }
}

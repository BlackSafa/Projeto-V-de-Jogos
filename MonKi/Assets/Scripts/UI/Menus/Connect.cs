using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Connect : MonoBehaviourPunCallbacks
{
    //public delegate void GameStart();
    [SerializeField]
    private Text nickName, roomName;

    public GameObject monkeyMenu;
    // Start is called before the first frame update

    private void Start() 
    {
        //if(GetComponent<Button>() == null)
            PhotonNetwork.ConnectUsingSettings();
    }
    public void ConnectHit()
    {
        //PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Conectando");
        //logText.text = "Log Panel: \nConectando";
        PhotonNetwork.NickName = nickName.text;
        RoomOptions roomOptions = new RoomOptions() {IsVisible = true, MaxPlayers = 3};
        //PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinOrCreateRoom(roomName.text, roomOptions, TypedLobby.Default);
    }

    public override void OnConnected()
    {
        Debug.Log("Conectado ao Photon");
        //logText.text = logText.text + "\nConectado ao Photon";
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado como Master");
        //logText.text = logText.text + "\nConectado como Master";
        PhotonNetwork.JoinLobby();
        //FindObjectOfType<Canvas>().gameObject.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entrando na Sala");
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.NickName = nickName.text + " P(1)";
        else
            PhotonNetwork.NickName = nickName.text + " P(" + PhotonNetwork.CurrentRoom.PlayerCount + ")";
        monkeyMenu.SetActive(true);
        //logText.text = "\nEntrando na Sala";
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Saindo da Sala");
        monkeyMenu.SetActive(false);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconectado do server por: " + cause.ToString());
        //logText.text = "\nDisconectado do server por: " + cause.ToString();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Falha em conectar na sala" + message.ToString());
        //logText.text = "\nFalha em conectar na sala" + message.ToString();
        PhotonNetwork.Disconnect();
    }

    private void OnApplicationQuit() 
    {
        PhotonNetwork.Disconnect();   
    }
}

/*using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Connect : MonoBehaviourPunCallbacks
{
    //public delegate void GameStart();
    [SerializeField]
    private Text nickName, roomName, logText;
    // Start is called before the first frame update

    private void Start() 
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public void ConnectHit()
    {
        Debug.Log("Conectando");
        logText.text = "Log Panel: \nConectando";
        PhotonNetwork.NickName = nickName.text;
        RoomOptions roomOptions = new RoomOptions() {IsVisible = true, MaxPlayers = 3};
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinOrCreateRoom(roomName.text, roomOptions, TypedLobby.Default);
        
    }

    public override void OnConnected()
    {
        Debug.Log("Conectado");
        logText.text = logText.text + "\nConectado ao Photon";
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado como Master");
        logText.text = logText.text + "\nConectado como Master";
        PhotonNetwork.JoinLobby();
        //FindObjectOfType<Canvas>().gameObject.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        logText.text = "\nEntrando na Sala";
        if(PhotonNetwork.IsMasterClient)
            //SceneManager.LoadScene(2, LoadSceneMode.Single);
            {
                PhotonNetwork.LoadLevel(2);
            }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconectado do server por: " + cause.ToString());
        logText.text = "\nDisconectado do server por: " + cause.ToString();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        logText.text = "\nFalha em conectar na sala" + message.ToString();
        PhotonNetwork.Disconnect();
    }

    private void OnApplicationQuit() 
    {
        PhotonNetwork.Disconnect();   
    }
}*/
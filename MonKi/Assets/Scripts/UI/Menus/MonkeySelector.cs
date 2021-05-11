using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MonkeySelector : MonoBehaviour
{
    Image R, M, F;

    bool statusR, statusM, statusF;
    PlayerMode selectedMonkey;
    Color standBy, selecting, setted, Unselectable;
    PhotonView photonView;

    private void Awake() {
        photonView = GetComponent<PhotonView>();
    }

    private void Update() {
        
    }

    public void OnClickF()
    {
        F.color = selecting;
        selectedMonkey = PlayerMode.Strong;
    }

    public void OnClickM()
    {
        M.color = selecting;
        selectedMonkey = PlayerMode.Psychic;
    }

    public void OnClickA()
    {
        R.color = selecting;
        selectedMonkey = PlayerMode.Fast;
    }

    public void OnClickNone()
    {
        switch(selectedMonkey)
        {
            case (PlayerMode.Fast):
                F.color = selecting;
                selectedMonkey = PlayerMode.Strong;
                break;
            case (PlayerMode.Psychic):
                F.color = selecting;
                selectedMonkey = PlayerMode.Strong;
                break;
            case (PlayerMode.Strong):
                F.color = selecting;
                selectedMonkey = PlayerMode.Strong;
                break;
            default:
                break;
        }
    }

    public void OnClickSelect()
    {
        switch(selectedMonkey)
        {
            case (PlayerMode.Fast):
                R.color = selecting;
                
                break;
            case (PlayerMode.Psychic):
                M.color = selecting;
                
                break;
            case (PlayerMode.Strong):
                F.color = selecting;
                
                break;
            default:
                break;
        }
    }

    [PunRPC]
    public void SetMonkey(PlayerMode choice)
    {
        switch(choice)
        {
            case (PlayerMode.Fast):
                break;
            case (PlayerMode.Psychic):
                break;
            case (PlayerMode.Strong):
                break;
            default:
                break;
        }
    }
}

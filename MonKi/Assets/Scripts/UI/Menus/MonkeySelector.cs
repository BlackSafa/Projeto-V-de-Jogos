using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MonkeySelector : MonoBehaviour
{
    Button R, M, F, None, select;
    PlayerMode monkey;

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

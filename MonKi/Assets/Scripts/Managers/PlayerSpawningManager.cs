using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawningManager : MonoBehaviour
{
    public Transform orSpPointF, orSpPointP, orSpPointS, sndLvlSpawn;
    void Start()
    {
        switch(MasterManager.GameSettings.PlayerMode)
        {
            case(PlayerMode.Fast):
                PhotonNetwork.Instantiate("Fast Monkey", orSpPointF.position, Quaternion.identity);
                break;
            case(PlayerMode.Psychic):
                PhotonNetwork.Instantiate("Psychic Monkey", orSpPointP.position, Quaternion.identity);
                break;
            case(PlayerMode.Strong):
                PhotonNetwork.Instantiate("Strong Monkey", orSpPointS.position, Quaternion.identity);
                break;
        }
    }
}

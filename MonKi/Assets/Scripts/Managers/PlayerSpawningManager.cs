using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawningManager : MonoBehaviour
{
    Transform orSpPointF, orSpPointP, orSpPointS, sndLvlSpawn;
    void Start()
    {
        switch(MasterManager.GameSettings.PlayerMode)
        {
            case(PlayerMode.Fast):
                PhotonNetwork.Instantiate("Fast Monkey", orSpPointF.position, Quaternion.identity);
                break;
            case(PlayerMode.Psychic):
                PhotonNetwork.Instantiate("Psychic Monkey", orSpPointF.position, Quaternion.identity);
                break;
            case(PlayerMode.Strong):
                PhotonNetwork.Instantiate("Strong Monkey", orSpPointF.position, Quaternion.identity);
                break;
        }
    }
}

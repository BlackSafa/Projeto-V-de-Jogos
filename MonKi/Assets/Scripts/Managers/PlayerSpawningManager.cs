using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawningManager : MonoBehaviour
{
    public Transform orSpPointF, orSpPointP, orSpPointS, actualSpawn;

    void Start()
    {
        GameObject monkey;
        switch(MasterManager.GameSettings.PlayerMode)
        {
            case(PlayerMode.Fast):
                monkey = PhotonNetwork.Instantiate("Fast Monkey", orSpPointF.position, Quaternion.identity);
                monkey.GetComponent<InteracterScript>().spawn = orSpPointF;
                monkey.GetComponent<InteracterScript>().spawn.localScale = monkey.transform.localScale;
                break;
            case(PlayerMode.Psychic):
                monkey = PhotonNetwork.Instantiate("Psychic Monkey", orSpPointP.position, Quaternion.identity);
                monkey.GetComponent<InteracterScript>().spawn = orSpPointP;
                monkey.GetComponent<InteracterScript>().spawn.localScale = monkey.transform.localScale;
                break;
            case(PlayerMode.Strong):
                monkey = PhotonNetwork.Instantiate("Strong Monkey", orSpPointS.position, Quaternion.identity);
                monkey.GetComponent<InteracterScript>().spawn = orSpPointS;
                monkey.GetComponent<InteracterScript>().spawn.localScale = monkey.transform.localScale;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView photonView = other.GetComponent<PhotonView>();
        if(photonView.Owner != null)
        {
            photonView.RPC("Respawn",RpcTarget.All);
        }
        else
        {
            photonView.RPC("Realocate",RpcTarget.All);
        }
    }
}

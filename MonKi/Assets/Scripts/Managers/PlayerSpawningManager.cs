using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawningManager : MonoBehaviour
{
    public Transform orSpPointF, orSpPointP, orSpPointS, actualSpawn;
    PhotonView monkeypw;

    void Start()
    {
        GameObject monkey;
        switch(MasterManager.GameSettings.PlayerMode)
        {
            case(PlayerMode.Fast):
                monkey = PhotonNetwork.Instantiate("Fast Monkey", orSpPointF.position, orSpPointF.rotation);
                monkey.GetComponent<InteracterScript>().spawn = orSpPointF;
                monkey.GetComponent<InteracterScript>().spawn.localScale = monkey.transform.localScale;
                monkeypw = monkey.GetComponent<PhotonView>();
                break;
            case(PlayerMode.Psychic):
                monkey = PhotonNetwork.Instantiate("Psychic Monkey", orSpPointP.position, orSpPointP.rotation);
                monkey.GetComponent<InteracterScript>().spawn = orSpPointP;
                monkey.GetComponent<InteracterScript>().spawn.localScale = monkey.transform.localScale;
                monkeypw = monkey.GetComponent<PhotonView>();
                break;
            case(PlayerMode.Strong):
                monkey = PhotonNetwork.Instantiate("Strong Monkey", orSpPointS.position, orSpPointS.rotation);
                monkey.GetComponent<InteracterScript>().spawn = orSpPointS;
                monkey.GetComponent<InteracterScript>().spawn.localScale = monkey.transform.localScale;
                monkeypw = monkey.GetComponent<PhotonView>();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView photonView = other.GetComponent<PhotonView>();
        if(photonView.GetComponent<InteracterScript>() != null)
        {
            photonView.RPC("Respawn",RpcTarget.All);
        }
        else if(photonView.GetComponent<InteractiveObject>() != null)
        {
            photonView.RPC("Realocate",RpcTarget.All);
        }
    }

    [PunRPC]
    public void CallSpawnChange(float[] pos, float[] rot)
    {
        monkeypw.RPC("ChangeSpawn", RpcTarget.All, pos, rot);
    }
}

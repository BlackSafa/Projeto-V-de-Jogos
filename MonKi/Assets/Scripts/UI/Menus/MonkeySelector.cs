using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MonkeySelector : MonoBehaviour
{
    [SerializeField]
    MonkeyOption R, M, F, none;
    PlayerMode selectedMonkey = PlayerMode.None;
    [SerializeField]
    Color standBy, selecting, setted, unselectable;
    PhotonView photonView;
    public bool gameStarted, selected;

    private void Awake() {
        photonView = GetComponent<PhotonView>();
        R = new MonkeyOption(transform.Find("Rapido").gameObject);
        M = new MonkeyOption(transform.Find("Mental").gameObject);
        F = new MonkeyOption(transform.Find("Forte").gameObject);
        photonView = GetComponent<PhotonView>();
        
    }

    private void Update() {
        if(!R.avaliability)
        {
            R.button.interactable = false;
            R.image.color = unselectable;
        }
        if(!M.avaliability)
        {
            M.button.interactable = false;
            M.image.color = unselectable;
        }
        if(!F.avaliability)
        {
            F.button.interactable = false;
            F.image.color = unselectable;
        }
    }

    public void OnClickF()
    {
        F.image.color = selecting;
        selectedMonkey = PlayerMode.Strong;
    }

    public void OnClickM()
    {
        M.image.color = selecting;
        selectedMonkey = PlayerMode.Psychic;
    }

    public void OnClickA()
    {
        R.image.color = selecting;
        selectedMonkey = PlayerMode.Fast;
    }

    public void OnClickNone()
    {
        switch(selectedMonkey)
        {
            case (PlayerMode.Fast):
                R.image.color = standBy;
                if(selected)
                    photonView.RPC("MonkeySetter",RpcTarget.OthersBuffered, selectedMonkey, false);
                R.button.interactable = true;
                M.button.interactable = true;
                F.button.interactable = true;
                selectedMonkey = PlayerMode.None;
                break;
            case (PlayerMode.Psychic):
                M.image.color = standBy;
                if(selected)
                    photonView.RPC("MonkeySetter",RpcTarget.OthersBuffered, selectedMonkey, false);
                R.button.interactable = true;
                M.button.interactable = true;
                F.button.interactable = true;
                selectedMonkey = PlayerMode.None;
                break;
            case (PlayerMode.Strong):
                F.image.color = standBy;
                if(selected)
                    photonView.RPC("MonkeySetter",RpcTarget.OthersBuffered, selectedMonkey, false);
                R.button.interactable = true;
                M.button.interactable = true;
                F.button.interactable = true;
                selectedMonkey = PlayerMode.None;
                break;
        }
        selected = false;
    }

    public void OnClickSelect()
    {
        switch(selectedMonkey)
        {
            case (PlayerMode.Fast):
                if(R.avaliability)
                {
                    R.image.color = setted;
                    R.button.interactable = false;
                    M.button.interactable = false;
                    F.button.interactable = false;
                    photonView.RPC("MonkeySetter",RpcTarget.OthersBuffered, selectedMonkey, true);
                }
                else
                {
                    Debug.Log("Macaco Indisponível");
                    selectedMonkey = PlayerMode.None;
                }
                break;
            case (PlayerMode.Psychic):
                if(M.avaliability)
                {
                    M.image.color = setted;
                    R.button.interactable = false;
                    M.button.interactable = false;
                    F.button.interactable = false;
                    photonView.RPC("MonkeySetter",RpcTarget.OthersBuffered, selectedMonkey, true);
                }
                else
                {
                    Debug.Log("Macaco Indisponível");
                    selectedMonkey = PlayerMode.None;
                }
                break;
            case (PlayerMode.Strong):
                if(F.avaliability)
                {
                    F.image.color = setted;
                    R.button.interactable = false;
                    M.button.interactable = false;
                    F.button.interactable = false;
                    photonView.RPC("MonkeySetter",RpcTarget.OthersBuffered, selectedMonkey, true);
                }
                else
                {
                    Debug.Log("Macaco Indisponível");
                    selectedMonkey = PlayerMode.None;
                }
                break;
        }
        selected = true;
        OnStartGame();
    }

    void OnStartGame()
    {
        int x = 0;
        x = !R.avaliability ? x+=1 : x;
        x = !M.avaliability ? x+=1 : x;
        x = !F.avaliability ? x+=1 : x;
        x = selected ? x+=1 : x;
        if(x >= 3)
        {
            photonView.RPC("ChangeScene",RpcTarget.All);
        }
    }

    [PunRPC]
    public void MonkeySetter(PlayerMode choice, bool settingMode)
    {
        if(settingMode){
            switch(choice)
            {
                case (PlayerMode.Fast):
                    R.avaliability = false;
                    break;
                case (PlayerMode.Psychic):
                    M.avaliability = false;
                    break;
                case (PlayerMode.Strong):
                    F.avaliability = false;
                    break;
            }
        }

        else
        {
            switch(choice)
            {
                case (PlayerMode.Fast):
                    R.avaliability = true;
                    R.button.interactable = true;
                    R.image.color = standBy;
                    break;
                case (PlayerMode.Psychic):
                    M.avaliability = true;
                    M.button.interactable = true;
                    M.image.color = standBy;
                    break;
                case (PlayerMode.Strong):
                    F.avaliability = true;
                    F.button.interactable = true;
                    F.image.color = standBy;
                    break;
            }
        }
    }

    private class MonkeyOption
    {
        public Button button;
        public Image image;
        public bool avaliability;

        public MonkeyOption(GameObject target)
        {
            button = target.GetComponent<Button>();
            image = target.GetComponent<Image>();
            avaliability = true;
        }

    }

    [PunRPC]
    public void ChangeScene()
    {
        MasterManager.GameSettings.SetChoice(selectedMonkey);
        PhotonNetwork.AutomaticallySyncScene = true;
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Level 1");
        }
    }
}

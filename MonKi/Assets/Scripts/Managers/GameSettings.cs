using System.Collections;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private string _nickName = "Anon";
    public string  NickName
    {
        get
        {
            return _nickName;
        }
    }

    [SerializeField]
    private string _roomName = "TestRoom";
    public string RoomName
    {
        get
        {
            return _roomName;
        }
    }

    private PlayerMode playerMode;

    public PlayerMode PlayerMode
    {
        get
        {
            return playerMode;
        }
    }

    public void setChoice(PlayerMode choice)
    {
        playerMode = choice;
    }
}

[Flags]
public enum PlayerMode
{
    Fast,
    Psychic,
    Strong
}
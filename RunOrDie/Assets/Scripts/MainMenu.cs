using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private InputField nameroom;
    [SerializeField]
    private InputField nickname;
    [SerializeField]
    private InputField playerforstartinput;
    public void CreateRoom()
    {
        Debug.Log(nickname.text);
        PhotonNetwork.NickName = nickname.text;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(nameroom.text, roomOptions);
        ForData.playerforstart = int.Parse(playerforstartinput.text);
    }
    public void joinRoom()
    {
        PhotonNetwork.NickName = nickname.text;
        PhotonNetwork.JoinRoom(nameroom.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
    public void Easy()
    {
        ForData.dificulty = "easy";
    }
    public void Normal()
    {
        ForData.dificulty = "normal";
    }
    public void Hard()
    {
        ForData.dificulty = "hard";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class IDInfo: MonoBehaviourPunCallbacks
{
    static IDInfo S;
    public string UserNickname;


    void Awake()
    {
        S = this;
    }
    public void getNickname(string str)
    {
        UserNickname = str; //로그인시에 호출됨
        PhotonNetwork.LocalPlayer.NickName = str;
    }
}

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviourPunCallbacks
{
    public void JoinRoom()
    {
        string str = transform.GetChild(0).GetComponent<Text>().text;
        PhotonNetwork.JoinRoom(str);
    }
    
}

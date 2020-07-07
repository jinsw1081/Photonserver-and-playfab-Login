using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
//친구 추가부터 만들기
public class FriendManger : MonoBehaviourPunCallbacks
{
    string[] Friends = new string[30];
    List<string> firends;

    void Start()
    {
        
    }
    void OnClickAddfriends()
    {

    }
    void DisplayFriends(List<string> friendsCache)
    { friendsCache.ForEach(f => Debug.Log(f)); }
    void DisplayPlayFabError(PlayFabError error) { Debug.Log(error.GenerateErrorReport()); }
    void DisplayError(string error) { Debug.LogError(error); }

    void OpenFriendsWindow()
    {
        PhotonNetwork.FindFriends(Friends);
    }

    void Update()
    {
        
    }
}

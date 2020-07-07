using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class JoinRoom : MonoBehaviourPunCallbacks,IInRoomCallbacks
{
    List<RoomInfo> RoomList=new List<RoomInfo>();
    bool boolActiveCreatRoom = false;
    ExitGames.Client.Photon.Hashtable hashtable ;

    public string JoinRoomName;
    public Button newbutton;
    public ScrollRect scrollRect;
    public InputField RoomName;
    public GameObject RoomPanel;
    public GameObject CreatroomSelection;

     void Start()
    {
        hashtable = new ExitGames.Client.Photon.Hashtable() { { "Reday", Readycheck() }, {"","" } };
        PhotonNetwork.SetPlayerCustomProperties(hashtable);
    }

    public void OnClickCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.PublishUserId = true;
        roomOptions.MaxPlayers = 4;

        string str = RoomName.text;
        //  string password = RoomPassword.text;
        PhotonNetwork.CreateRoom(str, roomOptions,null);
        
    }
    
    public void ActiveCreateRoom()
    {
        if (!boolActiveCreatRoom)
        {
            CreatroomSelection.SetActive(true);
            boolActiveCreatRoom = true;
        }
        else
        {
            boolActiveCreatRoom = false;
            CreatroomSelection.SetActive(false);
        }
    }
    

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        foreach (RoomInfo roominfo in roomList)
        {
            if(roominfo.IsOpen)
            {
                RoomList.Add(roominfo);
            }
        }

        int i = 0;
        foreach(RoomInfo roominfo in RoomList)
        {
            i++;
            RectTransform recttr = scrollRect.content.GetChild(0).GetComponent<RectTransform>();
            Vector3 vector3 = recttr.localPosition;
            Button instancBut = Instantiate<Button>(newbutton);
            instancBut.transform.parent = scrollRect.content;
            instancBut.transform.localScale = Vector3.one;
            instancBut.transform.GetChild(0).GetComponent<Text>().text = roominfo.Name;
            vector3.y -= 40;

            instancBut.transform.localPosition = vector3;
        }
    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.ConnectUsingSettings();

        scrollRect.gameObject.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        ActiveCreateRoom();
        RoomPanel.SetActive(true);
        scrollRect.gameObject.SetActive(false);
        var players = PhotonNetwork.PlayerList;
        int num = 0;
        foreach (var numPlayer in players)
        {

            RoomPanel.transform.GetChild(num).gameObject.SetActive(true);
            RoomPanel.transform.GetChild(num).GetChild(0).GetComponent<Text>().text = players[num].NickName;
            num++;
        }
        for (; num < 4; num++)
        {
            RoomPanel.transform.GetChild(num).GetChild(0).GetComponent<Text>().text = null;
            RoomPanel.transform.GetChild(num).gameObject.SetActive(false);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        var players = PhotonNetwork.PlayerList;
        int num = 0;
        foreach (var numPlayer in players)
        {
            Debug.Log(RoomPanel.transform.GetChild(num).gameObject);
            RoomPanel.transform.GetChild(num).gameObject.SetActive(true);
            RoomPanel.transform.GetChild(num).GetChild(0).GetComponent<Text>().text = players[num].NickName;
            num++;

        }
        for (; num < 4; num++)
        {
            RoomPanel.transform.GetChild(num).GetChild(0).GetComponent<Text>().text = null;
            RoomPanel.transform.GetChild(num).gameObject.SetActive(false);
        }
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        var players = PhotonNetwork.PlayerListOthers;
        int num = 0;
        foreach (var numPlayer in players)
        {
            RoomPanel.transform.GetChild(num).gameObject.SetActive(true);
            RoomPanel.transform.GetChild(num).GetChild(0).GetComponent<Text>().text = players[num].NickName;
            num++;

        }
        for (; num < 3; num++)
        {
            RoomPanel.transform.GetChild(num).GetChild(0).GetComponent<Text>().text = null;
            RoomPanel.transform.GetChild(num).gameObject.SetActive(false);
        }
        //CsReady.boolReady.re
    }

    public void CallStart()
    {
        if(PhotonNetwork.IsMasterClient)
        PhotonNetwork.LoadLevel("S2");
    }
    bool previousRed = true;
    [PunRPC]
    public bool Readycheck()
    {
        //정리 해보면은 각각플레이어마다 레디를 마스터 클라이언트한테 전해줘야함
        //전해주는데 전해줄 CsReady는 최대 4개의 값을 가지고 있어야함
        //4개의 값을 
        if (previousRed)
            previousRed = false;
        else
            previousRed = true;
        return previousRed;
    }
}

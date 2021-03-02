using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonLobbyCustom : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static PhotonLobbyCustom lobby;

    string roomName;
    int roomSize;
    public GameObject roomListingprefab;
    public Transform roomsPanel;


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        RemoveRoomListings();
        foreach (RoomInfo room in roomList)
        {
            ListRoom(room);
        }
    }

    void RemoveRoomListings()
    {
        while(roomsPanel.childCount != 0)
        {
            Destroy(roomsPanel.GetChild(0).gameObject);
        }
    }

    void ListRoom(RoomInfo room)
    {
        if (room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingprefab, roomsPanel);
            RoomBtn tempBtn = tempListing.GetComponent<RoomBtn>();
            tempBtn.roomName = room.Name;
            tempBtn.roomSize = room.MaxPlayers;
            tempBtn.SetRoom();
        }
    }

    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        Debug.Log("Trying to create a new Room");
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(roomName, roomOps, TypedLobby.Default);
        

    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Create room good");
        SceneManager.LoadScene(3);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed trying to create room");
        CreateRoom();
    }

    public void OnRoomNameChange(string nameIn)
    {
        roomName = nameIn;
        
    }

    public void OnRoomSizeChange(string sizeIn)
    {
        roomSize = int.Parse(sizeIn);
        
    }

    public void JoinLobbyOnClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }
}

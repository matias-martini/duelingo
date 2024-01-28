using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;  // Namespace for TextMeshPro
using Mirror;

public class FetchRooms : MonoBehaviour
{
    [SerializeField] private GameObject _spinner;
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _roomLinePrefab;

    void Start()
    {
        NetworkClient.RegisterHandler<RoomListMessage>(OnRoomListReceived);
    }

    void OnEnable()
    {
        CleanRoomList();
        ConnectToServer();
    }

    private void OnRoomListReceived(RoomListMessage msg)
    {
        foreach (var room in msg.rooms)
        {
            Debug.Log($"Room: {room.roomName}, ID: {room.roomId}, Map: {room.mapName}, Max Players: {room.maxPlayers}");

            GameObject instance = Instantiate(_roomLinePrefab, _content.transform);

            TMP_Text mapNameText = instance.transform.Find("map_name").GetComponent<TMP_Text>();
            mapNameText.text = room.mapName;

            // Find and set the second text by name
            TMP_Text players = instance.transform.Find("players").GetComponent<TMP_Text>();
            players.text =  "0/" + room.maxPlayers;

            // Find and set the third text by name
            TMP_Text roomName = instance.transform.Find("name").GetComponent<TMP_Text>();
            roomName.text = room.roomName;
        }

        _spinner.SetActive(false);
        _content.SetActive(true);
    }

    public void TriggerRefresh()
    {
        CleanRoomList();
        _spinner.SetActive(true);
        _content.SetActive(false);
        RequestRoomList();

    }

    void CleanRoomList()
    {
        foreach (Transform child in _content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void ConnectToServer()
    {
        string serverAddress = "127.0.0.1";

        NetworkManager.singleton.networkAddress = serverAddress;
        NetworkManager.singleton.StartClient();

        RequestRoomList();
    }
    public void RequestRoomList()
    {
        if (NetworkClient.isConnected)
        {
            NetworkClient.Send(new RequestRoomListMessage());
        }
    }


    [System.Serializable]
    public class RoomList
    {
        public Room[] rooms;
    }

    [System.Serializable]
    public class Room
    {
        public string name;
        public string map;
        public string ip;
        public int players;
        public int max_players;
        public string last_updated;
    }
}

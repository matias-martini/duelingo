using Mirror;
using UnityEngine;
using System.Collections.Generic;
using System;


public class MyNetworkManager : NetworkManager
{
    public List<GameRoom> rooms = new List<GameRoom>();

    public override void Start()
    {
        Debug.Log("Running in headless mode");
        base.Start();

        // StartServer();
    }
    public override void OnStartServer()
    {
        NetworkServer.RegisterHandler<RequestRoomListMessage>(OnRequestRoomList);
        Debug.Log($"Server started on {networkAddress}");
        base.OnStartServer();
    }
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
        Debug.Log($"Client connected: {conn.address}");
    }
    private void OnRequestRoomList(NetworkConnection conn, RequestRoomListMessage msg)
    {
        List<RoomInfo> roomInfos = new List<RoomInfo>();
        foreach (var room in rooms)
        {
            RoomInfo info = new RoomInfo
            {
                roomId = room.RoomId,
                roomName = room.RoomName,
                mapName = room.MapName,
                maxPlayers = room.MaxPlayers
            };
            roomInfos.Add(info);
        }

        conn.Send(new RoomListMessage { rooms = roomInfos.ToArray() });
    }


    public GameRoom CreateRoom(string name, int maxPlayers, string mapName)
    {
        // Create a new room with a unique ID
        GameRoom newRoom = new GameRoom(rooms.Count, name, mapName, maxPlayers);
        rooms.Add(newRoom);
        return newRoom;
    }
    public void HandleCreateRoomRequest(string roomName, int maxPlayers, string mapName, NetworkConnectionToClient requester)
    {
        GameRoom room = CreateRoom(roomName, maxPlayers, mapName);

        PlayerRoomRequests playerScript = requester.identity.GetComponent<PlayerRoomRequests>();
        if (playerScript != null)
        {
            playerScript.TargetRoomCreated(requester, room.RoomId);
        }
    }


    public bool UpdateRoom(int id, string name)
    {
        GameRoom newRoom = rooms.Find(room => room.RoomId == id);
        if (newRoom == null)
        {
            return false;
        }

        newRoom.RoomName = name;

        return true;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        Debug.Log("Client disconnected");
    }

    public List<GameRoom> GetRooms()
    {
        return rooms;
    }
}


public struct RequestRoomListMessage : NetworkMessage { }
public struct RoomInfo
{
    public int roomId;
    public string roomName;
    public string mapName;
    public int maxPlayers;
}

public struct RoomListMessage : NetworkMessage
{
    public RoomInfo[] rooms;
}


[Serializable]
public class GameRoom
{
    public int RoomId { get; set; }
    public string RoomName { get; set; }
    public string MapName { get; set; }
    public int MaxPlayers { get; set; }

    public GameRoom(int id, string name = "Untitled Room", string mapName = "Monger's Cave", int maxPlayers = 4)
    {
        RoomId = id;
        RoomName = name;
        MapName = mapName;
        MaxPlayers = maxPlayers;
    }
}
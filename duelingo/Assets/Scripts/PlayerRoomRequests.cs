using Mirror;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerRoomRequests : NetworkBehaviour
{
    private int _roomId;
    // Command to request room creation. This is called on the client but executed on the server.
    [Command]
    public void CmdRequestCreateRoom(string roomName, int maxPlayers, string mapName)
    {
        ((MyNetworkManager)NetworkManager.singleton).HandleCreateRoomRequest(roomName, maxPlayers, mapName, connectionToClient);
    }

    // TargetRpc to receive the created room ID back from the server
    [TargetRpc]
    public void TargetRoomCreated(NetworkConnection target, int roomId)
    {
        Debug.Log($"Room created with ID: {roomId}");
        _roomId = roomId;
    }
}

using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.Transports.UTP;

public class ServerNetworkManager : NetworkManager
{
    private void OnEnable()
    {
        UnityTransport transport = gameObject.AddComponent<UnityTransport>();
        NetworkConfig.NetworkTransport = transport;


        OnServerStarted += HandleServerStarted;
        OnClientConnectedCallback += HandleClientConnected;
        OnClientDisconnectCallback += HandleClientDisconnected;

        StartServer();
    }

    private void HandleServerStarted()
    {
        Debug.Log("Server started");
    }

    private void HandleClientConnected(ulong clientId)
    {
        Debug.Log($"Client {clientId} connected");
    }

    private void HandleClientDisconnected(ulong clientId)
    {
        Debug.Log($"Client {clientId} disconnected");
    }
}
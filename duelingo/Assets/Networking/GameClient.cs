using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.Transports.UTP;

public class GameClient : NetworkManager
{
    void Start()
    {
        UnityTransport transport = gameObject.AddComponent<UnityTransport>();
        NetworkConfig.NetworkTransport = transport;

        StartClient();
        Debug.Log("Client started");
    }
}
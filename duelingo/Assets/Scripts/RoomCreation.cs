using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomCreation : MonoBehaviour
{
    [SerializeField] private TMP_InputField _roomNameInputField;
    [SerializeField] private TMP_InputField _roomMaxPlayerField;
    [SerializeField] public TMP_Dropdown _roomMapDropdown;

    public void CreateRoom()
    {
        var player = NetworkClient.connection.identity.GetComponent<PlayerRoomRequests>();

        string roomName = _roomNameInputField.text;
        int maxPlayers = Int32.Parse(_roomMaxPlayerField.text);
        string mapName = _roomMapDropdown.options[_roomMapDropdown.value].text;

        player.CmdRequestCreateRoom(roomName, maxPlayers, mapName);


        SceneManager.LoadSceneAsync("SampleScene");
    }

    //onInputChange from an input field
    public void OnNameChange()
    {

    }
}

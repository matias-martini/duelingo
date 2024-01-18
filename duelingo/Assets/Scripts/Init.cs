using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Init : MonoBehaviour
{
    [SerializeField] private GameObject _serverGo;

    public void Start()
    {
        if(SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null)
        {
            Console.WriteLine("Server Initialized");
            Instantiate(_serverGo);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Client Initialized");
            SceneManager.LoadSceneAsync("Menu");
            Destroy(gameObject);
        }
    }
}
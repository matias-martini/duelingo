using UnityEngine;

public class UIActivator : MonoBehaviour
{
    // This method is called when the object becomes enabled and active
    [SerializeField] private GameObject _spinner;
    [SerializeField] private GameObject _room;

    private void OnEnable()
    {
        _spinner.SetActive(true);
        _room.SetActive(false);
        Invoke("LoadRooms", 3);
    }

    private void LoadRooms()
    {
        _spinner.SetActive(false);
        _room.SetActive(true);
    }
}

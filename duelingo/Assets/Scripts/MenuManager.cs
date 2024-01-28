using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainLayout;
    [SerializeField] private GameObject _playLayout;
    [SerializeField] private GameObject _multiplayerServerSelectLayout;
    [SerializeField] private GameObject _multiplayerRoomCreationLayout;
    [SerializeField] private GameObject _multiplayerConnectingLayout;
    [SerializeField] private GameObject _multiplayerConnectedLayout;

    [SerializeField] private GameObject _multiplayerClient;

    public void Start()
    {
        SetAllLayoutsInactive();
        _mainLayout.SetActive(true);
    }

    public void OnPlayButtonClicked()
    {

        _multiplayerServerSelectLayout.SetActive(false);
        _multiplayerConnectingLayout.SetActive(false);
        _multiplayerConnectedLayout.SetActive(false);
        _multiplayerRoomCreationLayout.SetActive(false);
        _mainLayout.SetActive(true);
        _playLayout.SetActive(true);
    }

    public void OnPlaySingleplayerButtonClicked()
    {
        SetAllLayoutsInactive();
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void OnMultiplayerButtonClicked()
    {
        SetAllLayoutsInactive();
        _multiplayerServerSelectLayout.SetActive(true);
    }
    public void OnMultiplayerRoomCreationButtonClicked()
    {
        SetAllLayoutsInactive();
        _multiplayerRoomCreationLayout.SetActive(true);
    }

    public void OnMultiplayerServerSelectBackButtonClicked()
    {
        SetAllLayoutsInactive();
        _mainLayout.SetActive(true);
    }

    public void OnMultiplayerServerConnectButtonClicked()
    {
        TMP_InputField childInputField = _multiplayerServerSelectLayout.GetComponentInChildren<TMP_InputField>();
        Debug.Log("Found InputField with text: " + childInputField.text);

        SetAllLayoutsInactive();
        _multiplayerConnectingLayout.SetActive(true);

        // wait async 3 secs and then executes Instantiate(_multiplayerClient);
        Invoke("InstantiateMultiplayerClient", 3);
    }

    private void InstantiateMultiplayerClient()
    {
        Instantiate(_multiplayerClient);
        SetAllLayoutsInactive();
        _multiplayerConnectedLayout.SetActive(true);

    }

    private void SetAllLayoutsInactive()
    {
        _mainLayout.SetActive(false);
        _playLayout.SetActive(false);
        _multiplayerServerSelectLayout.SetActive(false);
        _multiplayerConnectingLayout.SetActive(false);
        _multiplayerConnectedLayout.SetActive(false);
        _multiplayerRoomCreationLayout.SetActive(false);
    }
}
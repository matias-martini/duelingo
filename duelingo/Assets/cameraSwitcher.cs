using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera1;
    public Camera secondaryCamera2;

    private int currentCameraIndex = 0;

    void Start()
    {
        // Activate the main camera by default
        SetActiveCamera(mainCamera);
    }

    void Update()
    {
        // Check for camera switch input
        if (Input.GetKeyDown(KeyCode.K))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // Deactivate the current camera
        SetActiveCamera(null);

        // Switch to the next camera
        currentCameraIndex = (currentCameraIndex + 1) % 3;

        // Activate the new current camera
        if (currentCameraIndex == 0)
        {
            SetActiveCamera(mainCamera);
        }
        else if (currentCameraIndex == 1)
        {
            SetActiveCamera(secondaryCamera1);
        }
        else if (currentCameraIndex == 2)
        {
            SetActiveCamera(secondaryCamera2);
        }
    }

    void SetActiveCamera(Camera newActiveCamera)
    {
        mainCamera.enabled = false;
        secondaryCamera1.enabled = false;
        secondaryCamera2.enabled = false;

        if (newActiveCamera != null)
        {
            newActiveCamera.enabled = true;
        }
    }
}

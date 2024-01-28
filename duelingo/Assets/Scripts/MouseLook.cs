using UnityEngine;

public class Camerathird : MonoBehaviour
{
    public float sensitivity = 2f;
    public Transform playerTransform;
    public float upview = 0f;
    public float downview = 0f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, downview, upview);

        rotationY += mouseX;

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        playerTransform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }

    void Update()
    {
        RotateCamera();
    }
}
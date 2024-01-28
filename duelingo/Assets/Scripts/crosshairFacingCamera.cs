using UnityEngine;

public class CrosshairFacingCamera : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
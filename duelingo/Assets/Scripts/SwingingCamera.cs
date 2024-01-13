using UnityEngine;

public class CameraSwing : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f; // The maximum extent of the swing
    [SerializeField] private float frequency = 1f; // How fast the swing happens

    private Vector3 initialPosition;
    private float timer;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float swing = amplitude * Mathf.Sin(frequency * timer);
        transform.position = initialPosition + new Vector3(swing, swing, 0); // Swings up and down
    }
}
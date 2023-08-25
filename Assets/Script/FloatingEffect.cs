using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float floatStrength = 0.1f; // Strength of the up-down floating effect
    public float floatSpeed = 1.0f;    // Speed of the up-down floating effect
    public float rotationSpeed = 40.0f; // Speed of the rotation effect

    private Vector3 initialPosition;
    private float groundOffset = 0.5f; // Offset from the ground. Adjust as needed.

    private void Start()
    {
        // Set the initial position to be just above the ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            initialPosition = hit.point + new Vector3(0, groundOffset, 0);
            transform.position = initialPosition;
        }
        else
        {
            initialPosition = transform.position;
        }
    }

    private void Update()
    {
        // Apply the up-down floating effect
        transform.position = initialPosition + new Vector3(0.0f, Mathf.Sin(Time.time * floatSpeed) * floatStrength, 0.0f);

        // Apply the rotation effect
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

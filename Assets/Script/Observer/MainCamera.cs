using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraView { LockPlayerPosition, FollowMouse }

public class MainCamera : SceneSingleton<MainCamera>
{
    public Transform player;             // The player's transform
    public Vector3 offset;
    public float playerHeight = 1.8f;
    float sensitivity = 2.0f;     // Mouse sensitivity
    public float rotationSpeed = 5.0f;   // Rotation speed
    public Vector2 pitchMinMax = new Vector2(-30, 60);  // Min/Max pitch angles

    private float currentYaw = 0.0f;     // Current yaw rotation
    private float currentPitch = 0.0f;   // Current pitch rotation

    public LayerMask cameraLayerMask;

    void Update()
    {
        // Handle mouse input for camera rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentYaw += mouseX * sensitivity;  // Rotate around Y-axis (left/right)
        currentPitch += mouseY * sensitivity; // Rotate around X-axis (up/down)

        // Clamp pitch to avoid flipping the camera
        currentPitch = Mathf.Clamp(currentPitch, pitchMinMax.x, pitchMinMax.y);
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the desired position of the camera
            Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);

            float xOffset = offset.x;
            Vector3 useOffset = new Vector3(0, offset.y, offset.z);

            // Apply the rotation and position to the camera
            transform.position = player.position + (Quaternion.Euler(xOffset, 0, 0) * Vector3.right) + (rotation * offset);

            Vector3 headPos = player.position + (Vector3.up * playerHeight);
            Vector3 cameraDir = transform.position - player.position + (Vector3.up * playerHeight);
            float cameraDis = Vector3.Distance(headPos, transform.position);

            transform.LookAt(headPos);  // Make the camera always look at the player

            if (Physics.Raycast(headPos, cameraDir, out RaycastHit hit, cameraDis, cameraLayerMask))
            {
                transform.position = hit.point;
            }
        }
    }
}
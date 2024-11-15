using UnityEngine;

public class FollowCam : MonoBehaviour
{

    [SerializeField] private Transform target;  // Reference to the player's transform
    [SerializeField] private float smoothSpeed = 0.125f;  // Smoothing factor for camera movement
    [SerializeField] Vector3 offset;  // Offset to maintain between the camera and player

    [SerializeField] float minX = 0f;
    [SerializeField] float maxX = 5f;


    [SerializeField] float minY = 0f;
    [SerializeField] float maxY = 5f;



    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position of the camera
            Vector3 desiredPosition = target.position + offset;

            // fix z
            desiredPosition.z = offset.z;

            // Use Vector3.Lerp to smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY);

            // Set the camera's position to the smoothed position
            transform.position = smoothedPosition;
        }
    }
}

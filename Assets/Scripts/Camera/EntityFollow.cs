using UnityEngine;

public class EntityFollow : MonoBehaviour
{
    public GameObject entity;
    public float deadZoneSize = 1f;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float minX = -10f;
    public float maxX = 10f;

    private Camera mainCamera;
    private Transform target;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        target = entity.transform;
    }

    void LateUpdate()
    {
        if (target == null || mainCamera == null) return;

        // Calculate the dead zone boundaries for X-axis only
        float deadZoneMinX = transform.position.x - deadZoneSize / 2f;
        float deadZoneMaxX = transform.position.x + deadZoneSize / 2f;

        // Check if the target is outside the horizontal dead zone
        Vector3 targetPosition = transform.position;
        bool shouldMove = false;

        if (target.position.x < deadZoneMinX)
        {
            targetPosition.x = target.position.x + deadZoneSize / 2f;
            shouldMove = true;
        }
        else if (target.position.x > deadZoneMaxX)
        {
            targetPosition.x = target.position.x - deadZoneSize / 2f;
            shouldMove = true;
        }

        // Apply offset and smooth movement
        if (shouldMove)
        {
            targetPosition += offset;
            targetPosition.z = transform.position.z; // Maintain camera's Z position

            // Clamp X within min and max limits
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
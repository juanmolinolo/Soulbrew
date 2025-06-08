using UnityEngine;

public class EntityFollow : MonoBehaviour
{
    public GameObject entity;
    public Vector2 deadZoneSize = new(1f, 1f);
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

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
        float deadZoneMinX = transform.position.x - deadZoneSize.x / 2f;
        float deadZoneMaxX = transform.position.x + deadZoneSize.x / 2f;

        // Check if the target is outside the horizontal dead zone
        Vector3 targetPosition = transform.position;
        bool shouldMove = false;

        if (target.position.x < deadZoneMinX)
        {
            targetPosition.x = target.position.x + deadZoneSize.x / 2f;
            shouldMove = true;
        }
        else if (target.position.x > deadZoneMaxX)
        {
            targetPosition.x = target.position.x - deadZoneSize.x / 2f;
            shouldMove = true;
        }

        // Apply offset and smooth movement
        if (shouldMove)
        {
            targetPosition += offset;
            targetPosition.z = transform.position.z; // Maintain camera's Z position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
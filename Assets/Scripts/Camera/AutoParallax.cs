using UnityEngine;

public class AutoParallax : MonoBehaviour
{
    [SerializeField]
    private float parallaxEffect = 0.5f;

    [SerializeField]
    private Camera mainCamera;

    private float spriteWidth;

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;
    }

    private void Update()
    {
        transform.position += parallaxEffect * Time.deltaTime * Vector3.left;

        float rightEdge = transform.position.x + spriteWidth / 2f;
        float cameraLeft = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;

        if (rightEdge < cameraLeft)
        {
            float cameraRight = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
            float newX = cameraRight + spriteWidth / 2f;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}

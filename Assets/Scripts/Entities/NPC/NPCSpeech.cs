using UnityEngine;

public class NPCSpeech : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject speechBubble;

    [SerializeField]
    private GameObject tooltip;

    [SerializeField]
    private float interactionRange = 3f;

    private SpriteRenderer spriteRenderer;
    private bool isSpeechVisible = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        Vector3 directionToPlayer = player.transform.position - transform.position;
        if (directionToPlayer.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (directionToPlayer.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (distance < interactionRange)
        {
            tooltip.SetActive(!isSpeechVisible);
            if (Input.GetKeyDown(KeyCode.E))
            {
                isSpeechVisible = true;
                tooltip.SetActive(false);
                speechBubble.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.R) && isSpeechVisible)
            {
                isSpeechVisible = false;
                speechBubble.SetActive(false);
            }
        }
        else
        {
            tooltip.SetActive(false);
            speechBubble.SetActive(false);
            isSpeechVisible = false;
        }
    }
}

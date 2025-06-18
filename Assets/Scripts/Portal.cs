using Assets.Scripts.Constants;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public GameObject fadeImage;
    public float fadeDuration = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConstants.PLAYER))
        {
            Debug.Log("Player has entered the portal.");
            Fade(collision.gameObject);
        }
    }

    private void Fade(GameObject player)
    {
        StartCoroutine(FadeRoutine(player));
    }

    private IEnumerator FadeRoutine(GameObject player)
    {
        Image image = fadeImage.GetComponent<Image>();
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();

        float elapsedTime = 0f;

        Color imageColor = image.color;
        Color playerColor = spriteRenderer.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            image.color = imageColor;
            playerColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = playerColor;
            yield return null;
        }

        imageColor.a = 1f;
        image.color = imageColor;

        playerColor.a = 0f;
        spriteRenderer.color = playerColor;
    }
}

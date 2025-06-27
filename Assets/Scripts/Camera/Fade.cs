using Assets.Scripts.Constants;
using Assets.Scripts.Enums;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeImage;

    [SerializeField]
    private float fadeDuration = 1.0f;

    [SerializeField]
    private SceneBuildIndex sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConstants.PLAYER))
        {
            FadeCamera(collision.gameObject);
        }
    }

    private void FadeCamera(GameObject player)
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
        SceneManager.LoadScene((int)sceneToLoad);
    }
}

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
        if (collision.gameObject.CompareTag(TagConstants.PLAYER))
        {
            FadeCamera();
        }
    }

    private void FadeCamera()
    {
        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        Image image = fadeImage.GetComponent<Image>();

        float elapsedTime = 0f;

        Color imageColor = image.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            image.color = imageColor;
            yield return null;
        }

        imageColor.a = 1f;
        image.color = imageColor;

        SceneManager.LoadScene((int)sceneToLoad);
    }
}

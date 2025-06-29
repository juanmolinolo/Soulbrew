using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Unfade : MonoBehaviour
{
    [SerializeField]
    private GameObject unfadeImage;

    [SerializeField]
    private float unfadeDuration = 1.0f;

    private void Start()
    {
        StartCoroutine(UnfadeRoutine());
    }

    private IEnumerator UnfadeRoutine()
    {
        Image image = unfadeImage.GetComponent<Image>();

        float elapsedTime = 0f;

        Color imageColor = image.color;

        while (elapsedTime < unfadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = Mathf.Lerp(1f, 0f, elapsedTime / unfadeDuration);
            image.color = imageColor;
            yield return null;
        }

        imageColor.a = 0f;
        image.color = imageColor;
    }
}

using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private GameObject credits;

    [SerializeField]
    private GameObject tutorial;

    [SerializeField]
    private GameObject settings;

    public void Play()
    {
        SceneManager.LoadScene((int)SceneBuildIndex.StartingTown);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        if (credits.activeSelf)
        {
            credits.SetActive(false);
            menu.SetActive(true);
        }
        else if (tutorial.activeSelf)
        {
            tutorial.SetActive(false);
            menu.SetActive(true);
        }
        else if (settings.activeSelf)
        {
            settings.SetActive(false);
            menu.SetActive(true);
        }
    }

    public void ShowCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void ShowTutorial()
    {
        menu.SetActive(false);
        tutorial.SetActive(true);
    }

    public void ShowSettings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }
}

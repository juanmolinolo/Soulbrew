using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pause;

    [SerializeField]
    private GameObject ui;

    [SerializeField]
    private GameObject death;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (death == null || !death.activeSelf))
        {
            if (pause.activeSelf)
            {
                HidePauseMenu();
            }
            else
            {
                ShowPauseMenu();
            }
        }
    }

    public void ShowPauseMenu()
    {
        pause.SetActive(true);
        ui.SetActive(false);
        Time.timeScale = 0f;
    }

    public void HidePauseMenu()
    {
        pause.SetActive(false);
        ui.SetActive(true);
        Time.timeScale = 1f;
    }

    public void ShowDeathMenu()
    {
        death.SetActive(true);
        ui.SetActive(false);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene((int)SceneBuildIndex.MainMenu);
    }
}

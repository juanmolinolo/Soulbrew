using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pause;

    [SerializeField]
    private GameObject ui;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private float healthAmount = 100f;

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float amount)
    {
        healthAmount += amount;
        if (healthAmount > 100f) healthAmount = 100f;
        healthBar.fillAmount = healthAmount / 100f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene((int)SceneBuildIndex.MainMenu);
    }
}

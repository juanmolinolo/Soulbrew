using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

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
            if (pauseMenu.activeSelf)
            {
                HidePauseMenu();
            }
            else
            {
                ShowPauseMenu();
            }
        }
    }

    private void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}

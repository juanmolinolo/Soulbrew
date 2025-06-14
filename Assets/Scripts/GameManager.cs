using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

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
}

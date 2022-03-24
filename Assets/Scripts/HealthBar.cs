using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float changeSpeed = 2f;

    private float target = 1f;
    Color healthColor;

    void Update()
    {
        healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, target, changeSpeed * Time.deltaTime);
        healthBar.color = healthColor;
    }

    public void UpdateHealthBar (float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
        healthColor = Color.Lerp(Color.red, Color.green, (currentHealth/maxHealth));
    }
}

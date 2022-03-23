using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image playerHealthBar;
    [SerializeField] private float changeSpeed = 2f;

    private float target = 1f;
    Color healthColor;

    void Update()
    {
        playerHealthBar.fillAmount = Mathf.MoveTowards(playerHealthBar.fillAmount, target, changeSpeed * Time.deltaTime);
        playerHealthBar.color = healthColor;
    }

    public void UpdateHealthBar (float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
        healthColor = Color.Lerp(Color.red, Color.green, (currentHealth/maxHealth));
    }
}

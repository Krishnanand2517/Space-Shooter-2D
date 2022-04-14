using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int score = 10;
    public float enemyMaxHealth = 2f;
    public float enemyHealth;
    public float startShootingAfter = 1f;
    public int maxBulletsInARow = 3;
    public bool isActive;

    [SerializeField] private float enemySpeed = 3;
    [SerializeField] private HealthBar enemyHealthBarScript;

    private GameObject player;
    private Rigidbody2D enemyRb;
    private float boundX = 11f;
    private float healthBarDisplayTime = 2.5f;
    private float healthBarDisplayTimer;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = enemyMaxHealth;
        healthBarDisplayTimer = 0f;
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody2D>();
        InvokeRepeating("MoveEnemy", 0.5f, 1.5f);
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarDisplayTimer -= Time.deltaTime;

        if (transform.position.y - player.transform.position.y < 10){
            isActive = true;
        }
        
        if (enemyHealth <= 0){
            Destroy(gameObject);
            GameManager.instance.playerScore += score;
            GameManager.instance.enemiesDestroyed += 1;
        }

        if (healthBarDisplayTimer <= 0){
            healthBarDisplayTimer = 0f;
            enemyHealthBarScript.HideHealthBar();
        }
    }

    void MoveEnemy()
    {
        if (!isActive){
            return;
        }

        float randomX = Random.Range(-boundX, boundX);
        float randomY = Random.Range(player.transform.position.y - 1.5f, player.transform.position.y + 7.5f);
        Vector2 randomPos = new Vector2(randomX, randomY);

        Vector2 moveTo = (randomPos - new Vector2(transform.position.x, transform.position.y)).normalized;
        moveTo *= enemySpeed;
        enemyRb.velocity = moveTo;
    }

    public void DamageEnemy(float damage)
    {
        enemyHealth -= damage;
        healthBarDisplayTimer = healthBarDisplayTime;
        enemyHealthBarScript.UpdateHealthBar(enemyMaxHealth, enemyHealth);
    }
}

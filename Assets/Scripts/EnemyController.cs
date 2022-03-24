using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemyMaxHealth = 2f;
    public float enemyHealth;
    public float startShootingAfter = 1f;
    public int maxBulletsInARow = 3;

    [SerializeField] private float enemySpeed = 3;
    [SerializeField] private HealthBar enemyHealthBarScript;

    GameObject player;
    Rigidbody2D enemyRb;
    float boundX = 11f;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = enemyMaxHealth;
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody2D>();
        InvokeRepeating("MoveEnemy", 0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0){
            Destroy(gameObject);
        }
    }

    void MoveEnemy()
    {
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
        enemyHealthBarScript.UpdateHealthBar(enemyMaxHealth, enemyHealth);
    }
}

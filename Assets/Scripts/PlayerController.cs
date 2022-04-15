using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    float fireInput;
    float boundX = 10.7f;
    float boundDown = -4.8f;
    float maxHealth = 10f;
    float health;

    [SerializeField] float speed;
    [SerializeField] HealthBar playerHealthBarScript;
    private Meteors meteorScript;
    private EnemyController enemyScript;
    Rigidbody2D playerRb;
    ObjectPool objectPool;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        meteorScript = GameManager.instance.GetComponent<Meteors>();
        objectPool = ObjectPool.Instance;
        health = maxHealth;

        playerHealthBarScript.UpdateHealthBar(maxHealth, health);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameOver == true){
            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        if (health <= 0){
            GameManager.instance.gameOver = true;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.instance.gameOver){
            return;
        }

        float newPosX = playerRb.position.x + (horizontalInput * speed * Time.fixedDeltaTime);
        newPosX = Mathf.Clamp(newPosX, -boundX, boundX);
        float newPosY = playerRb.position.y + (verticalInput * speed * Time.fixedDeltaTime);
        newPosY = Mathf.Clamp(newPosY, boundDown, 1000f);

        Vector2 newPos = new Vector2(newPosX, newPosY);
        playerRb.MovePosition(newPos);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Meteor"){
            health = Mathf.Clamp(health - 1f, 0, maxHealth);
            Debug.Log(health + "/" + maxHealth);
            meteorScript.DestroyMeteor(other.gameObject);
        }

        else if (other.tag == "Enemy"){
            health = Mathf.Clamp(health - 1f, 0, maxHealth);
            Debug.Log(health + "/" + maxHealth);
            enemyScript = other.GetComponent<EnemyController>();
            enemyScript.DamageEnemy(1f);
        }

        else if (other.tag == "EnemyBullet"){
            health = Mathf.Clamp(health - 1f, 0, maxHealth);
            Debug.Log(health + "/" + maxHealth);
            other.gameObject.SetActive(false);
        }

        else if (other.tag == "LevelEnd"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        playerHealthBarScript.UpdateHealthBar(maxHealth, health);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D bulletRb;
    private GameObject player;
    private EnemyController enemyController;
    private float speed = 10f;

    void OnEnable()
    {
        bulletRb.velocity = Vector2.up * speed;
    }

    // Start is called before the first frame update
    void Awake()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletRb.position.y > (player.transform.position.y + 25f)){
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Meteor"){
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }

        else if (other.tag == "Enemy"){
            enemyController = other.GetComponent<EnemyController>();
            enemyController.DamageEnemy(1f);
            gameObject.SetActive(false);
        }
    }
}

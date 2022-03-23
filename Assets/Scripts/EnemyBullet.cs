using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D bulletRb;
    GameObject player;
    float speed = 8f;

    void OnEnable()
    {
        bulletRb.velocity = Vector2.down * speed;
    }

    void Awake()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletRb.position.y < (player.transform.position.y - 25f)){
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteors : MonoBehaviour
{
    public GameObject[] meteors;
    public GameObject player;

    [SerializeField] private int meteorScore = 5;
    private GameObject meteorObject;
    private Rigidbody2D meteorRb;
    private int meteorNum;
    private float posX;
    private float forceX;
    private float forceY;
    private float torque;

    float spawnTime = 3f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameOver){
            return;
        }

        timer -= Time.deltaTime;
        if (timer <= 0){
            SpawnMeteor();
            timer = spawnTime;
        }
    }

    void SpawnMeteor()
    {
        meteorNum = Random.Range(0, 4);
        posX = Random.Range(-15f, 15f);
        forceX = Random.Range(-100f, 100f);
        forceY = Random.Range(50f, 100f);
        torque = Random.Range(-100f, 100f);

        Vector2 meteorPos = new Vector2(posX, player.transform.position.y + 12f);
        Vector2 meteorForce = new Vector2(forceX, forceY);
        meteorObject = Instantiate(meteors[meteorNum], meteorPos, Quaternion.identity);

        meteorRb = meteorObject.GetComponent<Rigidbody2D>();
        meteorRb.AddTorque(torque);
        meteorRb.AddForce(meteorForce);
    }

    public void DestroyMeteor (GameObject meteor)
    {
        Destroy(meteor);
        GameManager.instance.playerScore += meteorScore;
    }
}

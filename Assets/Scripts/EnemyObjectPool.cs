using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject objectPrefab;
        public int size;
    }

    #region Singleton
    public static EnemyObjectPool Instance;
    
    void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private EnemyController enemyController;
    private float bulletDelay = 0.1f;
    private float timer;
    private float startShootingAfter;
    private float shootTimer;
    private int maxBullets;
    private int bullets = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = bulletDelay;
        enemyController = GetComponent<EnemyController>();
        startShootingAfter = enemyController.startShootingAfter;
        maxBullets = enemyController.maxBulletsInARow;

        shootTimer = startShootingAfter;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools){
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++){
                GameObject obj = Instantiate(pool.objectPrefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameOver){
            return;
        }
        
        timer -= Time.deltaTime;
        shootTimer -= Time.deltaTime;

        if (bullets <= 0){
            bullets = Random.Range(0, maxBullets+1);
        }

        if (shootTimer <= 0 && timer <= 0){
            SpawnFromPool("EnemyBullet1", transform.position, Quaternion.identity);
            timer = bulletDelay;
            bullets--;
            if (bullets <= 0){
                shootTimer = Random.Range(0.11f, 4f);
            }
        }
    }

    public void SpawnFromPool(string tag, Vector2 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag)){
            return;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);
    }
}

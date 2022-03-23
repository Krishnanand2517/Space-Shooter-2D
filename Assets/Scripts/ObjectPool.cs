using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject objectPrefab;
        public int size;
    }

    #region Singleton
    public static ObjectPool Instance;
    
    void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private float bulletDelay = 0.1f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = bulletDelay;
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

        if (Input.GetKey(KeyCode.Space) && timer <= 0){
            SpawnFromPool("Bullet1", transform.position, Quaternion.identity);
            timer = bulletDelay;
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

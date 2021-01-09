using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    
    #region Singleton
    public static WaveManager instance;


    void Awake()
    {
        instance = this;
    }

    #endregion


    public int wave = -1;
    public int killCount;
    public float timeBetweeenSpawn;

    public float downTime;
    public int enemyStartAmount;
    public int enemyAmountIncrease;

    public int enemyCount;
    bool spawning = true;

    public GameObject enemyPrefab;
    public GameObject WaveComplete;

    public List<GameObject> spawner = new List<GameObject>();
    public List<GameObject> enemy = new List<GameObject>();

    float spawnCountdown;

    
    void Start()
    {
        foreach(Transform child in transform)
        {
            spawner.Add(child.gameObject);
        }
    }
    
    void Update()
    {
        if (enemy.Count == 0 && enemyCount == 0)
        {
            GetComponent<SaveAndLoadMain>().SaveGame();

            spawning = false;
            wave++;
            SpawnSetup();
        }

        else if (spawning == true && enemyCount > 0)
        {
            spawnCountdown -= Time.deltaTime;

            if(spawnCountdown <= 0)
            {
                Spawn();
                spawnCountdown = timeBetweeenSpawn;
            }
        }
    }

    void SpawnSetup()
    {
        enemyCount = enemyStartAmount + enemyAmountIncrease * wave;

        if (wave != 0)
        {
            WaveComplete.SetActive(true);
            Invoke("StartSpawning", downTime);
        }
        else
            StartSpawning();

    }

    void StartSpawning()
    {
        spawning = true;
        WaveComplete.SetActive(false);
    }

    void Spawn()
    {
        enemyCount--;

        int index = Random.Range(0, spawner.Count);

        GameObject GO = Instantiate(enemyPrefab, spawner[index].transform.position, Quaternion.identity);
        enemy.Add(GO);
    }

    public void Kill(GameObject kill)
    {
        enemy.Remove(kill);
        killCount++;
    }
  
}

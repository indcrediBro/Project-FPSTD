using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : Singleton<EnemyManager>
{
    public List<GameObject> Enemies;
    public List<GameObject> ActiveEnemies;
    [SerializeField] public delegate void OnIncreaseRoundDelegate();
    [SerializeField] public event OnIncreaseRoundDelegate onIncreaseRoundEvent;
    [SerializeField] private Transform[] _enemySpawnPoint;
    [SerializeField] private GameObject _tempEnemy;
    [SerializeField] private int activeEnemyThreshold; //No of max active enemies at any given time
    [SerializeField] private int _enemyMaxPool; //No of max active/inactive enemies in scene
    [SerializeField] private int roundEnemyTotal; //No of enemies in round
    [SerializeField] private int increaseEnemyTotal; //No of enemies to add to the total of round
    private int roundSpawned; //No of enemies spawned during that round
    private int enemySpawnQueue; //No of enemies waiting to be spawned by the timer. 
    [SerializeField] private float enemySpawnInterval; //Interval between enemy spawns
    private float enemySpawnTimer; //enemy spawn count down timer


    void Start()
    {
        ActiveEnemies = new List<GameObject>();
        CreateEnemyPool();
        onIncreaseRoundEvent += IncreaseRound;
        UpdateAcviteEnemies();
        enemySpawnTimer = enemySpawnInterval;
    }

    void Update()
    {
        enemySpawnTimer -= Time.deltaTime;

        if (enemySpawnTimer <= 0 && enemySpawnQueue > 0)
        {
            //we only spawn enemies when there is more than 1 in queue
            SpawnEnemy();

            enemySpawnTimer = enemySpawnInterval;
        }
    }

    private void CreateEnemyPool()
    {
        Enemies = new List<GameObject>();

        for (int i = 0; i < _enemyMaxPool; i++)
        {
            GameObject _enemy = Instantiate(_tempEnemy);
            Enemies.Add(_enemy);
            _enemy.SetActive(false);
        }
    }

    private GameObject GetEnemyInPool()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (!Enemies[i].activeInHierarchy)
            {
                ActiveEnemies.Add(Enemies[i]);
                return Enemies[i];
            }
        }
        return null;
    }

    public void SpawnEnemy()
    {
        GameObject _enemy = GetEnemyInPool();
        if (_enemy != null)
        {
            _enemy.transform.position = GetAvailableSpawnPoint().position;
            _enemy.SetActive(true);

            enemySpawnQueue--;
            roundSpawned++;
        }
    }

    public void UpdateAcviteEnemies()
    {
        if (roundSpawned >= roundEnemyTotal && ActiveEnemies.Count <= 0 && enemySpawnQueue <= 0)
        {
            //round has ended, broadcast event
            onIncreaseRoundEvent?.Invoke();
        }
        else
        {
            while (roundSpawned + enemySpawnQueue < roundEnemyTotal && (enemySpawnQueue < activeEnemyThreshold - ActiveEnemies.Count))
            {
                enemySpawnQueue++;
            }
        }
    }

    private Transform GetAvailableSpawnPoint()
    {
        int _randomSpawnPointIndex = Random.Range(0, _enemySpawnPoint.Length);
        return _enemySpawnPoint[_randomSpawnPointIndex];
    }

    //currently not really keeping track of rounds...
    private void IncreaseRound()
    {
        //TODO why is this not resetting?
        roundSpawned = 0;
        roundEnemyTotal += increaseEnemyTotal;
    }


    private void OnDisable()
    {
        onIncreaseRoundEvent -= IncreaseRound;
    }
}

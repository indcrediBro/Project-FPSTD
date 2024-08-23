using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyType
{
    public EnemyManager.EnemyNames m_EnemyName;
    public string m_PoolName;
    public int m_SpawnWeight;
}

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private List<EnemyType> m_enemyTypes;
    [SerializeField] private List<Transform> m_spawnPoints;
    [SerializeField] private float m_checkInterval = 1f;
    [SerializeField] private int m_initialEnemies = 3;
    [SerializeField] private bool m_autoWaveStart = true;

    private bool m_isWaveActive;
    private int m_currentWave;
    public int GetCurrentWave() { return m_currentWave; }

    private void Start()
    {
        if (m_autoWaveStart) StartWave();
    }

    public void StartWave()
    {
        if (m_isWaveActive) return;
        m_isWaveActive = true;
        m_currentWave++;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        int enemiesToSpawn = m_initialEnemies + m_currentWave;
        EnemyManager.Instance.ResetEnemyCounter(enemiesToSpawn);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            EnemyType enemyType = GetRandomEnemyType();
            Transform spawnPoint = m_spawnPoints[RandomNumber.Instance.NextInt(m_spawnPoints.Count)];

            GameObject enemyObject = ObjectPoolManager.Instance.GetPooledObject(enemyType.m_PoolName);
            enemyObject.transform.position = spawnPoint.position;
            enemyObject.SetActive(true);

            EnemyManager.Instance.AddEnemyToList(enemyObject, enemyType.m_EnemyName);
            
            yield return new WaitForSeconds(RandomNumber.Instance.NextFloat(0.5f, 2f));
        }
        StartCoroutine(CheckWaveCompletion());
    }

    private EnemyType GetRandomEnemyType()
    {
        int totalWeight = 0;

        foreach (EnemyType enemyType in m_enemyTypes)
        {
            totalWeight += enemyType.m_SpawnWeight;
        }

        int randomValue = RandomNumber.Instance.NextInt(totalWeight);
        foreach (EnemyType enemyType in m_enemyTypes)
        {
            if (randomValue < enemyType.m_SpawnWeight)
            {
                return enemyType;
            }
            randomValue -= enemyType.m_SpawnWeight;
        }

        return m_enemyTypes[0];
    }

    private IEnumerator CheckWaveCompletion()
    {
        while (m_isWaveActive)
        {
            yield return new WaitForSeconds(m_checkInterval);

            if (EnemyManager.Instance.AreAllEnemiesDefeated())
            {
                WaveCompleted();
                yield break;
            }
        }
    }

    private void WaveCompleted()
    {
        m_isWaveActive = false;
        UpgradeEnemies();
        if (m_autoWaveStart) StartWave();
    }

    public void UpgradeEnemies()
    {
        foreach (EnemyType enemyType in m_enemyTypes)
        {
            List<GameObject> enemies = EnemyManager.Instance.GetActiveEnemyCollection(enemyType.m_EnemyName).GetEnemyList();
            foreach (GameObject enemyObject in enemies)
            {
                EnemyStats enemy = enemyObject.GetComponent<EnemyStats>();
                if (enemy != null)
                {
                    enemy.UpgradeStats(m_currentWave);
                }
            }
        }
    }
}

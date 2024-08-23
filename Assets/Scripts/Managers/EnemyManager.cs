using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
#region
    public enum EnemyNames
    {
        MINION,
        GHOUL,
    }

    [Serializable]
    public class ActiveEnemyCollection
    {
        [SerializeField] private EnemyNames m_enemyName;
        private List<GameObject> m_enemyList = new List<GameObject>();

        public EnemyNames GetEnemyName() { return m_enemyName; }
        public List<GameObject> GetEnemyList() { return m_enemyList; }
        public void AddEnemyToList(GameObject _enemy) { m_enemyList.Add(_enemy); }
        public void RemoveEnemyToList(GameObject _enemy) { m_enemyList.Remove(_enemy); }
    }
#endregion

    [SerializeField] private ActiveEnemyCollection[] m_activeEnemyCollections;
    public int m_TotalActiveEnemies;
    private int m_enemiesLeft;

    public void AddEnemyToList(GameObject _enemy, EnemyNames _enemyName)
    {
        ActiveEnemyCollection activeEnemyCollection = GetActiveEnemyCollection(_enemyName);
        activeEnemyCollection.AddEnemyToList(_enemy);
    }

    public void RemoveEnemyFromList(GameObject _enemy, EnemyNames _enemyName)
    {
        ActiveEnemyCollection activeEnemyCollection = GetActiveEnemyCollection(_enemyName);
        activeEnemyCollection.RemoveEnemyToList(_enemy);
    }

    public ActiveEnemyCollection GetActiveEnemyCollection(EnemyNames _enemyName)
    {
        ActiveEnemyCollection activeEnemyCollection =
            m_activeEnemyCollections.FirstOrDefault(collection => collection.GetEnemyName() == _enemyName);
        if (activeEnemyCollection == null)
        {
            throw new Exception("List with enemy name " + _enemyName + " does not exist!");
        }
        return activeEnemyCollection;
    }

    public void ResetEnemyCounter(int _enemyCount)
    {
        m_TotalActiveEnemies = _enemyCount;
        m_enemiesLeft = m_TotalActiveEnemies;
    }

    public void ReduceActiveEnemyCount(int _valueToReduce)
    {
        m_enemiesLeft -= _valueToReduce;
    }

    public bool AreAllEnemiesDefeated()
    {
        return m_enemiesLeft <= 0;

        //foreach (ActiveEnemyCollection collection in m_activeEnemyCollections)
        //{
        //    if (collection.GetEnemyList().Any(enemy => enemy.activeInHierarchy))
        //    {
        //        return false;
        //    }
        //}
        //return true;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public enum EnemyNames
    {
        MINION,
        GHOUL,
    }

    [Serializable]
    public class ActiveEnemyCollection
    {
        [SerializeField] private EnemyNames m_enemyName;
        // [SerializeField] private string m_enemyName;
        private List<GameObject> m_enemyList = new List<GameObject>();

        public EnemyNames GetEnemyName()
        {
            return m_enemyName;
        }

        public List<GameObject> GetEnemyList()
        {
            return m_enemyList;
        }

        public void AddEnemyToList(GameObject _enemy)
        {
            m_enemyList.Add(_enemy);
        }

        public void RemoveEnemyToList(GameObject _enemy)
        {
            m_enemyList.Remove(_enemy);
        }
    }

    [SerializeField] private ActiveEnemyCollection[] m_activeEnemyCollections;

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
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRandomiser : MonoBehaviour
{
    [SerializeField] private GameObject[] m_baseObjectsGO;
    [SerializeField] private GameObject[] m_decorationsGO;

    [SerializeField] private Transform m_baseParentTF;

    [SerializeField] private float m_decorSpawnLocationRange;

    private void OnEnable()
    {
        SpawnRandomFloorTile();
        RandomiseDecorations();
    }

    private void SpawnRandomFloorTile()
    {
        int randomIndex = RandomNumber.Instance.NextInt(m_baseObjectsGO.Length);

        for (int i = 0; i < m_baseObjectsGO.Length; i++)
        {
            m_baseObjectsGO[i].SetActive(false);
        }
        m_baseObjectsGO[randomIndex].SetActive(true);

        m_baseParentTF.localRotation = Quaternion.Euler(0f, 90f * RandomNumber.Instance.NextInt(4), 0f);
    }

    private void RandomiseDecorations()
    {
        int randomSpawnIndex = RandomNumber.Instance.NextInt(m_decorationsGO.Length + 50);

        foreach (GameObject _decor in m_decorationsGO)
        {
            _decor.SetActive(false);
        }

        if (randomSpawnIndex < m_decorationsGO.Length)
        {
            GameObject decor = m_decorationsGO[randomSpawnIndex];
            decor.SetActive(true);
            if (decor != m_decorationsGO[0])
            {
                decor.transform.localPosition = new Vector3(
                                                RandomNumber.Instance.NextFloat(-m_decorSpawnLocationRange, m_decorSpawnLocationRange),
                                                decor.transform.localPosition.y,
                                                RandomNumber.Instance.NextFloat(-m_decorSpawnLocationRange, m_decorSpawnLocationRange)
                                                );
                decor.transform.localRotation = Quaternion.Euler(0f, RandomNumber.Instance.NextFloat(0f, 360f), 0f);
                decor.transform.localScale *= RandomNumber.Instance.NextFloat(0.5f, 1.25f);
            }
        }
    }
}

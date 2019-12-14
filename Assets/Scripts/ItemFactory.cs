using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField]
    private SpawnController[] m_spawnList;

    [SerializeField]
    private ItemController m_addPrefab;

    [SerializeField]
    private ItemController m_minusPrefab;

    void Start()
    {
        StartCoroutine(AssignSpawnTask());
    }

    private IEnumerator AssignSpawnTask()
    {
        yield return new WaitForSeconds(GetRandomTime());

        var item = Instantiate((Random.Range(0, 2) == 1) ? m_addPrefab : m_minusPrefab);
        m_spawnList[Random.Range(0, m_spawnList.Length)].GenerateItem(item);
        StartCoroutine(AssignSpawnTask());
    }

    float GetRandomTime()
    {
        return Random.Range(0.5f, 3.0f);
    }
}
using System.Collections;
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
        if (item.type == ItemType.Minus)
        {
            item.OnInvoke = HandleOnMinus;
        }
        else
        {
            item.OnInvoke = HandleOnAdd;
        }
        item.OnFail = HandleOnFail;
        m_spawnList[Random.Range(0, m_spawnList.Length)].GenerateItem(item);
        StartCoroutine(AssignSpawnTask());
    }

    private float GetRandomTime()
    {
        return Random.Range(0.5f, 3.0f);
    }

    private void HandleOnAdd()
    {
        GameManager.I.SetSpeed(1000);
    }

    private void HandleOnMinus()
    {
        GameManager.I.SetSpeed(-1000);
    }

    private void HandleOnFail()
    {
        GameManager.I.FaildCount += 1;
    }
}
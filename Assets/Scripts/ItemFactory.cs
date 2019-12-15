using System.Collections;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField]
    private SpawnController[] m_spawnList;

    [SerializeField]
    private ItemController[] m_itemPrefabs;

    void Start()
    {
        StartCoroutine(AssignSpawnTask());
    }

    private IEnumerator AssignSpawnTask()
    {
        yield return new WaitForSeconds(GetRandomTime());

        var item = Instantiate(m_itemPrefabs[(Random.Range(0, 3))]);
        if (item.type == ItemType.Minus)
        {
            item.OnInvoke = HandleOnMinus;
        }
        else if(item.type == ItemType.Add)
        {
            item.OnInvoke = HandleOnAdd;
        }
        else if(item.type == ItemType.Block)
        {
            item.OnFail = HandleOnFail;
        }
        m_spawnList[Random.Range(0, m_spawnList.Length)].GenerateItem(item);
        StartCoroutine(AssignSpawnTask());
    }

    private float GetRandomTime()
    {
        return Random.Range(0.5f, 2.0f);
    }

    private void HandleOnAdd()
    {
        GameManager.I.SetSpeed(2000);
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
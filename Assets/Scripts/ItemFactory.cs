using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemFactory : MonoBehaviour
{
    [SerializeField]
    private SpawnController[] m_spawnList;

    [SerializeField]
    private ItemController[] m_itemPrefabs;
    
    [SerializeField]
    private AudioSource carRush;
    
    [SerializeField]
    private AudioSource carBrake;
    
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
            item.OnInvoke = HandleOnFail;
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
        carRush.Play();
    }

    private void HandleOnMinus()
    {
        GameManager.I.SetSpeed(-1000);
        carBrake.Play();
    }

    private void HandleOnFail()
    {
        StopAllCoroutines();
        GameManager.I.DisplayLose(true);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public void GenerateItem(ItemController item)
    {
        item.transform.position = transform.position;
        item.Invoke();
    }
}

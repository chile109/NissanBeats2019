using System;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Range(40, 80)]
    public int Speed = 40;

    public bool isActive = false;
    private Transform m_target;

    private void Start()
    {
        m_target = GetComponent<Transform>();
    }

    public void Invoke()
    {
        isActive = true;
    }

    void Update()
    {
        if (isActive)
            m_target.Translate(0, 0, Time.deltaTime * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "DeadZone")
        {
            Destroy(this);
        }
        
    }
}
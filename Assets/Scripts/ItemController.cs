using System;
using UnityEngine;

public enum ItemType
{
    Add, 
    Minus,
    Block,
}
public class ItemController : MonoBehaviour
{
    [Range(40, 80)]
    public int Speed = 40;
    public bool isActive = false;
    public ItemType type;
    public Action OnInvoke = null;

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
        if (isActive && !GameManager.I.IsOver)
            m_target.Translate(0, 0, Time.deltaTime * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "Player":
                OnInvoke?.Invoke();
                break;
        }

        Destroy(this.gameObject);
        
    }
}
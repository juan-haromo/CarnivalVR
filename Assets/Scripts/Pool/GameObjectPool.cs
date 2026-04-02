using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private int initialPoolSize = 3;
    [SerializeField] private PooledObject interactablePooledObject;
    private Stack<PooledObject> stack;

    void Awake()
    {
        SetUpPool();
    }

    private void SetUpPool()
    {
        stack = new Stack<PooledObject>(initialPoolSize);
        PooledObject instance;
        for (int i = 0; i < initialPoolSize; i++)
        {
            instance = CreateNewPooledObject();
            stack.Push(instance);
        }
    }

    private PooledObject CreateNewPooledObject()
    {
        PooledObject instance = Instantiate(interactablePooledObject).GetComponent<PooledObject>();
        instance.gameObject.SetActive(false);
        instance.Pool = this;
        return instance;
    }

    public PooledObject GetPooledObject()
    {
        if (stack.Count > 0)
        {
            return stack.Pop();
        }
        else
        {
            return CreateNewPooledObject();
        }
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        stack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}
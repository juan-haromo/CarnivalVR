using System.Collections.Generic;
using UnityEngine;

public class MultiObjectPool : GameObjectPool
{
    [SerializeField] private List<PooledObject> pooledObjectPrefabs;
    protected override PooledObject CreateNewPooledObject()
    {
        int randomIndex = Random.Range(0, pooledObjectPrefabs.Count);
        PooledObject instance = Instantiate(pooledObjectPrefabs[randomIndex]).GetComponent<PooledObject>();
        instance.gameObject.SetActive(false);
        instance.Pool = this;
        return instance;
    }
}

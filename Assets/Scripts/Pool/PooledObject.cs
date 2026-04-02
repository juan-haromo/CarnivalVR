using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public GameObjectPool Pool { get; set; }
    public void ReturnToPool(){Pool.ReturnToPool(this);}   
}
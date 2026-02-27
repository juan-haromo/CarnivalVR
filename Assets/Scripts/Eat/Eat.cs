using UnityEngine;

public class Eat : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IEatable>(out IEatable eatable))
        {
            eatable.Eat(transform);
        }
    }
}
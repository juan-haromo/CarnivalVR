using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] Rigidbody rb;

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject,lifeTime);
        rb.isKinematic = true;
        rb.detectCollisions =false;
    }
}
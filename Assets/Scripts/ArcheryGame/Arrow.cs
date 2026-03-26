using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform arrowMehs;
    bool isUpdating = true;

    void Update()
    {
        if(!isUpdating){return;}
        arrowMehs.forward = rb.linearVelocity.normalized;   
    }
    void OnTriggerEnter(Collider other)
    {
        isUpdating = false;
        Destroy(gameObject,lifeTime);
        rb.isKinematic = true;
        rb.detectCollisions =false;
    }
}
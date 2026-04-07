using UnityEngine;

public class RestrictedArea : MonoBehaviour
{
    [SerializeField] private Transform exitPosition;

    void OnDrawGizmosSelected()
    {
        if(exitPosition != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, exitPosition.position);
            Gizmos.DrawWireSphere(exitPosition.position, 1f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IRestrictedObject restrictedObject))
        {
            restrictedObject.OnEnterRestrictedArea(exitPosition);
        }
    }
}

using UnityEngine;

public class RestrictedRB : MonoBehaviour, IRestrictedObject
{
    [SerializeField] private Rigidbody rb;
    public void OnEnterRestrictedArea(Transform exitPosition)
    {
        gameObject.SetActive(false);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = exitPosition.position;
        gameObject.SetActive(true);
    }
}
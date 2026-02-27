using System.Data;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] int points = 10;
    [SerializeField] Rigidbody rb;

    Transform startPoint;
    StickScore score;

    public void Initialize(Transform _startPoint, StickScore _score)
    {
        startPoint = _startPoint;
        score = _score;
        ReturnToStart();
        gameObject.SetActive(true);
    }    
    
    public void Release()
    {
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = true;
        rb.detectCollisions = true;
    }

    public void ReturnToStart()
    {
        rb.isKinematic = false;
        rb.detectCollisions = false;
        rb.useGravity = false;
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        transform.SetPositionAndRotation(startPoint.position,startPoint.rotation);
        gameObject.SetActive(false);
    }


    public void AddPoints()
    {
        score.AddScore(points);
        ReturnToStart();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ReturnToStart();
        }
    }


}

using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionDetector : MonoBehaviour
{
    [SerializeField, Range(0,0.5f)]
    float detectionDelay = 0.05f;
    [SerializeField]
    float detectionDistance = 0.2f;
    [SerializeField]
    LayerMask detectionLayers;

    public List<RaycastHit> DetectedColliderHits {get; private set;}

    float nextCheck = 0;

    private List<RaycastHit> PreformDetection(Vector3 startPosition, float distance, LayerMask mask)
    {
        List<RaycastHit> detectedHits = new List<RaycastHit>();
        List<Vector3> directions = new List<Vector3>(){transform.forward,-transform.forward,transform.right,-transform.right};

        RaycastHit hit;
        foreach(Vector3 dir in directions)
        {
            if(Physics.Raycast(startPosition,dir,out hit, distance, mask))
            {
                detectedHits.Add(hit);
            }
        }
        return detectedHits;
    }

    void Start()
    {
        DetectedColliderHits = PreformDetection(transform.position,detectionDistance,detectionLayers);
    }

    void Update()
    {
        if(nextCheck <= Time.time)
        {
            nextCheck = Time.time + detectionDelay;
            DetectedColliderHits = PreformDetection(transform.position,detectionDistance,detectionLayers);
        }
    }

    void OnDrawGizmos()
    {
        if(Application.isPlaying == false){return;}

        Color c = 0 < DetectedColliderHits.Count? Color.red : Color.green;
        c.a = 0.5f;
        Gizmos.color = c;
        Gizmos.DrawWireSphere(transform.position,detectionDistance);

        List<Vector3> directions = new List<Vector3>(){transform.forward,transform.right,-transform.right};
        Gizmos.color = Color.magenta;
        foreach(Vector3 dir in directions)
        {
            Gizmos.DrawRay(transform.position,dir * detectionDistance);
        }
    }
}

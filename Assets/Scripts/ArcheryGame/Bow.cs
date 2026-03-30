using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : MonoBehaviour
{
    [Header("Grab Points")]
    [SerializeField] Transform bowGrabPoint;
    [SerializeField] Transform boneString;

    [Header("Arrow")]
    [SerializeField] GameObject arrowMesh;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float baseSpeed;
    public bool isUpdatingArrow;

    [SerializeField] BallonGame ballonGame;
    
    int grabCount = 0;

    void Start()
    {
        arrowMesh.SetActive(false);
    }

    void Update()
    {
        if (isUpdatingArrow){ UpdateArrow(); }  
    }

    public void StartShoot(SelectEnterEventArgs args)
    {
        //Event is called when grabbing object, Only activate it when grabbed by two hands
        grabCount++;
        if(grabCount == 2) 
        {
            isUpdatingArrow = true;
            arrowMesh.SetActive(true);
            handBowstringPosition = args.interactorObject.transform;
        }
    }

    public void Shoot()
    {
        //Event is called when released. Only activate when one hand released and one still grabs
        grabCount--;
        if (grabCount != 1) return;
        ballonGame.ShootArrow();
        isUpdatingArrow = false;
        Vector3 direction = arrowMesh.transform.forward;
        GameObject instance = Instantiate(arrowPrefab,arrowMesh.transform.position, arrowMesh.transform.rotation);
        instance.transform.forward = bowGrabPoint.forward;
        Rigidbody arrowRb =  instance.GetComponent<Rigidbody>();
        float shootSpeed= baseSpeed * Vector3.Distance(handBowstringPosition.position,bowGrabPoint.position);
        arrowRb.AddForce(shootSpeed * direction,ForceMode.Impulse);
        arrowRb.useGravity = true;
        boneString.position = bowStringStart.position;
        handBowstringPosition = null;
        arrowMesh.SetActive(false);
    }

    [Header("Bowstring")]
    [SerializeField] Transform bowStringStart;    
    [SerializeField] Transform handTracker;    
    Transform handBowstringPosition;

    void UpdateArrow()
    {
        boneString.position = handBowstringPosition.position;
    }
}

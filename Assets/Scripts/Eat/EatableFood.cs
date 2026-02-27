using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class EatableFood : MonoBehaviour, IEatable
{
    [SerializeField] List<AudioClip> clips;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform model;
    [SerializeField] Rigidbody rb;
    [SerializeField] XRGrabInteractable grabInteractable;
    bool isEatable = false;
    

    public void Eat(Transform eater)
    {
        if(!isEatable){return;}
        if(eater.TryGetComponent<SoundPlayer>(out SoundPlayer player)){
            player.PlaySound(clips);
          
        }  
            isEatable = false;
            grabInteractable.throwOnDetach = false;
            gameObject.SetActive(false);
            transform.SetPositionAndRotation(startPoint.position, startPoint.rotation);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(true);
            this.enabled = false;
    }

    public void SetEatable()
    {
        isEatable = true;
    }
}

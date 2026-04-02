using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class EatableFood : MonoBehaviour, IEatable
{
    [SerializeField] List<AudioClip> clips;
    [SerializeField] Transform model;
    [SerializeField] Rigidbody rb;
    [SerializeField] XRGrabInteractable grabInteractable;
    [SerializeField] XRInteractablePooledObject pooledObject;
    bool isEatable = false;


    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(SetEatable);
        grabInteractable.lastSelectExited.AddListener(SetUneatable);
        isEatable = false;
        grabInteractable.throwOnDetach = true;
    }

    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(SetEatable);
        grabInteractable.lastSelectExited.RemoveListener(SetUneatable);
        grabInteractable.throwOnDetach = false;
    }

    void SetEatable(SelectEnterEventArgs args)
    {
        isEatable = true;
    }

    void SetUneatable(SelectExitEventArgs args)
    {
        isEatable = false;
    }

    public void Eat(Transform eater)
    {
        if(!isEatable){return;}
        if(eater.TryGetComponent<SoundPlayer>(out SoundPlayer player)){
            player.PlaySound(clips); 
        }  
        gameObject.SetActive(false);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        pooledObject.ReturnToPool();
    }
}

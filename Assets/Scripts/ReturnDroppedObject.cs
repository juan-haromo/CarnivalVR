using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ReturnDroppedObject : MonoBehaviour
{
    [SerializeField] XRGrabInteractable interactable;
    [SerializeField] float timeToReturn;
    Coroutine ReturnRoutine;
    Vector3 intialPosition; 
    Quaternion intialRotation;

    private void Awake()
    {
        intialPosition = transform.position;
        intialRotation = transform.rotation;
    }

    private void OnEnable()
    {
        if(interactable != null)
        {
            interactable.selectEntered.AddListener(Grabbed);
            interactable.lastSelectExited.AddListener(Released);
        }
    }
    private void OnDisable()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(Grabbed);
            interactable.lastSelectExited.RemoveListener(Released);
        }
    }

    private void Grabbed(SelectEnterEventArgs arg0)
    {
        if(ReturnRoutine != null) { StopCoroutine(ReturnRoutine);}
    }

    private void Released(SelectExitEventArgs arg0)
    {
        if(ReturnRoutine != null) { StopCoroutine(ReturnRoutine);}
        ReturnRoutine = StartCoroutine(ReturnToSpawn());
    }

    IEnumerator ReturnToSpawn()
    {
        yield return new WaitForSeconds(timeToReturn);
        transform.position = intialPosition;
        transform.rotation = intialRotation;
    }

}

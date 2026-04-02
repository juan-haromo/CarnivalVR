using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class XRInteractablePooledObject : PooledObject
{
    [SerializeField] XRBaseInteractable interactable;
    public XRBaseInteractable Interactable => interactable;
    [SerializeField] Transform grabDisplay;
    public GameObject GrabDisplay => grabDisplay.gameObject;    

    public void SetInteractable(bool state)
    {
        interactable.enabled = state;
        GrabDisplay.SetActive(state);
    }
}
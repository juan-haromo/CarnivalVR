using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabableDisplay : MonoBehaviour
{
    #region Manage Interactable Events
    [SerializeField] XRGrabInteractable grabInteractable;
    void OnEnable()
    {
        if(grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(Grab);   
            grabInteractable.lastSelectExited.AddListener(Release);
        }
        if(target == null)
        {
            target = GrabDisplayManager.Instance.targert;
        }
        imgDisplay.gameObject.SetActive(false); 
        isActive = true;
    }

    void OnDisable()
    {
        if(grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(Grab);   
            grabInteractable.lastSelectExited.RemoveListener(Release);
        }
        imgDisplay.gameObject.SetActive(false); 
        isActive = true;
    }
    #endregion
    
    bool isActive;
    [SerializeField] Transform target;
    [SerializeField] float detectionDistance;
    [SerializeField] Image imgDisplay;
    float t;
    void Update()
    {
        if(!isActive || !isInRange){return;}
        float distance = Vector3.Distance(target.position,transform.position);
        imgDisplay.gameObject.SetActive(true);
        Color imgColor = imgDisplay.color;
        t=Mathf.Clamp01(distance/detectionDistance);
        imgColor.a = t;
        imgDisplay.color = imgColor;
        transform.rotation = Quaternion.LookRotation(transform.position - target.position,Vector3.up);
    }

    private void Release(SelectExitEventArgs arg0)
    {
        if(!arg0.interactableObject.isSelected){return;}
        imgDisplay.gameObject.SetActive(true);
        isActive = true;
    }

    private void Grab(SelectEnterEventArgs arg0)
    {
        imgDisplay.gameObject.SetActive(false);
        isActive = false;
    }

    bool isInRange;

    void OnTriggerEnter(Collider other)
    {
        if(!isActive){return;}
        isInRange = true;
        imgDisplay.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        if(!isActive){return;}
        isInRange = false;
        imgDisplay.gameObject.SetActive(false);
    }
}
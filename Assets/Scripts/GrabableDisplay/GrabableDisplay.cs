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
        isActive = true;
        isInRange = false;
        isDirty = true;
    }

    void OnDisable()
    {
        if(grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(Grab);   
            grabInteractable.lastSelectExited.RemoveListener(Release);
        }
        isActive = false;
    }
    #endregion
    
    bool isActive;
    [SerializeField] Transform target;
    [SerializeField] float detectionDistance;
    [SerializeField] Image imgDisplay;
    float t;
    bool isDirty;
    void Update()
    {
        if(isDirty){imgDisplay.gameObject.SetActive(isActive && isInRange);}
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
        isActive = true;
        isInRange = false;
        isDirty = true;
    }

    private void Grab(SelectEnterEventArgs arg0)
    {
        isActive = false;
        isDirty = true;
    }

    bool isInRange;

    void OnTriggerEnter(Collider other)
    {
        if(!isActive){return;}
        isInRange = true;
        isDirty = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(!isActive){return;}
        isInRange = false;
        isDirty = true;
    }
}
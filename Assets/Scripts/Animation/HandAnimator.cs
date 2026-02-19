using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

[RequireComponent(typeof(Animator))]
public class HandAnimator : MonoBehaviour
{
    [SerializeField] private XRInputValueReader<float> m_StickInput = new XRInputValueReader<float>("Thumbstick");
    [SerializeField] private XRInputValueReader<float> m_TriggerInput = new XRInputValueReader<float>("Trigger");
    [SerializeField] private XRInputValueReader<float> m_GripInput = new XRInputValueReader<float>("Grip");

    private readonly List<Finger> thumbFingers = new List<Finger>()
    {
        new Finger(FingerType.Thumb)
    };
    
    private readonly List<Finger> pointingFingers = new List<Finger>()
    {
        new Finger(FingerType.Index)
    };

    private readonly List<Finger> grippingFingers = new List<Finger>()
    {
      new Finger(FingerType.Middle),  
      new Finger(FingerType.Ring),  
      new Finger(FingerType.Pinky)  
    };
    
    Animator handAnimator = null;
    void Start()
    {
        handAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if(m_StickInput != null)
        {
            float stickValue = m_StickInput.ReadValue();
            SetFingerAnimationValue(thumbFingers,stickValue);
            AnimateActionInput(thumbFingers);
        }
        if(m_TriggerInput != null)
        {
            float triggerValue = m_TriggerInput.ReadValue();
            SetFingerAnimationValue(pointingFingers,triggerValue);
            AnimateActionInput(pointingFingers);
        }
        if(m_GripInput != null)
        {
           float gripValue = m_GripInput.ReadValue();
           SetFingerAnimationValue(grippingFingers,gripValue);
           AnimateActionInput(grippingFingers); 
        }
    }

    public void SetFingerAnimationValue(List<Finger> fingersToAnimate, float blendValue)
    {
        foreach(Finger f in fingersToAnimate)
        {
            f.blendValue = blendValue;
        }
    }

    public void AnimateActionInput(List<Finger> fingersToAnimate)
    {
        foreach(Finger f in fingersToAnimate)
        {
            handAnimator.SetFloat(f.Name,f.blendValue);
        }
    }
}

public class Finger
{
    public float blendValue;

    private readonly FingerType type;
    public string Name{get => type.ToString();}

    public Finger(FingerType type)
    {
        this.type = type;
    }
}

public enum FingerType
{
    Thumb,
    Index,
    Middle,
    Ring,
    Pinky
}
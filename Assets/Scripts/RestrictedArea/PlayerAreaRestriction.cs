using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Gravity;

public class PlayerAreaRestriction : MonoBehaviour, IRestrictedObject
{
    [SerializeField] Fade fade;
    [SerializeField] private CharacterController controller;
    [SerializeField] private SoundPlayer restrictedAreaSound;
    [SerializeField] private GravityProvider gravityProvider;

    void Start()
    {
        ActiveMovement(false);
        fade.FadeOut(() => ActiveMovement(true));    
    }

    public void OnEnterRestrictedArea(Transform exitPosition)
    {
        restrictedAreaSound.PlaySound();
        FadeIn(exitPosition.position);
    }

    void FadeIn(Vector3 exitPosition)
    {
        ActiveMovement(false);
        fade.FadeIn(() => FadeOut(exitPosition));
    }

    void FadeOut(Vector3 exitPosition)
    {
        transform.position = exitPosition;
        fade.FadeOut(() => ActiveMovement(true));
    }

    void ActiveMovement(bool active)
    {
        gravityProvider.useGravity = active; 
        //controller.attachedRigidbody.useGravity = active;
        controller.enabled = active;
    }
}
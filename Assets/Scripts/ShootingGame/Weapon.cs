using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform shootStart;
    [SerializeField] float maxShootSDistance = 50;
    [SerializeField] LayerMask collitionMask;
    [SerializeField] float damage;
    IXRSelectInteractor shootInteractor;
    [SerializeField] SoundPlayer shootSoundPlayer;
    [SerializeField] ParticleSystem collisionParticles;
    [SerializeField] ShootingGame shootingGame;
    [SerializeField] Animator rifleAnimator;
    
    public void Shoot(ActivateEventArgs args)
    {
        Shoot();
    }
    void Shoot(){
        shootSoundPlayer.PlaySound();
        rifleAnimator.SetTrigger("Shoot");
        shootingGame.ShootBullet();
        RaycastHit hit;
        Physics.Raycast(shootStart.position,shootStart.forward, out hit, maxShootSDistance,collitionMask);
        if (hit.collider!= null)
        {
            collisionParticles.Stop();
            collisionParticles.transform.position = hit.point;
            collisionParticles.transform.forward = shootStart.transform.position - hit.point;
            collisionParticles.Play();
            if(hit.collider.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                damagable.Damage(damage);
            }
        }
    }


    public void Grab(SelectEnterEventArgs args)
    {
        if(shootInteractor == null || args.interactableObject.interactorsSelecting.Count == 1)
        {
            shootInteractor = args.interactorObject;
        }
    }

    public void Release(SelectExitEventArgs args)
    {
        //Hand in trigger is released
        if(args.interactorObject == shootInteractor)
        {
            shootInteractor = null;
        }
        //There is another hand on weapon
        if(0 < args.interactableObject.interactorsSelecting.Count)
        {
            shootInteractor = args.interactableObject.interactorsSelecting[0];
        }
        
    }
}
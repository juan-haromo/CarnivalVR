using UnityEngine;

public class ShootingGameTarget : MonoBehaviour, IDamagable, IShootingTarget
{
    [SerializeField] float maxHealth;
    float currentHealth;
    [SerializeField] ShootingGame shootingGame;
    [SerializeField] SoundPlayer breakSound;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] Collider col;

   

    public void Damage(float amount)
    {
        currentHealth -= Mathf.Abs(amount);
        if(currentHealth <= 0)
        {
            shootingGame.HitTarget();
            breakSound.PlaySound();
            Deactivate();
        }
    } 
    
    public void Activate()
    {
        col.enabled = true;
        mesh.enabled = true;
        currentHealth = maxHealth;
    }


    public void Deactivate()
    {
        col.enabled = false;
        mesh.enabled = false;
    }
}
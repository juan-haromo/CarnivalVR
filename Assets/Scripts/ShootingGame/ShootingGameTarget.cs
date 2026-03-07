using UnityEngine;

public class ShootingGameTarget : MonoBehaviour, IDamagable
{
    [SerializeField] float maxHealth;
    [SerializeField] ShootingGame shootingGame;
    [SerializeField] SoundPlayer breakSound;
    float currentHealth;
    [SerializeField] Transform mesh;
 
    public void Damage(float amount)
    {
        currentHealth -= Mathf.Abs(amount);
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
            mesh.gameObject.SetActive(false);
            shootingGame.HitTarget();
            breakSound.PlaySound();
        }
    }

    void OnEnable()
    {
        mesh.gameObject.SetActive(true);
        currentHealth = maxHealth;
    }
}
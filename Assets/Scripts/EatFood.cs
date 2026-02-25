using UnityEngine;

public class EatFood : MonoBehaviour, IEatable
{
    public Transform spawn;
    public SoundPlayer soundPlayer;
    public Transform foodTransform;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Eat()
    {
        soundPlayer.PlaySound();
        foodTransform.position = spawn.position;
        foodTransform.rotation = spawn.rotation;
        rb.Sleep();
    }
}

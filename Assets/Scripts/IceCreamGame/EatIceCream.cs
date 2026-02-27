using UnityEngine;

public class EatIceCream : MonoBehaviour, IEatable
{
    [SerializeField] IceCreamGameManager manager;
    public bool canBeAten;
    public void Eat(Transform eater)
    {
        if(!canBeAten){return;}
        canBeAten = false;
        manager.StopIceCreamGame();
    }
}
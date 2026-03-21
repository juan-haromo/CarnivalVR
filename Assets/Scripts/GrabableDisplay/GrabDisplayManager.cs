using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GrabDisplayManager : MonoBehaviour
{
    public static GrabDisplayManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public Transform targert;
}
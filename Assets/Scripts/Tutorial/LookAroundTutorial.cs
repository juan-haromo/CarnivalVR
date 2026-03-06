using UnityEngine;
using UnityEngine.InputSystem;

public class LookAroundTutorial : MonoBehaviour
{
    [SerializeField] InputActionReference lookReference;
    [SerializeField] int times;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lookReference.action.WasPressedThisFrame() && lookReference.action.ReadValue<Vector2>().x != 0)
        {
            times--;
            if (times <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

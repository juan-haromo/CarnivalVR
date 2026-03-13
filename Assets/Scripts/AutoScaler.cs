using UnityEngine;
using UnityEngine.InputSystem;

public class AutoScaler : MonoBehaviour
{
    [SerializeField] InputActionReference actionReference;

    void Awake()
    {
        actionReference.action.performed += Resize;
    }

    [SerializeField] float defaultHeight;
    [SerializeField] Transform cameraOffset;
    private void Resize(InputAction.CallbackContext context)
    {
        float headHeight = cameraOffset.localPosition.y;
        float scale = defaultHeight/headHeight;
        transform.localScale = Vector3.one * scale;
    }
} 
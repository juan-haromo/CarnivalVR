using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.Hands;

public class LevelChangeUI : MonoBehaviour
{
    [SerializeField] Transform ui;
    [SerializeField] private InputActionReference openUIAction;

    void Start()
    {
        ui.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        openUIAction.action.performed += ToggleUI;
    }

    void OnDisable()
    {
        openUIAction.action.performed -= ToggleUI;
    }

    private void ToggleUI(InputAction.CallbackContext context)
    {
        ui.gameObject.SetActive(!ui.gameObject.activeSelf);
    }
}

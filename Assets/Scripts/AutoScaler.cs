using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScaler : MonoBehaviour
{
    private const string SCALE = "PLAYER_SCALE";

    [SerializeField] float defaultHeight;
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;
    [SerializeField] Transform cameraOffset;

    void Awake()
    {
        Resize(PlayerPrefs.GetFloat(SCALE, defaultHeight));
    }

    private void Resize(InputAction.CallbackContext context)
    {
        Resize();
    }


    public void Resize()
    {
        float headHeight = Mathf.Clamp(cameraOffset.localPosition.y,minHeight,maxHeight);   
        float scale = defaultHeight/headHeight;
        Resize(scale);
        PlayerPrefs.SetFloat(SCALE,scale);
    }

    private void Resize(float scale)
    {
        Debug.Log(scale);
        transform.localScale = Vector3.one * scale;
    }

   
} 
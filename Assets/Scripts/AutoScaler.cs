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
    [SerializeField] private SoundPlayer soundPlayer;
    [SerializeField] private SoundContainer growSound;
    [SerializeField] private SoundContainer shrinkSound;
    [SerializeField] private Fade fade;

    void Awake()
    {
        Resize(PlayerPrefs.GetFloat(SCALE, defaultHeight));
    }


    public void Resize(bool playSound = false)
    {
        float headHeight = Mathf.Clamp(cameraOffset.localPosition.y,minHeight,maxHeight);   
        float scale = defaultHeight/headHeight;
        Resize(scale);
        PlayerPrefs.SetFloat(SCALE,scale);
        if(playSound)
        {
            if(cameraOffset.localPosition.y < headHeight )
            {
                soundPlayer.PlaySound(growSound);
            }
            else
            {
                soundPlayer.PlaySound(shrinkSound);
            }
        }
    }

    private void Resize(float scale)
    {
        Debug.Log(scale);
        transform.localScale = Vector3.one * scale;
    }

    public void FadeResize()
    {
        fade.FadeIn(FadeOutResize);
    }

    void FadeOutResize()
    {
        Resize(true);
        fade.FadeOut();
    }

   
} 
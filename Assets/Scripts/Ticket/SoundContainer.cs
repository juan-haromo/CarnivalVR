using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundContainer", menuName = "ScriptableObjects/SoundContainer", order = 1)]
public class SoundContainer : ScriptableObject
{
    public List<AudioClip> sounds;
    public virtual AudioClip GetSound()
    {
        if (sounds.Count == 0) {return null;}
        int i = Random.Range(0, sounds.Count);
        return sounds[i];
    }
}
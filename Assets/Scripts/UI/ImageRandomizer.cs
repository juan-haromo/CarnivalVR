using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageRandomizer : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Image image;

    void OnEnable()
    {
        image.sprite = sprites[Random.Range(0, sprites.Count)];
    }
}

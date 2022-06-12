using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerManager : MonoBehaviour
{

    [SerializeField]
    private Image image;


    public void UpdateSticker(Sprite sprite)
    {
        image.sprite = sprite;
    }
}

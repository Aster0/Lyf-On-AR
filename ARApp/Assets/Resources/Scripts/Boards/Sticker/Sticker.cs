using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sticker", menuName = "LyfOn/Sticker/New Sticker", order = 1)]
public class Sticker : ScriptableObject
{
    public Sprite stickerSprite;

    [Range(0,1000)]
    public int price;

    public bool purchasable;

}

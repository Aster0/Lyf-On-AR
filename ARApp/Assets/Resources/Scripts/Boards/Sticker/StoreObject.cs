using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GITHUB DOCUMENTATION ISSUE FOUND HERE: https://github.com/Aster0/Lyf-On-AR/issues/12

[CreateAssetMenu(fileName = "New Store Object", menuName = "LyfOn/Sticker/New Store Object", order = 1)]
public class StoreObject : ScriptableObject
{
    public Sprite stickerSprite;

    [Range(0,1000)]
    public int price;

    public bool purchasable;


    public StorePopulator.StoreType storeType;

}

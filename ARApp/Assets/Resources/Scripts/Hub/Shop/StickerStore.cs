using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StickerStore : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI nameText, priceText;
    
    [SerializeField]
    private Image icon;
    
    public Sticker sticker { get; set; }

    public bool owned { get; set; }

    public void UpdateStickerStoreIcon(Sticker sticker, bool owned)
    {
        nameText.text = sticker.name.Replace("Sticker", "");

        this.sticker = sticker;

        this.owned = owned;
        
        if(!owned)
            priceText.text = sticker.price.ToString();
        else
        {
            priceText.text = "OWNED";

            Destroy(GetComponent<Button>());
        }

        icon.sprite = sticker.stickerSprite;
    }

    public void UpdateStickerStatus()
    {
        priceText.text = "OWNED";
    }
}

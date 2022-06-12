using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// GITHUB DOCUMENTATION ISSUE FOUND HERE: https://github.com/Aster0/Lyf-On-AR/issues/12
public class StoreIcon : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI nameText, priceText;
    
    [SerializeField]
    private Image icon;
    
    public StoreObject sticker { get; set; }

    public bool owned { get; set; }

    public void UpdateStoreIcon(StoreObject storeObj, bool owned)
    {
        nameText.text =    Regex.Replace(storeObj.name.Replace("Sticker", "").Replace("Avatar", ""), 
            "([a-z])([A-Z])", "$1 $2"); // putting a space before capital letters using Regex
        this.sticker = storeObj;
        

   

        this.owned = owned;
        
        if(!owned)
            priceText.text = storeObj.price.ToString();
        else
        {
            priceText.text = "OWNED";

            Destroy(GetComponent<Button>());
        }

        icon.sprite = storeObj.sprite;
    }

    public void UpdateStoreItemStatus()
    {
        priceText.text = "OWNED";
    }
}

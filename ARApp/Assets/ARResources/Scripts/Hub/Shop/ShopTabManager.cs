using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// GITHUB DOCUMENTATION ISSUE FOUND HERE: https://github.com/Aster0/Lyf-On-AR/issues/18
public class ShopTabManager : MonoBehaviour
{

    [SerializeField]
    private Image tabFocusedImage;

    [SerializeField]
    private Color focusedColor, unfocusedColor;


    [SerializeField]
    private StorePopulator.StoreType storeType;


    [SerializeField] private ShopTabManager otherTab;

    [SerializeField] private StorePopulator storePopulator;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnTabSwitch);
    }

    private void OnTabSwitch() // on player switches the shop tab
    {
        tabFocusedImage.color = focusedColor;
        UnfocusTab();

        storePopulator.storeType = this.storeType;
        
        storePopulator.UpdateStoreContents();
    }


    private void UnfocusTab() // when the other tab should be unfocused as it's switched away
    {
        otherTab.tabFocusedImage.color = otherTab.unfocusedColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    private void UnfocusTab()
    {
        otherTab.tabFocusedImage.color = otherTab.unfocusedColor;
    }
}

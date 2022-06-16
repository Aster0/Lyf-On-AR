using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

// GITHUB DOCUMENTATION ISSUE FOUND HERE: https://github.com/Aster0/Lyf-On-AR/issues/18
public class StickerStoreButton : MonoBehaviour
{


    private Button _button;


    private StorePopulator storePopulator;

    private GameManager _gameManager;

    private StoreIcon storeItem;

    private PointsManager _pointsManager;
    // Start is called before the first frame update
    void Start()
    {
        storeItem = GetComponent<StoreIcon>();
        _gameManager = GameManager.Instance;
        
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(OnStorePurchase);

        _pointsManager = FindObjectOfType<PointsManager>();

        storePopulator = FindObjectOfType<StorePopulator>();

    }


    private void OnStorePurchase() // when the player buys an item
    {

        
        
   
        string collectionName = "";
        string documentName = "";
        
 
            
        if (storeItem.owned)
        {
            return; // dont buy sticker
        }

        if (_gameManager.user.details.points < storeItem.sticker.price) // not enough money
        {
        
            ErrorPopupManager.GeneratePopup("Not enough points to buy this!");
            return;
        }
        
        collectionName = "users";
        documentName = _gameManager.user.uuid;


        int newPoints = _gameManager.user.details.points -
                        storeItem.sticker.price;
        
        _pointsManager.UpdatePointsText(newPoints); // update the new points visually
 
        
        _gameManager.user.UpdatePlayerDetails(newPoints, User.UpdateType.POINTS); // update the database with the new deducted points

        storeItem.owned = true;

        storeItem.UpdateStoreItemStatus();


        User.UpdateType updateType = User.UpdateType.STICKER;

        if (storePopulator.storeType == StorePopulator.StoreType.AVATAR)
        {
            updateType = User.UpdateType.AVATAR;
        }

        _gameManager.user.UpdatePlayerDetails(storeItem.sticker,updateType);
        
        

        Debug.Log("BOUGHT");

    }

}

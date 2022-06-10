using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

// GITHUB DOCUMENTATION ISSUE FOUND HERE: https://github.com/Aster0/Lyf-On-AR/issues/5
public class StickerStoreButton : MonoBehaviour
{


    private enum StoreItemType
    {
        STICKER
    }
    
    private Button _button;

    [SerializeField]
    private StoreItemType _storeItemType;

    private GameManager _gameManager;

    private StickerStore stickerStore;

    private PointsManager _pointsManager;
    // Start is called before the first frame update
    void Start()
    {
        stickerStore = GetComponent<StickerStore>();
        _gameManager = GameManager.Instance;
        
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(OnStorePurchase);

        _pointsManager = FindObjectOfType<PointsManager>();
        


    }


    private void OnStorePurchase() // when the player buys an item
    {

        Debug.Log("PRESSED");

        
        
   
        string collectionName = "";
        string documentName = "";
        
        if (_storeItemType == StoreItemType.STICKER)
        {
            
            if (stickerStore.owned)
            {
                return; // dont buy sticker
            }

            if (_gameManager.user.details.points < stickerStore.sticker.price) // not enough money
            {
            
                ErrorPopupManager.GeneratePopup("Not enough points to buy this!");
                return;
            }
            
            collectionName = "users";
            documentName = _gameManager.user.uuid;


            int newPoints = _gameManager.user.details.points -
                            stickerStore.sticker.price;
            
            _pointsManager.UpdatePointsText(newPoints); // update the new points visually
     
            
            _gameManager.user.UpdatePlayerDetails(newPoints, User.UpdateType.POINTS); // update the database with the new deducted points

            stickerStore.owned = true;

            stickerStore.UpdateStickerStatus();
        }
        
        
        _gameManager.user.UpdatePlayerDetails(stickerStore.sticker, User.UpdateType.STICKER);
        
        

        Debug.Log("BOUGHT");

    }

}

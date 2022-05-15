using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerStorePopulator : MonoBehaviour
{

    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        
        Object[] stickerObjects = UnityEngine.Resources.LoadAll("Stickers/StickersObject/");

        
        foreach (Object obj in stickerObjects)
        {
            
            Sticker sticker = (Sticker) obj;


            if (!sticker.purchasable) // if its not purchasable, 
                continue; // we skip this iteration and dont show in the shop.
            
            GameObject stickerObject = Instantiate(_gameManager.stickerStorePrefab, new Vector3(0,0),
                Quaternion.identity);
            
            stickerObject.transform.SetParent(this.gameObject.transform, false);


            bool owned = false;

            if (_gameManager.user.details.ownedStickers.Contains(sticker))
            {
                owned = true;
                Debug.Log("Owned");
            }


            StickerStore stickerStore = stickerObject.GetComponent<StickerStore>();
            
            stickerStore.UpdateStickerStoreIcon(sticker, owned);
        }
    }


}

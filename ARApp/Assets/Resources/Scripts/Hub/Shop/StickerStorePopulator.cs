using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GITHUB DOCUMENTATION ISSUE FOUND HERE: https://github.com/Aster0/Lyf-On-AR/issues/12
public class StickerStorePopulator : MonoBehaviour
{

    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        
        Object[] stickerObjects = UnityEngine.Resources.LoadAll("Stickers/StickersObject/"); // get all the sticker scriptable objects

        
        foreach (Object obj in stickerObjects) // iterate the objects array
        {
            
            Sticker sticker = (Sticker) obj; // cast it back to a sticker object cause we know they are stickers 


            if (!sticker.purchasable) // if its not purchasable, 
                continue; // we skip this iteration and dont show in the shop.
            
            GameObject stickerObject = Instantiate(_gameManager.stickerStorePrefab, new Vector3(0,0),
                Quaternion.identity); // create a new game object icon
            
            stickerObject.transform.SetParent(this.gameObject.transform, false); // update the parent


            bool owned = false;

            if (_gameManager.user.details.ownedStickers.Contains(sticker)) // if the player already owns the sticker
            {
                owned = true; // set owned to true so they can't buy it again.
                Debug.Log("Owned");
            }


            StickerStore stickerStore = stickerObject.GetComponent<StickerStore>(); // get the stickerstore instance 
            // from the current newly instantiated game object
            
            stickerStore.UpdateStickerStoreIcon(sticker, owned); // update the sticker and the owned status.
        }
    }


}

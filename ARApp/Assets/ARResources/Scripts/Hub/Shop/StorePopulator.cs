using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GITHUB DOCUMENTATION ISSUE FOUND HERE: https://github.com/Aster0/Lyf-On-AR/issues/12
public class StorePopulator : MonoBehaviour
{

    public enum StoreType // w hich type should be loaded up in the shop?
    {
        STICKER,
        AVATAR
    }

    private GameManager _gameManager;

  
    public StoreType storeType;

    private bool firstTimeOver;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;


        UpdateStoreContents();

        firstTimeOver = true;
    }

    public void UpdateStoreContents()
    {

        if (firstTimeOver)
        {
            foreach (Transform child in gameObject.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        Object[] objects = UnityEngine.Resources.LoadAll("StoreItems/Objects/"); // get all the store item scriptable objects

        
        
        foreach (Object obj in objects) // iterate the objects array
        {
            
            StoreObject storeObj = (StoreObject) obj; // cast it back to a sticker object cause we know they are stickers 

            if (storeObj.storeType == storeType)
            {
                if (!storeObj.purchasable) // if its not purchasable, 
                    continue; // we skip this iteration and dont show in the shop.


       
                GameObject storeGameObject = Instantiate(_gameManager.stickerStorePrefab, new Vector3(0,0),
                    Quaternion.identity); // create a new game object icon
            
                storeGameObject.transform.SetParent(this.gameObject.transform, false); // update the parent


                bool owned = false;

                bool containsInList = _gameManager.user.details.ownedStickers.Contains(storeObj) ||
                                      _gameManager.user.details.ownedAvatars.Contains(storeObj);

                if (containsInList) // if the player already owns the sticker
                {
                    owned = true; // set owned to true so they can't buy it again.
               
                }
          


                StoreIcon storeIcon = storeGameObject.GetComponent<StoreIcon>(); // get the store icon instance 
                // from the current newly instantiated game object
            
                storeIcon.UpdateStoreIcon(storeObj, owned); // update the sticker and the owned status.
            }

            
        }
    }


}

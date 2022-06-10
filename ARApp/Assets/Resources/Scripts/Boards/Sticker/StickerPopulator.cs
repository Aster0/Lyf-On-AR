using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerPopulator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (StoreObject sticker in GameManager.Instance.user.details.ownedStickers)
        {
            GameObject stickerObject = Instantiate(GameManager.Instance.stickerPrefab, new Vector3(0,0),
                Quaternion.identity);
            
            stickerObject.transform.SetParent(this.gameObject.transform, false);



            StickerManager stickerManager = stickerObject.GetComponent<StickerManager>();
            
            stickerManager.UpdateSticker(sticker.sprite);
        }
    }


}

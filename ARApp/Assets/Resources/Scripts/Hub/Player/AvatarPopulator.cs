using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarPopulator : MonoBehaviour
{

    private GameManager _gameManager;
    private void Start()
    {
        _gameManager = GameManager.Instance;
        
        foreach (StoreObject storeObject in _gameManager.listOfAllAvatars)
        {
            GameObject gameObject = Instantiate(_gameManager.avatarPrefab, new Vector3(0,0),
                Quaternion.identity); // create a new game object icon
            
            gameObject.transform.SetParent(this.gameObject.transform, false); // update the parent

            
            AvatarIcon avatarIcon = gameObject.GetComponent<AvatarIcon>(); // get the avatar icon instance 
            // from the current newly instantiated game object
            
            avatarIcon.UpdateAvatarIcon(storeObject);
            
        }
    }
}

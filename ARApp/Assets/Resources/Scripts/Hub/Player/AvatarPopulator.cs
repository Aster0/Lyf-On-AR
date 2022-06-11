using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarPopulator : MonoBehaviour
{

    public AvatarIcon currentlySelectedAvatar { get; set; }
    
    public Button saveButton;
    
    private GameManager _gameManager;

    private bool startedBefore = false;

    private UserProfileManager userProfileManager;
    private void Awake()
    {
        _gameManager = GameManager.Instance;
        userProfileManager = FindObjectOfType<UserProfileManager>();
        
        saveButton.onClick.AddListener(OnSave);
        
   
    }

    private void OnEnable()
    {
        if (startedBefore)
        {
            foreach (Transform child in this.transform) // iterate through the whole grid and reset it.
            {
                Destroy(child.gameObject);
            }
        }
        
        foreach (StoreObject storeObject in _gameManager.listOfAllAvatars)
        {
            GameObject avatarGameObject = Instantiate(_gameManager.avatarPrefab, new Vector3(0,0),
                Quaternion.identity); // create a new game object icon
            
            avatarGameObject.transform.SetParent(this.gameObject.transform, false); // update the parent

            
            AvatarIcon avatarIcon = avatarGameObject.GetComponent<AvatarIcon>(); // get the avatar icon instance 
            // from the current newly instantiated game object
            
            avatarIcon.UpdateAvatarIcon(storeObject);
            
        }

        startedBefore = true;

    }

    private void OnSave() // on avatar saved
    {
        if (currentlySelectedAvatar != null)
        {
            _gameManager.user.UpdatePlayerDetails(currentlySelectedAvatar.avatarStoredInfo.name,
                User.UpdateType.CURRENT_AVATAR);

            _gameManager.user.details.currentAvatar = currentlySelectedAvatar.avatarStoredInfo;
            
            userProfileManager.UpdateAvatar();
        }
    }
}

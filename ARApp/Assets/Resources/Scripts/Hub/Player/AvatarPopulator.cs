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

            if (_gameManager.user.details.currentAvatar != null) // has a current avatar already,
            {
                // we highlight it visually to show.

                if (_gameManager.user.details.currentAvatar.name.Equals(storeObject.name)) // if the current iterated avatar matches the current avatar
                {
                    // we can highlight as we know which one to highlight now.
                    
                    avatarIcon.ToggleOutline();
                    currentlySelectedAvatar = avatarIcon;  // make sure the system updates
                    // which is the currently selected avatar in the menu as well,
                    // so we can remove the outline when they choose another avatar.
                }
            }
            
        }

        startedBefore = true;

    }

    private void OnSave() // on avatar saved
    {
        if (currentlySelectedAvatar != null) // if its not null, (null check)
        {
            _gameManager.user.UpdatePlayerDetails(currentlySelectedAvatar.avatarStoredInfo.name,
                User.UpdateType.CURRENT_AVATAR); // update firebase 

            Debug.Log(currentlySelectedAvatar.avatarStoredInfo.name +  " SELECTED");
            _gameManager.user.details.currentAvatar = currentlySelectedAvatar.avatarStoredInfo; 
            // update locally with the new avatar
            Debug.Log(_gameManager.user.details.currentAvatar.name + " CURRENT!");
            
            userProfileManager.UpdateAvatar(); // update the avatar in the hub screen visually
            saveButton.interactable = false; // reset back to false.
        }
    }
}

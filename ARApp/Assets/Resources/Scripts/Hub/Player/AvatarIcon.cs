using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarIcon : MonoBehaviour
{

    private AvatarPopulator avatarPopulator;
    private Outline outline;

    public StoreObject avatarStoredInfo { get; set; }

    private GameManager _gameManager;


    private void Awake()
    {
        _gameManager = GameManager.Instance;
        outline = GetComponent<Outline>();
        GetComponent<Button>().onClick.AddListener(OnAvatarSelect);
      
        

        avatarPopulator = FindObjectOfType<AvatarPopulator>();
    }

    private void OnAvatarSelect() // when the avatar is selected by the player
    {

        if (lockIcon.activeSelf == false) // if its unlock then we can select
        {
            if (avatarPopulator.currentlySelectedAvatar != null) // if there's a previously selected avatar
            {
                avatarPopulator.currentlySelectedAvatar.ToggleOutline(); // turn off the outline
            }
            
            avatarPopulator.currentlySelectedAvatar = this;
            ToggleOutline(); // turn on for this current avatar


            if (avatarPopulator.saveButton.interactable == false) // previously non interactable because nothing was selected
            {
                

                avatarPopulator.saveButton.interactable = true; // now something is selected so we can enable the save.
            }
           
            if(_gameManager.user.details.currentAvatar.name.Equals(avatarStoredInfo.name)) // if the player chooses the current avatar, we don't need them to save again.
                avatarPopulator.saveButton.interactable = false; // so we set the interactable to false so the player cant press.
        
        }
            
        
        
    }


    public void ToggleOutline()
    {

        outline.enabled = !outline.enabled;
    }

    public GameObject lockIcon;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField]
    private Image icon;



    public void UpdateAvatarIcon(StoreObject obj)
    {
        if (GameManager.Instance.user.details.ownedAvatars.Contains(obj)) // if we own it,
        {
            lockIcon.SetActive(false); // we dont show the lock icon because it's unlocked.
        }


        nameText.text = Regex.Replace(obj.name.Replace("Avatar", ""), 
            "([a-z])([A-Z])", "$1 $2"); // putting a space before capital letters using Regex
        
        icon.sprite = obj.sprite;

        avatarStoredInfo = obj;


    }
}

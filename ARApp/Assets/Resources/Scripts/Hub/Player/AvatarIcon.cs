using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarIcon : MonoBehaviour
{

    private AvatarPopulator avatarPopulator;
    private Outline outline;

    public StoreObject avatarStoredInfo { get; set; }


    private void Start()
    {
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


        nameText.text = obj.name.Replace("Avatar", "");
        icon.sprite = obj.sprite;

        avatarStoredInfo = obj;


    }
}

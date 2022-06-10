using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarIcon : MonoBehaviour
{

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


    }
}

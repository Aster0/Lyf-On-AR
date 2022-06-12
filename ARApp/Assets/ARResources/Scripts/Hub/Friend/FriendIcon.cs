using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendIcon : MonoBehaviour
{

    [SerializeField]
    private Image image;

    [SerializeField]
    private TextMeshProUGUI username, count;



    public void UpdateFriendIcon(User user, int currentCount)
    {
 
        if(user.details.currentAvatar != null) // if the user has a profile picture
            image.sprite = user.details.currentAvatar.sprite;

        username.text = user.username;

        count.text = currentCount.ToString();
    }
}

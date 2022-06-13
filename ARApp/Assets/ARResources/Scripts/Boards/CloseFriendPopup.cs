using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseFriendPopup : MonoBehaviour
{

    private Button button; 
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // find and turn off the friend popup
        
        AddFriendButton previousFriendObject = FindObjectOfType<AddFriendButton>();

        if (previousFriendObject != null)
        {
            Destroy(previousFriendObject.gameObject);
        }
    }
}

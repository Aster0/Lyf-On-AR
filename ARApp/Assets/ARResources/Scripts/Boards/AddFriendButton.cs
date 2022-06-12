using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddFriendButton : MonoBehaviour
{

    public User user { get; set; }
    private Button button;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {

        button = GetComponent<Button>();
        _gameManager = GameManager.Instance;



    
        
        button.onClick.AddListener(OnFriendAdd);
    }

    private void OnFriendAdd()
    {
        

        _gameManager.user.UpdatePlayerDetails(user, User.UpdateType.FRIENDS);
      
        
        
        button.interactable = false; // make it so the user can't add again.
        

        
    }
}

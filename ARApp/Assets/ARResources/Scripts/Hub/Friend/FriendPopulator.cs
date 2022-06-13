
using System;
using TMPro;
using UnityEngine;

public class FriendPopulator : MonoBehaviour
{
    private bool startedBefore = false;

    private GameManager _gameManager;

    [SerializeField] private TextMeshProUGUI noFriendsText;

    private int count;
    private void Awake()
    {
        _gameManager = GameManager.Instance;
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

        count = 1;
        
        foreach (User user in _gameManager.user.details.friends)
        {
            GameObject friendGameObject = Instantiate(_gameManager.friendPrefab, new Vector3(0,0),
                Quaternion.identity); // create a new game object icon
            
            friendGameObject.transform.SetParent(this.gameObject.transform, false); // update the parent


            FriendIcon friendIcon = friendGameObject.GetComponent<FriendIcon>(); // get the friend icon instance 
            // from the current newly instantiated game object
            
            friendIcon.UpdateFriendIcon(user, count);


            count++;

            startedBefore = true;


        }
        
        if(count > 1) // means there's friends added
            noFriendsText.gameObject.SetActive(false); // dont show the no friends text
    }
}

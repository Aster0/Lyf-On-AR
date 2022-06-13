using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.Boards
{
    public class ChatBoard : MonoBehaviour
    {


        [SerializeField]
        private TextMeshProUGUI usernameText, messageText;


        [SerializeField] private Image image, avatarImage;

        private GameManager _gameManager;

        private User currentUser;


        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnChatClick);
            _gameManager = GameManager.Instance;
        }

        private void OnChatClick()
        {

            if (currentUser.uuid != _gameManager.user.uuid) // check if its not yourself, so u cant add yourself
            {
                AddFriendButton previousFriendObject = FindObjectOfType<AddFriendButton>();

                if (previousFriendObject != null)
                {
                    Destroy(previousFriendObject.gameObject);
                }
            
                GameObject friendGameObject = Instantiate(_gameManager.friendPopupPrefab, new Vector3(0,0),
                    Quaternion.identity); // create a new game object icon
            
                friendGameObject.transform.SetParent(this.gameObject.transform, false); // update the parent

                AddFriendButton addFriendButton = friendGameObject.GetComponent<AddFriendButton>();
            
                Debug.Log(currentUser.uuid + " CURRENT");
                foreach (User friendUser in _gameManager.user.details.friends)
                {
                    if (friendUser.uuid.Equals(currentUser.uuid)) // if we found a friend that matches this we're trying to add
                    {
                
                        addFriendButton.GetComponent<Button>().interactable = false; // make it so the user can't add again. as the friend is already added.
                        break; // break out of the iteration.
                    }
                }
            
                addFriendButton.user = currentUser;

                friendGameObject.transform.localPosition = new Vector3(370, 50);
            }
            
        }

        public void BuildChatBoard(string username, string message, string uuid, string avatarName) // overloaded method
        {
            usernameText.text = username;

            currentUser = new User();
            currentUser.username = username;
            currentUser.uuid = uuid;
            
   
         

            StoreObject avatarObject = UnityEngine.Resources.Load("StoreItems/Objects/" + avatarName,
                    typeof(StoreObject))
                as StoreObject;

            if (avatarObject != null)
            {
                avatarImage.sprite = avatarObject.sprite;

                currentUser.details = new User.Details();
                currentUser.details.currentAvatar = avatarObject;
            }

 

            if (message.StartsWith("STICKER//"))
            {
                Sprite sprite = 
                    UnityEngine.Resources.Load("StoreItems/" + message.Replace("STICKER//", ""),
                            typeof(Sprite))
                        as Sprite;
                

         
        

                image.sprite = sprite;
            }
            else
            {
                messageText.text = message;
            }
        }
        
        public void BuildChatBoard(string username, string message, string uuid) // overloaded method
        {
            usernameText.text = username;
            
            currentUser = new User();
            currentUser.username = username;
            currentUser.uuid = uuid;


            if (message.StartsWith("STICKER//"))
            {
                Sprite sprite = 
                    UnityEngine.Resources.Load("StoreItems/" + message.Replace("STICKER//", ""),
                            typeof(Sprite))
                        as Sprite;
                

         

                image.sprite = sprite;
            }
            else
            {
                messageText.text = message;
            }
        }
    }
    
}
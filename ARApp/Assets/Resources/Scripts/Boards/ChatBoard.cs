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



        
        public void BuildChatBoard(string username, string message, string avatarName) // overloaded method
        {
            usernameText.text = username;

            StoreObject avatarObject = UnityEngine.Resources.Load("StoreItems/Objects/" + avatarName,
                    typeof(StoreObject))
                as StoreObject;
            
            if(avatarObject != null)
                avatarImage.sprite = avatarObject.sprite;


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
        
        public void BuildChatBoard(string username, string message) // overloaded method
        {
            usernameText.text = username;
           


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
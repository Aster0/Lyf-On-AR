using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.Boards
{
    public class ChatBoard : MonoBehaviour
    {


        [SerializeField]
        private TextMeshProUGUI usernameText, messageText;


        [SerializeField] private Image image; 



        public void BuildChatBoard(string username, string message)
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
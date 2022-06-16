using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// GITHUB DOCUMENTATION: https://github.com/Aster0/Lyf-On-AR/issues/21
public class SendChat : MonoBehaviour
{

    private enum MessageType
    {
        DEFAULT,
        STICKER
    }

    [SerializeField]
    private MessageType messageType;

    [SerializeField] private Image image;
    
    private Button _button;

    [SerializeField]
    private TMP_InputField chatInput;
    

    private GameManager _gameManager;


    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnChatSend);

        _gameManager = GameManager.Instance;

  


    }
    

    private void OnChatSend()
    {

        
        string chatModifier;

        if (messageType == MessageType.STICKER) // if its a sticker message
        {
            chatModifier = "STICKER//" + image.sprite.name; // we place a STICKER// so we can know in the future.
            

            GameObject.Find("Stickers").SetActive(false); 
        }
        else  // normal message
        {
            
            if (chatInput.text.Length == 0) // empty
                return; // dont send empty text
            
            chatModifier = chatInput.text; // just input whatever we typed normally
        }
        
        // to update the firebase below
        
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("boards").Document(_gameManager.currentBoardName);


        
        
        Debug.Log("SENDING CHAT " + chatModifier + this.gameObject.name);

        _gameManager.currentChat = _gameManager.currentBoard[_gameManager.currentBoardName];
        
        Debug.Log("CHAT: " + _gameManager.currentBoardName);
       
        _gameManager.currentChat.Add(
            new Dictionary<string, object>()
            {
                {"contents", chatModifier},
                {"datePosted", System.DateTime.Today},
                {"user", GameManager.Instance.user.uuid}

            });
        
        Dictionary<string, List<object>> update = new Dictionary<string, 
            List<object>>
        {
            { "chats", _gameManager.currentChat}
        };
        docRef.SetAsync(update, SetOptions.MergeAll);


        ChatQuestEvent(); // for quest event handler
        
        if(messageType == MessageType.DEFAULT) // if its a text message (not sticker)
            chatInput.text = ""; // we reset the chat input to empty so they can easily type a new message without deleting the last.
    }


    private void ChatQuestEvent()
    {
        foreach (Quest quest in _gameManager.user.details.inProgressQuests) // loop through all in progress chats
        {
            if (quest.questType == Quest.QuestType.CHAT)
            {
                QuestAchievementManager.Instance.UpdateQuest(quest); // update the quest value as we have just chatted
                // GITHUB DOCUMENTATION: https://github.com/Aster0/Lyf-On-AR/issues/3
            }
        }
        
        _gameManager.user.UpdatePlayerDetails(_gameManager.user.details.inProgressQuests,
            User.UpdateType.QUEST); // update in firebase.
    }


}

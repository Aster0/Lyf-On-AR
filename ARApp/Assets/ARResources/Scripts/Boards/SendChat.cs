using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        if (messageType == MessageType.STICKER)
        {
            chatModifier = "STICKER//" + image.sprite.name;
            

            GameObject.Find("Stickers").SetActive(false);
        }
        else 
        {
            
            if (chatInput.text.Length == 0) // empty
                return; // dont send empty text
            
            chatModifier = chatInput.text;
        }
        
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


        ChatQuestEvent();
        
        if(messageType == MessageType.DEFAULT)
            chatInput.text = "";
    }


    private void ChatQuestEvent()
    {
        foreach (Quest quest in _gameManager.user.details.inProgressQuests)
        {
            if (quest.questType == Quest.QuestType.CHAT)
            {
                QuestAchievementManager.Instance.UpdateQuest(quest);
               
            }
        }
        
        _gameManager.user.UpdatePlayerDetails(_gameManager.user.details.inProgressQuests,
            User.UpdateType.QUEST);
    }


}

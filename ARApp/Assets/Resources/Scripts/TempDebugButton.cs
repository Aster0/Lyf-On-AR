using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.UI;

public class TempDebugButton : MonoBehaviour
{

    private GameManager _gameManager;

    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;

        _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonPress);
    }

    // Update is called once per frame
    private void ButtonPress()
    {
        _gameManager.user.UpdatePlayerDetails(999, User.UpdateType.POINTS);




        foreach (Quest quest in _gameManager.user.details.inProgressQuests)
        {
            quest.IsInProgress = false;
            quest.currentValue = 0;
        }

        _gameManager.user.details.ownedStickers.Clear();

   
        _gameManager.user.details.inProgressQuests.Clear();
        

        /*
        *
        *
        *  Connect to the firebase and update.
        */
                
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(_gameManager.user.uuid);
        
        Dictionary<string, object> update = new Dictionary<string, object>();



        update.Add("details", new Dictionary<string, object>()
        {
            {"quests", null},
            {"stickers", null},
        });



            
            
        docRef.SetAsync(update, SetOptions.MergeAll);
        
        
    }
}

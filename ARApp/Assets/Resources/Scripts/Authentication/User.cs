using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class User {

    // GITHUB DOCUMENTATION LINK: https://github.com/Aster0/Lyf-On-AR/issues/1



    public class Details
    {
        public int points { get; set; }
        public int exp { get; set; }
        public int level { get; set; }
        
        public int maxExp { get; set; }
        
        public StoreObject currentAvatar { get; set; }


        public List<StoreObject> ownedStickers { get; set; }
        public List<StoreObject> ownedAvatars { get; set; }
        
        public List<User> friends { get; set; }
        
        public List<Quest> inProgressQuests { get; set; }
        
        
    }
    
    
    public Details details { get; set; }
    
    public string emailAddress { get; set; }
    
    public string username { get; set; }
    
    public string uuid { get; set; }


    
    public enum UpdateType
    {
        POINTS,
        EXP,
        LEVEL,
        STICKER,
        QUEST,
        AVATAR,
        CURRENT_AVATAR,
        FRIENDS
    }

    
    // for saving ints
    public void UpdatePlayerDetails(int newValue, UpdateType updateType)
    {

        string updateField = "";
        
        /*
         *
         *  updateField checks which details field we are updating, whether it
         *  being points, EXP, level, etc. (makes it dynamic)
         */
        
        
        if (updateType == UpdateType.POINTS)
        {
            updateField = "points";
        }
        else if (updateType == UpdateType.QUEST)
        {
            updateField = "quests";
        }
        
        
        /*
         *
         *
         *  Connect to the firebase and update.
         */
                
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(uuid);
        
        Dictionary<string, object> update = new Dictionary<string, object>();
        
        
        
        update.Add("details", new Dictionary<string, object>()
        {
            
            {updateField, newValue}
        });
        
        docRef.SetAsync(update, SetOptions.MergeAll);
    }
    
    // for saving strings
    public void UpdatePlayerDetails(string newValue, UpdateType updateType)
    {

        string updateField = "";
        
        /*
         *
         *  updateField checks which details field we are updating, whether it
         *  being points, EXP, level, etc. (makes it dynamic)
         */
 
        if (updateType == UpdateType.CURRENT_AVATAR) // currently only current avatar updates a string
        {
            updateField = "current_avatar";
        }
        
        
        /*
         *
         *
         *  Connect to the firebase and update.
         */
                
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(uuid);
        
        Dictionary<string, object> update = new Dictionary<string, object>();
        
        
        
        update.Add("details", new Dictionary<string, object>()
        {
            
            {updateField, newValue}
        });
        
        docRef.SetAsync(update, SetOptions.MergeAll);
    }
    
    // for saving quests
    public void UpdatePlayerDetails(List<Quest> newValue, UpdateType updateType)
    {

        string updateField = "";
        
        /*
         *
         *  updateField checks which details field we are updating, whether it
         *  being points, EXP, level, etc. (makes it dynamic)
         */
        
        
        if (updateType == UpdateType.QUEST)
        {
            updateField = "quests";
        }
        
        
        /*
         *
         *
         *  Connect to the firebase and update.
         */
                
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(uuid);
        
        Dictionary<string, object> update = new Dictionary<string, object>();


        Dictionary<string, object> questDict = new Dictionary<string, object>();

        foreach (Quest quest in newValue)
        {
            string status = "IN_PROGRESS";

            if (quest.claimed)
            {
                status = "CLAIMED";
            }
            
            questDict.Add(quest.uid, new Dictionary<string, object>()
            {
                {"value", quest.currentValue },
                {"status", status },
                
            });
        }
        
        update.Add("details", new Dictionary<string, object>()
        {
            
            {updateField, questDict}
        });
        
        docRef.SetAsync(update, SetOptions.MergeAll);
    }

    
    public void UpdatePlayerDetails(StoreObject targetStoreObj, UpdateType updateType) // for store items
    {

      
        
        /*
         *
         *  updateField checks which details field we are updating, whether it
         *  being points, EXP, level, etc. (makes it dynamic)
         */



        Dictionary<string, List<object>> storeItemsDict = new Dictionary<string, List<object>>();

        List<object> storeItems = new List<object>();
        
        Debug.Log(targetStoreObj.name + " TARGET");
        
        string updateField = "stickers";

        List<StoreObject> playerStoreObjectList;

        if (updateType == UpdateType.AVATAR)
        {
            updateField = "avatars";
            playerStoreObjectList = this.details.ownedAvatars;
        }
        else
        {
            playerStoreObjectList = this.details.ownedStickers;
          
        }

        playerStoreObjectList.Add(targetStoreObj);
    
        foreach (StoreObject storeItem in playerStoreObjectList)
        {
            
            Debug.Log(storeItem.name);
        
            if(storeItem.price != 0) // dont add the free stickers to the database because
                // it might be subjected to changes in the future (free sticker rotations basically)
            
                storeItems.Add(storeItem.name);
        }
    
   
        /*
         *
         *  Below is for the structure of the firebase database.
         *
         *  Github Documentation Link:
         */
        storeItemsDict.Add(updateField, storeItems);

        
    /*
    *
    *
    *  Connect to the firebase and update.
    */
            
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(uuid);
    
        Dictionary<string, object> update = new Dictionary<string, object>();



        update.Add("details", storeItemsDict);



        
        
        docRef.SetAsync(update, SetOptions.MergeAll);
        
        
        
       
    }
    
    public void UpdatePlayerDetails(User targetUser, UpdateType updateType) // for adding friends
    {

      
        
        /*
         *
         *  updateField checks which details field we are updating, whether it
         *  being points, EXP, level, etc. (makes it dynamic)
         */



        Dictionary<string, List<object>> usersDict = new Dictionary<string, List<object>>();

        List<object> users = new List<object>();
        
        
        string updateField = "friends";

        List<User> friendsList = this.details.friends;


        friendsList.Add(targetUser);
    
    
        foreach (User user in friendsList)
        {
            
            users.Add(user.uuid);
        }
    
   
        /*
         *
         *  Below is for the structure of the firebase database.
         *
         *  Github Documentation Link:
         */
        usersDict.Add(updateField, users);

        
    /*
    *
    *
    *  Connect to the firebase and update.
    */
            
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(uuid);
    
        Dictionary<string, object> update = new Dictionary<string, object>();



        update.Add("details", usersDict);



        
        
        docRef.SetAsync(update, SetOptions.MergeAll);
        
        
        
       
    }




    
    
}

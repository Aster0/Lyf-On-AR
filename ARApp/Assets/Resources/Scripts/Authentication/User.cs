using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class User {





    public struct Details
    {
        public int points { get; set; }
        public int exp { get; set; }
        public int level { get; set; }
        
        public int maxExp { get; set; }


        public List<Sticker> ownedStickers { get; set; }
        
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
        QUEST
    }

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

    
    public void UpdatePlayerDetails(Sticker targetSticker, UpdateType updateType) // for stickers
    {

      
        
        /*
         *
         *  updateField checks which details field we are updating, whether it
         *  being points, EXP, level, etc. (makes it dynamic)
         */
        
        
        if (updateType == UpdateType.STICKER)
        {
          
            Dictionary<string, List<object>> stickersDict = new Dictionary<string, List<object>>();

            List<object> stickers = new List<object>();
            
            Debug.Log(targetSticker.name + " TARGET");

            this.details.ownedStickers.Add(targetSticker);
        
            foreach (Sticker sticker in this.details.ownedStickers)
            {
                
                Debug.Log(sticker.name);
            
                if(sticker.price != 0) // dont add the free stickers to the database because
                    // it might be subjected to changes in the future (free sticker rotations basically)
                    stickers.Add(sticker.name);
            }
        
       
            /*
             *
             *  Below is for the structure of the firebase database.
             *
             *  Github Documentation Link:
             */
            stickersDict.Add("stickers", stickers);

            
        /*
        *
        *
        *  Connect to the firebase and update.
        */
                
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            DocumentReference docRef = db.Collection("users").Document(uuid);
        
            Dictionary<string, object> update = new Dictionary<string, object>();



            update.Add("details", stickersDict);



            
            
            docRef.SetAsync(update, SetOptions.MergeAll);
        }
        
        
       
    }




    
    
}

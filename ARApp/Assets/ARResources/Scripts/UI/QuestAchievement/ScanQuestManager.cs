using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GITHUB DOCUMENTATION: https://github.com/Aster0/Lyf-On-AR/issues/3
public class ScanQuestManager : MonoBehaviour
{

    private GameManager _gameManager;


    [SerializeField]
    private string questUID;

    private GameObject arrowGuidance;
    private void Awake()
    {
        arrowGuidance = GameObject.Find("ArrowGuide");
        
    }
    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void ToggleArrowGuide(bool on)
    {
    
        arrowGuidance.SetActive(on);
    }
    
    public void OnScanImage()
    {
   
       
        foreach (Quest quest in _gameManager.user.details.inProgressQuests) // iterate all the in progress quest
        {
           
            if (quest.questType == Quest.QuestType.SCAN)
            {

                if (quest.uid.Equals(questUID)) // and find the current scan quest we're doing
                {
                    if (quest.currentValue < 1) // only if it's lower than one then we finish the quest.
                    {
                        QuestAchievementManager.Instance.UpdateQuest(quest); // show a prompt that the quest is completed
                        // GITHUB DOCUMENTATION: https://github.com/Aster0/Lyf-On-AR/issues/3
                        
                        _gameManager.user.UpdatePlayerDetails(_gameManager.user.details.inProgressQuests,
                            User.UpdateType.QUEST); // update via firebase.
                    }
                    
                }
               
               
            }
        }
    }
}

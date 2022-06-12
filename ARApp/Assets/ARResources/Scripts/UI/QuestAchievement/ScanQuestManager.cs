using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanQuestManager : MonoBehaviour
{

    private GameManager _gameManager;


    [SerializeField]
    private string questUID;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void OnScanImage()
    {
        foreach (Quest quest in _gameManager.user.details.inProgressQuests)
        {
            if (quest.questType == Quest.QuestType.SCAN)
            {

                if (quest.uid.Equals(questUID))
                {
                    if (quest.currentValue < 1) // only if it's lower than one then we finish the quest.
                    {
                        QuestAchievementManager.Instance.UpdateQuest(quest);
                        
                        _gameManager.user.UpdatePlayerDetails(_gameManager.user.details.inProgressQuests,
                            User.UpdateType.QUEST);
                    }
                    
                }
               
               
            }
        }
    }
}

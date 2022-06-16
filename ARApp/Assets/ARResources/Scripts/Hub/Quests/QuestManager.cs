using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// GITHUB DOCUMENTATION: https://github.com/Aster0/Lyf-On-AR/issues/3
public class QuestManager : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI titleText, currentValueText, 
        rewardValueText;

    [SerializeField]
    private Slider slider;

    [SerializeField] private Image mainIcon, rewardIcon;


    private Quest quest;

    private GameManager _gameManager;

    [SerializeField] private Button _button;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        
        _button.onClick.AddListener(OnButtonPress);
        
    }


    public void UpdateQuestVisual(Quest quest) // responsible for updating the prefab visual
    {
        titleText.text = quest.description;
        currentValueText.text = quest.currentValue.ToString() + "/" + quest.maxValue.ToString();

        rewardValueText.text = quest.reward.rewardAmount.ToString();

        mainIcon.sprite = quest.icon;
        rewardIcon.sprite = quest.reward.rewardIcon;

        slider.maxValue = quest.maxValue;

        slider.value = quest.currentValue;
    
        this.quest = quest;
    }


    public void OnButtonPress()
    {
        if (!quest.IsInProgress)
        {

            int questCount = 0;
            
            foreach (Quest quest in _gameManager.user.details.inProgressQuests)
            {
                if (!quest.claimed) // count the not claimed quests
                {
                    questCount++; // increase the quest count
                }
            }
            
            if (questCount >= 3)
            {
                ErrorPopupManager.GeneratePopup("Can't claim more than 3 quests!");

                return;
            }
          
      
            _gameManager.user.details.inProgressQuests.Add(quest); // add the quest into the currently in progress list
   
            quest.IsInProgress = true; // make it so we know this specific quest is currently in progress

            _gameManager.user.UpdatePlayerDetails(_gameManager.user.details.inProgressQuests,
                User.UpdateType.QUEST); // update firebase.
         


        
          


        }
        else
        {
            if (quest.currentValue >= quest.maxValue) // if its time to claim instead,
            {
                quest.reward.OnRewardClaim(); // we claim the reward
                
                // and reset the progress and set claim to true so we can't claim it again.
                quest.IsInProgress = false;
                quest.claimed = true;
                

                _gameManager.user.UpdatePlayerDetails(_gameManager.user.details.inProgressQuests,
                    User.UpdateType.QUEST); // update firebase.
                
                
                
            }
        }
        
        GameObject.FindObjectOfType<QuestPopulator>().PopulateQuests();
        
     
    }
}

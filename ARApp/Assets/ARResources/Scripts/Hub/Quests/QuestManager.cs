using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            if (_gameManager.user.details.inProgressQuests.Count >= 3)
            {
                ErrorPopupManager.GeneratePopup("Can't claim more than 3 quests!");

                return;
            }
          
      
            _gameManager.user.details.inProgressQuests.Add(quest);
   
            quest.IsInProgress = true;

            _gameManager.user.UpdatePlayerDetails(_gameManager.user.details.inProgressQuests,
                User.UpdateType.QUEST);
            Debug.Log(_gameManager.user.details.inProgressQuests.Count + " COUNT");


        
          


        }
        else
        {
            if (quest.currentValue >= quest.maxValue)
            {
                quest.reward.OnRewardClaim();
                
                quest.IsInProgress = false;
                quest.claimed = true;

                _gameManager.user.UpdatePlayerDetails(_gameManager.user.details.inProgressQuests,
                    User.UpdateType.QUEST);
                
            }
        }
        
        GameObject.FindObjectOfType<QuestPopulator>().PopulateQuests();
        
        Debug.Log(this.gameObject.name);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestAchievementManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;


    private Animator anim;
    
    
    public static QuestAchievementManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        anim = descriptionText.gameObject.transform.parent.GetComponent<Animator>();
        
        
    }

    private void ShowAchievement(string description)
    {
        
        descriptionText.gameObject.transform.parent.gameObject.SetActive(true);
        
        descriptionText.text = "Quest Completed: " + description;
        
  
        
        
        Invoke("CloseAchievement", 2);
    }


    private void CloseAchievement()
    {
        anim.SetBool("Achievement", true);
        
        Invoke("HideAchievement", 1);
    }


    private void HideAchievement()
    {
        descriptionText.gameObject.transform.parent.gameObject.SetActive(false);
    }


    public void UpdateQuest(Quest quest)
    {

        if (!quest.claimed) // still in progress, not claimed yet.
        {
            quest.currentValue += 1;

            if (quest.currentValue >= quest.maxValue)
            {
               this.ShowAchievement(quest.description);
            }
        }
         
        
        
    }
    
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    
    public static GameManager Instance { get; set; }
    private void Awake()
    {
        
    
        
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }



    public GameObject popupPrefab, chatBoardPrefab, chatBoardStickerPrefab, stickerPrefab, stickerStorePrefab, 
        questDefaultPrefab, questInProgressPrefab, questClaimPrefab, questClaimedPrefab;
    
    public User user { get; set; }


    
    public string currentBoardName { get; set; }
    public string previousScenePath { get; set; }
    public bool animateLoadingScreen { get; set; }
    public List<object> currentChat { get; set; }

    public Dictionary<string, List<object>> currentBoard = new Dictionary<string, List<object>>();
    
    
    public List<Quest> quests { get; set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ALL THE POPULATOR CLASS IS VERY SIMILAR TO HOW THE STORE IS POPULATED.

// STORE SYSTEM GITHUB: https://github.com/Aster0/Lyf-On-AR/issues/18

public class QuestPopulator : MonoBehaviour
{

    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        
        
        PopulateQuests();
        
        
        
    }

    public void PopulateQuests() // used to populate the hub's quest section.
    {


        foreach (Transform child in transform) // destroy all child contents first
        {
            Destroy(child.gameObject);
        }
        
        
        foreach (Quest quest in _gameManager.quests) // we iterate through all the existing scriptable object quests, read
        // https://github.com/Aster0/Lyf-On-AR/issues/3 for how the quest system works.
        {

            GameObject questObject;

            // then we check if and populate accordingly if its in progress, or not claimed, or done, etc.

            if (quest.IsInProgress)
            {
                questObject = Instantiate(GameManager.Instance.questInProgressPrefab, 
                    new Vector3(0,0),
                    Quaternion.identity);
            }
            else
            {
                questObject = Instantiate(GameManager.Instance.questDefaultPrefab, 
                    new Vector3(0,0),
                    Quaternion.identity);
            }
            
            if(quest.currentValue >= quest.maxValue)
            {
                questObject = Instantiate(GameManager.Instance.questClaimPrefab, 
                    new Vector3(0,0),
                    Quaternion.identity);
                
               
            }

            if (quest.claimed)
            {
                questObject = Instantiate(GameManager.Instance.questClaimedPrefab, 
                    new Vector3(0,0),
                    Quaternion.identity);
            }
                
            
            questObject.transform.SetParent(this.gameObject.transform, false);



            QuestManager questManager = questObject.GetComponent<QuestManager>();
            
            questManager.UpdateQuestVisual(quest); // updating the visual appearance of the quest icon.
        }
   
    }
}

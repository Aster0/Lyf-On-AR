using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPopulator : MonoBehaviour
{

    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        
        
        PopulateQuests();
        
        
        
    }

    public void PopulateQuests()
    {


        foreach (Transform child in transform) // destroy all child contents first
        {
            Destroy(child.gameObject);
        }
        
        
        foreach (Quest quest in _gameManager.quests)
        {

            GameObject questObject;


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
            
            questManager.UpdateQuestVisual(quest);
        }
   
    }
}

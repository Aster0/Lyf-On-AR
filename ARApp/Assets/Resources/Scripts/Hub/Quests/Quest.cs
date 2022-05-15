using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "LyfOn/Quest/New Quest", order = 1)]
public class Quest : ScriptableObject
{
    public Sprite icon;
    public int currentValue, maxValue;

    public new string name, description;

    public string uid;

    public Reward reward;


    public QuestType questType;
    public bool IsInProgress { get; set; }
    public bool claimed { get; set; }

    private void OnEnable()
    {
        IsInProgress = false;
        currentValue = 0;
    
        claimed = false;
    }

    public enum QuestType
    {
        SCAN,
        CHAT
    }
    
    
    
    /*
     *
     *
     *  Each quest will give one reward, this is the struct for the Reward so
     *  we can outline what to give.
     * 
     */
    
    [Serializable]
    public struct Reward
    {

        public Sprite rewardIcon;
        public RewardType rewardType;


        public int rewardAmount; // for pts & exp rewards
        public Sticker targetedStickerReward; // for sticker rewards
        
        public enum RewardType
        {
            EXP,
            POINTS,
            STICKER
        }


        public void OnRewardClaim()
        {
            if (rewardType == RewardType.EXP)
            {
                // give exp
                GameManager.Instance.user.UpdatePlayerDetails(
                    
                    GameManager.Instance.user.details.exp + rewardAmount, User.UpdateType.EXP);
            }
            else if (rewardType == RewardType.POINTS)
            {
                // give pts
                
                GameManager.Instance.user.UpdatePlayerDetails(
                    GameManager.Instance.user.details.points +
                    rewardAmount, User.UpdateType.POINTS);
            }
            else if (rewardType == RewardType.STICKER)
            {
                // give sticker
                
                Debug.Log(targetedStickerReward.name + " TARGETED STICKER");
                GameManager.Instance.user.UpdatePlayerDetails(targetedStickerReward,
                    User.UpdateType.STICKER);
                
                
            }
        }
        
    }
}

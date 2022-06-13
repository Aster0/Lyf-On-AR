using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserProfileManager : MonoBehaviour
{

    
    

    [SerializeField]
    private TextMeshProUGUI usernameText, expText, levelText, coinsText;

    
    [SerializeField]
    private Slider expSlider;


    [SerializeField] private Image avatarImage;

    private User user;

    private GameManager gameManager;
    



    [SerializeField]
    [Range(0, 100)]
    private int expMultiplier = 5;
    // Start is called before the first frame update
    void Start()
    {

        user = GameManager.Instance.user;
        
        gameManager = GameManager.Instance;
        
        usernameText.text = user.username;
        levelText.text = user.details.level.ToString();

        int maxExp = user.details.level * expMultiplier;

        expSlider.maxValue = maxExp;
        expSlider.value = user.details.exp;


        expText.text = expSlider.value + "/" + expSlider.maxValue;


        coinsText.text = user.details.points.ToString();


        UpdateAvatar();

    }

    public void UpdateAvatar()
    {
       
   
        if (user.details.currentAvatar != null)
        {
            avatarImage.sprite = user.details.currentAvatar.sprite;
        }
    }


}

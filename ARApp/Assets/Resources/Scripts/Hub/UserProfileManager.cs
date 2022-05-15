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


    private User user;



    [SerializeField]
    [Range(0, 100)]
    private int expMultiplier = 5;
    // Start is called before the first frame update
    void Start()
    {

        user = GameManager.Instance.user;
        
        usernameText.text = user.username;
        levelText.text = user.details.level.ToString();

        int maxExp = user.details.level * expMultiplier;

        expSlider.maxValue = maxExp;
        expSlider.value = user.details.exp;


        expText.text = expSlider.value + "/" + expSlider.maxValue;


        coinsText.text = user.details.points.ToString();

    }


}

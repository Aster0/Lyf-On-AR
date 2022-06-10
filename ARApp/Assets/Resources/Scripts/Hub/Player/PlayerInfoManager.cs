using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI userIDText;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        userIDText.text = "#" + gameManager.user.uuid;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

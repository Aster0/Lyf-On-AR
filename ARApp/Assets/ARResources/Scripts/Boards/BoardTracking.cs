using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTracking : MonoBehaviour
{

    private GameManager _gameManager;

    private GameObject arrowGuidance;
    private void Awake()
    {
        _gameManager = GameManager.Instance;
        
        arrowGuidance = GameObject.Find("ArrowGuide");
        
    }


    public string currentBoardName { get; set; }

    public void ToggleArrowGuide(bool on)
    {
    
        arrowGuidance.SetActive(on);
    }
    

    public void ChangeCurrentBoard(string boardName) // tracking which board we're currently on.
    {
        _gameManager.currentBoardName = boardName;
    }
}

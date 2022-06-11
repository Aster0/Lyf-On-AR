using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTracking : MonoBehaviour
{

    private GameManager _gameManager;

    private GameObject arrowGuidance;
    private void Start()
    {
        _gameManager = GameManager.Instance;
        
        arrowGuidance = GameObject.Find("ArrowGuide");
        
    }


    public string currentBoardName { get; set; }

    public void ToggleArrowGuide()
    {
        arrowGuidance.SetActive(!arrowGuidance.activeSelf);
    }
    

    public void ChangeCurrentBoard(string boardName)
    {
        _gameManager.currentBoardName = boardName;
    }
}

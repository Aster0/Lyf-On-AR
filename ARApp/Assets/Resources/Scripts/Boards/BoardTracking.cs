using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTracking : MonoBehaviour
{

    private GameManager _gameManager;
    private void Start()
    {
        _gameManager = GameManager.Instance;
        
    }


    public string currentBoardName { get; set; }

    

    public void ChangeCurrentBoard(string boardName)
    {
        _gameManager.currentBoardName = boardName;
    }
}

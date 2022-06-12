using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPopupManager : MonoBehaviour
{


    [SerializeField]
    private Button closeButton;
    
    [SerializeField]
    private TextMeshProUGUI messageText;

    
    public static void GeneratePopup(string message)
    {
        GameObject contentCanvas = GameObject.FindWithTag("ContentCanvas");
        GameObject newObject = Instantiate(GameManager.Instance.popupPrefab, 
            
             new Vector3(0, 0), Quaternion.identity);


        
     
        newObject.GetComponent<ErrorPopupManager>().SetMessage(message);
        
        newObject.transform.SetParent(contentCanvas.transform, false);
  
        
    }

    private void OnClose()
    {
        Destroy(this.gameObject);
    }


    private void Start()
    {
        closeButton.onClick.AddListener(OnClose);
    }

    private void SetMessage(string message)
    {
        messageText.text = message;
    }
}

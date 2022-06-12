using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenObjectButton : MonoBehaviour
{

    private Button _button;

    [SerializeField]
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnOpenSticker);
    }

    private void OnOpenSticker()
    {
        
        if(target.activeSelf)
            target.SetActive(false);
        else
            target.SetActive(true);
    }
}
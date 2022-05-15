using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMenuButton : MonoBehaviour
{
    private Button _button;


    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnBackToMenu);
    }

    private void OnBackToMenu()
    {
        CustomSceneManager.LoadScene("Resources/Scenes/Hub");
    }
}

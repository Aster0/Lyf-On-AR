using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartScanningButton : MonoBehaviour
{

    private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        CustomSceneManager.LoadScene("Resources/Scenes/ARScene");
    }
}

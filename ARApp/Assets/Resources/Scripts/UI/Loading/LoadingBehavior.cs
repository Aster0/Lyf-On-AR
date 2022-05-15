using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    
    [SerializeField]
    private GameObject _globeObject;
    [SerializeField]
    private TextMeshProUGUI loadingText;


    [SerializeField]
    private CanvasGroup _canvasGroup;



    private bool _previouSceneUnloaded = false;
    
    
    public string previousScenePath { get; set; }


    private string dots;


    private GameManager _gameManager;
    
    void Start()
    {
        StartCoroutine(animateLoading());
        StartCoroutine(animateLoadingText());
        
        _gameManager = GameManager.Instance;
        
        _gameManager.animateLoadingScreen = false;
        
    }


    private IEnumerator animateLoadingText()
    {

        while (true)
        {
           

            loadingText.text = "Loading" + dots;

            dots += ".";

            if (dots.Length >= 4)
            {
                dots = ".";
            }
            
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator animateLoading()
    {

        while (true)
        {
            _globeObject.transform.Rotate(0, 0, 4, Space.World);

            
            yield return new WaitForSeconds(0.05f);
        }
        
    }

    private void OnDestroy()
    {
        Debug.Log("Unloaded Loading");
    }

    private void Update()
    {
        BeginLoading();
    }

    public void BeginLoading()
    {
    
        if (_gameManager.animateLoadingScreen)
        {
            _canvasGroup.alpha -= Time.deltaTime * 5;

            if (_canvasGroup.alpha <= 0)
            {
                SceneManager.UnloadSceneAsync("Resources/Scenes/LoadingScreen");
            }


        }
        else
        {
            if (_canvasGroup.alpha != 1)
            {
              
                _canvasGroup.alpha += Time.deltaTime * 5;
            }
        }


        if (!_previouSceneUnloaded)
        {
            if (_canvasGroup.alpha >= 1)
            {
                SceneManager.UnloadSceneAsync(_gameManager.previousScenePath);
                _previouSceneUnloaded = true;
            }
        }
        
    }
}

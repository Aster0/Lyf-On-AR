using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenLoading : MonoBehaviour
{
    // Start is called before the first frame update

    
    [SerializeField]
    private GameObject _globeObject;
   

    [SerializeField]
    private CanvasGroup _canvasGroup, _canvasGroup2;



    private bool _previouSceneUnloaded = false;
    
    
    public string previousScenePath { get; set; }




    private bool animate;



    
    void Start()
    {
        StartCoroutine(animateLoading());
    
        


        
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
    
        if (animate) // finish animating in
        {
            _canvasGroup.alpha -= Time.deltaTime * 5;
            _canvasGroup2.alpha -= Time.deltaTime * 5;
            
            if (_canvasGroup.alpha <= 0)
            {
                SceneManager.LoadScene("Resources/Scenes/MainMenu");
            }


        }
        else
        {
            if (_canvasGroup.alpha != 1)
            {
              
                _canvasGroup.alpha += Time.deltaTime * 5;
                _canvasGroup2.alpha += Time.deltaTime * 5;
            }
            else
            {
                Invoke("SetAnimationToFade", 1.5f);
            }
        }


 
        
    }

    private void SetAnimationToFade()
    {
        animate = true;
    }
}

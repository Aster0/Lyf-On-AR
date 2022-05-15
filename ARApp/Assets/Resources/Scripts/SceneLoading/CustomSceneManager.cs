using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CustomSceneManager : MonoBehaviour
{
    


    

    public static async void LoadScene(string scenePath)
    {


        Time.timeScale = 1;



        string currentPath = SceneManager.GetActiveScene().path;
        
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync("Resources/Scenes/LoadingScreen", LoadSceneMode.Additive);




        
       
        
       
        AsyncOperation scene = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);

        
        scene.allowSceneActivation = false;

        while (scene.progress < 0.9f || loadingScene.progress < 0.9f)
        {
            
            await Task.Delay(100);
           


            
            
   
            
            
        }
        
        
        LoadingBehavior loadingBehavior = FindObjectOfType<LoadingBehavior>();

        
        //Debug.Log(loadingBehavior + " loading behavior");
      
        //loadingBehavior.previousScenePath = currentPath;

        GameManager.Instance.previousScenePath = currentPath;
   


      

        await Task.Delay(2000);
        
        //SceneManager.UnloadSceneAsync("Resources/Scenes/LoadingScreen");

        
        GameManager.Instance.animateLoadingScreen = true;
        
        scene.allowSceneActivation = true;

   
  
    }
    
    

  
}

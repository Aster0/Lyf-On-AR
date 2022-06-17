using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// GITHUB DOCUMENTATION: https://github.com/Aster0/Lyf-On-AR/issues/23
public class CustomSceneManager : MonoBehaviour
{
    


    

    public static async void LoadScene(string scenePath)
    {


        Time.timeScale = 1;



        string currentPath = SceneManager.GetActiveScene().path;
        
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync("Resources/Scenes/LoadingScreen", LoadSceneMode.Additive);
        // load a loading scene async, added on the current scene (ADDICTIVE load)



        
       
        
       
        AsyncOperation scene = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive); 
        // load the target scene async, added on the current scene (ADDICTIVE load)

        
        scene.allowSceneActivation = false;

        while (scene.progress < 0.9f || loadingScene.progress < 0.9f) // if the scene has not loaded finish
        {
            
            await Task.Delay(100); // delay to make the loading more believable.
           


            
            
   
            
            
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

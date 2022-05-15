using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using UnityEngine;

public class Test : MonoBehaviour
{

    private Firebase.Auth.FirebaseAuth auth; 
    
    // Start is called before the first frame update
    void Start()
    {
    
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

  
                auth.SignInWithEmailAndPasswordAsync("test@gmail.com", "123456").ContinueWithOnMainThread(task => {
                    if (task.IsCanceled) {
                        
               
                        Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
        
        
                     
                        return;
                    }
                    if (task.IsFaulted) {
                        
                  
                        Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
        
                     
                     
                        return;
                    }
            
        
                    Firebase.Auth.FirebaseUser newUser = task.Result;
                    Debug.LogFormat("User signed in successfully: {0} ({1})",
                        newUser.DisplayName, newUser.UserId);
                });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class LoginSessionManager : MonoBehaviour
{

    
    // GITHUB DOCUMENTATION LINK: https://github.com/Aster0/Lyf-On-AR/issues/1

    // SESSION MANAGER MANAGES THE LOGIN SESSION OF THE USER. 
    // ONCE THE USER LOGS IN, IT STORES ALL THE USER DETAILS FETCHED FROM FIREBASE LOCALLY WITHIN THE APPLICATION.

    private enum AuthenticationType // Whether it's registering or logging in.
    {
        
        REGISTER,
        LOGIN
    }



    private AuthenticationType _authenticationType; // determines the type of authentication
    private Firebase.Auth.FirebaseAuth auth;  // to store an instance of FirebaseAuth.


    

    [SerializeField]
    private TMP_InputField usernameField, passwordField, checkConfirmPasswordField;

    private GameObject _confirmPasswordGameObject, _backButton;

    [SerializeField] private Button loginButton, registerButton;

    private GameManager _gameManager;

    private void Awake()
    {
        // basic initializations
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }


    private void Start()
    {
        // basic initializations

        _gameManager = GameManager.Instance;
        _authenticationType = AuthenticationType.LOGIN;
        loginButton.onClick.AddListener(OnFinalized);
        
        
        
       
    }

    private void OnRegisterPress()
    {
        _confirmPasswordGameObject.SetActive(true);
        _backButton.SetActive(true);
    }

    private void OnFinalized() // when the login/register button is pressed by the user
    {


        bool checkConfirmPassword = false;

        if (_authenticationType == AuthenticationType.REGISTER) // if the user is trying to register
        {
            checkConfirmPassword = checkConfirmPasswordField.text.Length == 0; // if password length is 0, save true to checkConfirmPassword
        }

        if (usernameField.text.Length == 0 || passwordField.text.Length == 0 || checkConfirmPassword) // check if user and password fields are empty and confirm password.
        {
            ErrorPopupManager.GeneratePopup("Please fill in all blanks before proceeding."); // popup an error.
            return;
        }



        if (_authenticationType == AuthenticationType.LOGIN) // if user pressed login
        {
            auth.SignInWithEmailAndPasswordAsync(usernameField.text, passwordField.text).ContinueWithOnMainThread(task => {
               
                // we connect to firebase and try to login the user. Firebase will tell us if its incorrect or correct username & password.
                
                if (task.IsCanceled) {
                    
                
                    ErrorPopupManager.GeneratePopup("Username and or password was incorrect."); // if incorrect, popup message
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");


             
                    return;
                }
                if (task.IsFaulted) {
                
                    ErrorPopupManager.GeneratePopup("Username and or password was incorrect."); // if incorrect, popup message
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);

              
             
                    return;
                }
    

                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);

                
                CacheUserData(); // this is how we cache the user data locally
                
                     
                CustomSceneManager.LoadScene("Resources/Scenes/Hub"); // swap scenes
                
       



            });
        }
        
    }


    private void CacheUserData()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        
        Debug.Log("Done");
        
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        
        DocumentReference docRef = db.Collection("users").Document(user.UserId); // we loop the user document, targeting the logged in user's id
        docRef.Listen(snapshot => {
            
            Debug.Log("Callback received document snapshot.");
            Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));

            User user = new User(); // make a new instance of User object
            
            // and basically save all the details into the User Object. below.
            // we save things like quests, stickers, points, levels, etc.
            
            user.uuid = snapshot.Id;
            
            Dictionary<string, object> dict = snapshot.ToDictionary();

            user.username = dict["username"].ToString();
            
            user.emailAddress = dict["username"].ToString();
            user.username = dict["username"].ToString();


            User.Details details = new User.Details();

            Dictionary<string, object> detailsDict = dict["details"] 
                as Dictionary<string, object>;


            details.exp = int.Parse(detailsDict["exp"].ToString());

            details.points = int.Parse(detailsDict["points"].ToString());
            details.level = int.Parse(detailsDict["level"].ToString());


            details.ownedStickers = new List<Sticker>();

            
            try
            {
                List<object> stickerList = detailsDict["stickers"] as List<object>;

         

                foreach (string stickerName in stickerList)
                {
                    Sticker sticker = UnityEngine.Resources.Load("Stickers/StickersObject/" + stickerName,
                        typeof(Sticker)) as Sticker;
                
                    Debug.Log(sticker + " STICKER");
                    
                    details.ownedStickers.Add(sticker);
                }
                
                

            }
            catch (Exception e)
            {
                // no stickers found
            }

            Object[] stickers = 
                UnityEngine.Resources.LoadAll("Stickers/StickersObject/");

            foreach (Object obj in stickers)
            {

                Sticker sticker = (Sticker) obj;
                
                if(sticker.price == 0) 
                    details.ownedStickers.Add(sticker);
            }

            
            if (_gameManager.quests == null) // if it's the first time loading the quests,
                // we'll cache it from resources.
            {
                _gameManager.quests = new List<Quest>();

                object[] questObjects = UnityEngine.Resources.LoadAll("Quests");

                foreach (object obj in questObjects)
                {
                    Quest quest = (Quest) obj;
                
                    _gameManager.quests.Add(quest);
                
                }
            }

            details.inProgressQuests = new List<Quest>();
            
            try
            { 
                
                Dictionary<string, object> questDict = detailsDict["quests"] as Dictionary<string, object>;



                foreach (string questUID in questDict.Keys)
                {

           
                    Debug.Log(questUID + questDict[questUID] +  " DICT") ;
                    
                    foreach (Quest quest in _gameManager.quests)
                    {
                        if (questUID.Equals(quest.uid)) // if it matches the uid in the current
                        // quests, we set it to in progress because the player is currently using it.
                        {
                            Debug.Log("HIT");
                            quest.IsInProgress = true;

                            Dictionary<string, object> questValues = questDict[questUID] 
                                as Dictionary<string, object>;
                            
                            quest.currentValue = int.Parse(questValues["value"].ToString());

                            if (questValues["status"].ToString().Equals("CLAIMED"))
                            {
                                quest.claimed = true;
                            }

                            Debug.Log(quest.currentValue + " CURRENT VALUE");
                            details.inProgressQuests.Add(quest);
                            break;
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                    // NO QUESTS TAKEN
            }
            
          

            user.details = details;


            GameManager.Instance.user = user;
            
            
            //CustomSceneManager.LoadScene("Resources/Scenes/Hub");
        });
        
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.Firestore;
using Resources.Scripts.Boards;
using UnityEngine;

public class BoardManager : MonoBehaviour
{


    public string boardName;


    [SerializeField] private GameObject chatBoardPrefab;

    

    private GameManager _gameManager;
    private void Start()
    {
        _gameManager = GameManager.Instance;


        CacheBoard();


    }
    
    

    public void CacheBoard()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("boards").Document(boardName);
        docRef.Listen(snapshot => {




            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            
            
            Debug.Log("Callback received document snapshot.");
            Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));

       
            
            Dictionary<string, object> dict = snapshot.ToDictionary();


            List<object> chats = 
                dict["chats"] as List<object>;


            //_gameManager.currentChat = chats; // 10/04/2022

            _gameManager.currentBoard[boardName] = chats;

            if (chats.Count == 0)
            {
                return;
            }
            
            foreach (Dictionary<string, object> chat in chats)
            {


                GameObject gameObject = _gameManager.chatBoardPrefab;
                

                if (chat["contents"].ToString().StartsWith("STICKER//"))
                {
                    gameObject = _gameManager.chatBoardStickerPrefab;
                }
                
                
             
             
                GameObject chatBoardObject = Instantiate(gameObject, new Vector3(0,0),
                    Quaternion.identity);
            
                chatBoardObject.transform.SetParent(this.gameObject.transform, false);



                ChatBoard chatBoard = chatBoardObject.GetComponent<ChatBoard>();
                
                
                FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
                DocumentReference docRef = db.Collection("users").Document(
                    chat["user"].ToString());

                docRef.GetSnapshotAsync().ContinueWithOnMainThread((task) =>
                {

                    DocumentSnapshot snapshot = task.Result;
                    Dictionary<string, object> userDictionary = snapshot.ToDictionary();
                    
                    
                    chatBoard.BuildChatBoard(userDictionary["username"].ToString(),
                        chat["contents"].ToString());
                    
                });
         

                
                
                chatBoardObject.transform.localScale = new Vector3(1, 1, 1);
                chatBoardObject.transform.localPosition = new Vector3(0, 0, -13);
                chatBoardObject.transform.localRotation = Quaternion.Euler(new Vector3(0,0));
            }
                
     



        });
    }
    
}

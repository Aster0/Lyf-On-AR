using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuExpandButton : MonoBehaviour
{

    private Button _button;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private GameObject contents;

    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnMenuExpand);
    }

    private void OnMenuExpand()
    {
        if (!open)
        {
          
            open = true;


        }
        else
        {
        
            open = false;
        }
        
        anim.SetBool("Open", open);
        
        Invoke("UpdateContents", 0.3f);
    }

    private void UpdateContents()
    {
        foreach (Transform child in contents.transform)
        {
            child.gameObject.SetActive(open);   
        }
    }
}

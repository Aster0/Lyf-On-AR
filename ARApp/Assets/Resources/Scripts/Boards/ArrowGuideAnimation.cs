using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGuideAnimation : MonoBehaviour
{

    private CanvasGroup _canvasGroup;

    private float transparentFloat;

    private bool fade;
    // Start is called before the first frame update
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_canvasGroup.alpha <= 0.5)
        {
            fade = false;
        }
        else if(_canvasGroup.alpha >= 1)
        {
            fade = true;
        }

        if (fade)
        {
            transparentFloat -= Time.deltaTime;
        }
        else
        {
            transparentFloat += Time.deltaTime;
        }

        _canvasGroup.alpha = transparentFloat;
        Debug.Log(transparentFloat);
    }
}

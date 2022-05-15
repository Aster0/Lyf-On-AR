using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI pointsText;



    public void UpdatePointsText(int points)
    {
        pointsText.text = points.ToString();
    }
}

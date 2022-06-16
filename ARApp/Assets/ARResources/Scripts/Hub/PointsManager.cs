using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI pointsText;



    public void UpdatePointsText(int points) // this is used when the points need to be updated visually. nothing much.
    {
        pointsText.text = points.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour 
{
    public static int[] HighScoreArray = new int[7];
    public Text[] textArray = new Text[7];

    void Start() 
    {
        for (int i = 0; i < 7; i++) {
            textArray[i].text = HighScoreArray[i].ToString();
        }
    }
}

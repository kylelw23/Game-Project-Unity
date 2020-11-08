using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
	public TMP_Text textDisplay;
	public static int points;

    void Start()
    {
    	points = 0;   
    }

    void Update() {
    	UpdateScoreText();
    }

    public static void UpdateScoreboard() 
    {
    	int i = 6;
    	ScoreMenu.HighScoreArray[i] = points;
    	while ((i >= 1) && (ScoreMenu.HighScoreArray[i] > ScoreMenu.HighScoreArray[i-1]))
        {
            int t = ScoreMenu.HighScoreArray[i];
            ScoreMenu.HighScoreArray[i] = ScoreMenu.HighScoreArray[i - 1];
            ScoreMenu.HighScoreArray[i - 1] = t;
            i--;
        }
    }
    void UpdateScoreText() {
    	textDisplay.text = "Score: " + points;
    }
}

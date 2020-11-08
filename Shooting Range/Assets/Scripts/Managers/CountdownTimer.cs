using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
	private bool timerActive = false;
	public float timeStart = 60;
	public TMP_Text textDisplay;
    public TMP_Text finalScoreText;
    public GameObject congratsUI;

	private float timeRemaining;

    void Start()
    {
        timeRemaining = timeStart;
        timerActive = true;
    }

    void Update()
    {
        if (timerActive) 
        {
        	if (timeRemaining > 0.0f)
        	{
        		// Display current time
        		UpdateDisplay();

        		// Subtract time
        		timeRemaining -= Time.deltaTime;
        	}
        	else
        	{
        		EndRound();
        	}

        	// Play siren
        	if (timeRemaining >= 9.00f && timeRemaining <= 11.00f)
        	{
        		AudioManager.Instance.Play("Siren");
        	}
        }
    }

    public void PauseTimer()
    {
    	timerActive = !timerActive;
    }

    public void AddTime(float time)
    {
    	timeRemaining += time;
    }

    public void RemoveTime(float time)
    {
    	if (timeRemaining - time > 0)
    	{
    		timeRemaining = time;
    	}
    }

    void UpdateDisplay() {
    	float minutes = Mathf.FloorToInt(timeRemaining / 60);
    	float seconds = Mathf.FloorToInt(timeRemaining % 60);

    	textDisplay.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);
    }

    public void EndRound() {
        // Stop the timer
        timeRemaining = 0;
        timerActive = false;

        // Set the lock state to confined
        Cursor.lockState = CursorLockMode.Confined;

        // Update High Score
        ScoreManager.UpdateScoreboard();

        // Display the final score and update the scoreboard
        congratsUI.SetActive(true);
        int finalScore = ScoreManager.points;
        finalScoreText.text = "Final Score: " + finalScore;
        PauseMenu.GamePaused = true;
    }
}

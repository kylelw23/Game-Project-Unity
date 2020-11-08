using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartupManager : MonoBehaviour
{
    public TMP_Text startupText;
    public int remainSeconds;
    public GameObject timer;
    public GameObject startupUI;
    public GameObject healthUI;
    public GameObject staminaUI;
    public GameObject scoreUI;
    public PlayerController playerController;
    public AudioSource music;
    public GameObject enemySpawner;
    public static bool HasStarted;

    void Start()
    {
        HasStarted = false;    	
    	StartCoroutine(BeginStartup());
    }

    IEnumerator BeginStartup() {
        // Display countdown
    	for (int i = remainSeconds; i > 0; i--) {
    		if (i <= 5) {
    			AudioManager.Instance.Play("Beep");
    		}

    		UpdateStartupText(i);
    		yield return new WaitForSeconds(1f);
    	}

        // Once countdown is complete, activate all the necessary functions
        HasStarted = true;

        startupText.text = "FIRE!";
        timer.SetActive(true);
        healthUI.SetActive(true);
        staminaUI.SetActive(true);
        scoreUI.SetActive(true);
        enemySpawner.SetActive(true);
        music.Play();
        playerController.SetPlayerControl();

        yield return new WaitForSeconds(1);
        Destroy(startupUI);
    }

    void UpdateStartupText(int sec) {
    	startupText.text = "Starting in " + sec + " seconds.";
    }
}

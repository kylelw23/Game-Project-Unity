using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
	public TMP_Text healthText;
	public TMP_Text sprintText;
    public PlayerController playerController;

	private HealthManager healthManager;
	private SprintManager sprintManager;

    // Start is called before the first frame update
    void Start()
    {
        healthManager = playerController.GetComponent<HealthManager>();
        sprintManager = playerController.GetComponent<SprintManager>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HP: " + healthManager.GetHealth();
        sprintText.text = "Stamina: " + sprintManager.GetSprint();
    }
}

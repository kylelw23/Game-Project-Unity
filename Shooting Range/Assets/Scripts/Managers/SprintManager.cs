using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintManager : MonoBehaviour 
{
    private bool sprintActive = false;
    private float sprintBar = 100.0f;

    void FixedUpdate() {
        if (sprintActive) {
        	if (sprintBar < 100)
            {
                sprintBar++;
            }
        } else {
        	if (sprintBar > 0)
            {
                sprintBar--;
            }
        }
    }

    public float GetSprint() {
        return sprintBar;
    }

    public void SetSprintActive(bool isActive) 
    {
    	sprintActive = isActive;
    }
}

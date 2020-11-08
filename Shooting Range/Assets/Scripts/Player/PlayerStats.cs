using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Player Stats")]
public class PlayerStats : ScriptableObject 
{
    public float turnSpeed;
    public float jumpForce;
    public float sprintSpeed;
 	public float walkSpeed;
    public float crouchSpeed;
    public float minTurnAngle;
    public float maxTurnAngle;
}
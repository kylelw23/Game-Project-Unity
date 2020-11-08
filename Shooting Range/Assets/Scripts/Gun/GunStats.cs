using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Gun Stats")]
public class GunStats : ScriptableObject 
{
    public float damage;
	public float range;
	public float fireRate;
    public float hitForce;
	public float impactForce;
}
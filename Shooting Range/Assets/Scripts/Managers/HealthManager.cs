using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	public float health;
	public bool IsDead {
		get {
            return health <= 0;
		}
	}

    public float GetHealth() 
    {
        return health;
    }

    public virtual void ApplyDamage(float damage) 
    {
    	
    }

    public virtual void OnDeath() 
    {
        
    }
}

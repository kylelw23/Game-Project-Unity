using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : HealthManager
{
	// Object to instantiate upon dying
	public GameObject destroyedVersion;

    // Animator
    Animator animator;

	// Visual component for taking damage
	public ShakeInfo shakeInfo;
	private float tempShakeIntensity = 0;
	private bool shaking = false;

    // Navigation settings
    public float wanderTimer;
    public float wanderRadius;
    private NavMeshAgent agent;
    private Vector3 targetPos;

    private float timer;
	private Vector3 originalPos;
    private Quaternion originalRot;

    void Start() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;

        Debug.Log(agent.isOnNavMesh);

        if (agent.enabled && !agent.isOnNavMesh)
        {
            var position = transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(position, out hit, 10.0f, NavMesh.AllAreas);
            position = hit.position;
            agent.Warp(position);
        }
    }

	void Update() {
		// If true, perform shaking
		if (shaking) {
			//Calculate both linear and rotational displacement
            Vector3 movement = new Vector3(originalPos.x ,originalPos.y,originalPos.z + (Random.Range(-shakeInfo.shakeIntensity, shakeInfo.shakeIntensity) ));
            Vector3 rotation = new Vector3(originalRot.x, originalRot.y + (1 + Random.Range(-shakeInfo.shakeIntensity, 1 + shakeInfo.shakeIntensity)), originalRot.z);

            //Displace
            transform.position = movement;
            transform.Rotate(rotation);

            //Shake gets smaller as it continues
            tempShakeIntensity -= shakeInfo.shakeDelay;
		} 
        else 
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer) 
            {
                targetPos = RandomNavSphere(transform.position, wanderRadius, -1);
                
                if (agent.path.status == NavMeshPathStatus.PathComplete)
                {
                    animator.SetTrigger("Walk");
                    agent.SetDestination(targetPos);
                }
                timer = 0;
            }

            if (transform.position == targetPos) {
                animator.SetTrigger("Idle");
            }
        }
	}

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }    

    public override void ApplyDamage(float damage) {
        // Check if entity is already dead
        if (IsDead) return;

        // Perform shake
        Shake();

        // Otherwise, apply damage
        health -= damage;

        if (health <= 0)
        {
            OnDeath();
        }
    }

    public override void OnDeath()
    {
        // Alterations in spawn location due to object center missalignments
        Quaternion rotationAmount = Quaternion.Euler(-90, 0, 0);
        Vector3 positionAmount = new Vector3(0, 2, 0);

        //Instantiate the severed parts
        GameObject bodyParts = Instantiate(destroyedVersion, transform.position + positionAmount, transform.rotation * rotationAmount);
        
        //Re-scale due to object missalignment
        float scale = 10.0f;
        bodyParts.transform.localScale = transform.localScale * scale;

        //Destroy remaining game objects
        Destroy(gameObject);
        Destroy(transform.parent.gameObject);

        // Increase score
    	ScoreManager.points += 100;
    }

    // Visual representation of the target losing health as a result of taking damage
    void Shake() 
    {
    	// Reset max displacement of "shake" feature
    	tempShakeIntensity = shakeInfo.shakeIntensity;

    	// Commence coroutine
    	StartCoroutine("ShakeNow");
    }

    IEnumerator ShakeNow() 
    {
    	// Record pre-shake position
    	originalPos = transform.position;	// Modify with transform.parent.position if need be
        originalRot = transform.rotation;	// Modify with transform.parent.rotation if need be
        shaking = true;

    	///How long the target will be sh00k
        yield return new WaitForSeconds(.3f);

        //Return target to pre-shake position
        shaking = false;
        transform.position = originalPos;		// Modify with transform.parent.position if need be
        transform.rotation = originalRot;		// Modify with transform.parent.rotation if need be
    }
}
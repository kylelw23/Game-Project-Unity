using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
	public GunStats stats;
	public Camera gameCamera;
	public Transform gunEnd;
    public AudioManager audioManager;

    private Vector3 originalRotation;
    public Vector3 upRecoil;

	private LineRenderer laserLine;
	private float timeToFire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.localEulerAngles;
        laserLine = GetComponent<LineRenderer>();
        audioManager = AudioManager.Instance.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GamePaused || !StartupManager.HasStarted) 
        {
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= timeToFire) 
        {
        	timeToFire = Time.time + 1f  / stats.fireRate;
            StartCoroutine(ShotEffect());
        	Shoot();
            AddRecoil();
            audioManager.Play("GunSound");
        } else {
            StopRecoil();
        }
    }
    void Shoot() 
    {
    	// Initialise raycast hit
    	RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);

        Vector3 rayOrigin = gameCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

    	// If object has been hit by raycast, deal damage
    	if (Physics.Raycast(rayOrigin, gameCamera.transform.forward, out hit, stats.range)) 
        {
    		// Draw laser line
            laserLine.SetPosition(1, hit.point);

    		// If hit object has target component, deal damage
    		Target target = hit.transform.GetComponent<Target>();
            Chaser chaser = hit.transform.GetComponent<Chaser>();

            // Deal damage to target
    		if (target != null) 
            {
                target.ApplyDamage(stats.damage);
    		}

            if (chaser != null) 
            {
                chaser.ApplyDamage(stats.damage);
            }

            // Apply force to hit object
            if (hit.rigidbody != null) 
            {
                hit.rigidbody.AddForce(-hit.normal * stats.hitForce);
            }
    	} 
        else 
        {
            laserLine.SetPosition(1, rayOrigin + (gameCamera.transform.forward * stats.range));
        }
    }
    private IEnumerator ShotEffect() 
    {
        laserLine.enabled = true;
        yield return stats.fireRate;
        laserLine.enabled = false;
    }

    private void AddRecoil()
    {
        transform.localEulerAngles += upRecoil;
    }

    private void StopRecoil()
    {
        transform.localEulerAngles = originalRotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[Header("Enemy Spawn Management")]
	public float respawnDuration = 30f;
	public List<GameObject> spawnPoints = new List<GameObject>();
	public List<GameObject> enemyTypes = new List<GameObject>();

	private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyTypes.Capacity <= 0) {
        	Debug.Log("No enemies found!");
        }
        if (spawnPoints.Capacity <= 0) {
        	Debug.Log("No spawn points found!");
        }

        spawnTimer = respawnDuration;
    }

    // Update is called once per frame
    void Update()
    {
    	// Spawn new enemies after a certain period of time
        if (spawnTimer < respawnDuration) {
        	spawnTimer += Time.deltaTime;
        } else {
        	SpawnEnemy();
        }
    }

    void SpawnEnemy() {
    	if (spawnTimer < respawnDuration) return;

    	// Spawn enemies
    	if (enemyTypes.Capacity > 0) {
    		foreach (GameObject spawnPoint in spawnPoints) {
    			int rand = Random.Range(0, enemyTypes.Capacity);
    			GameObject enemy = enemyTypes[rand];

    			Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
    		}
    	}

    	// Reset timer
    	spawnTimer = 0f;
        respawnDuration = respawnDuration + (respawnDuration * 1.5f);
    }
}

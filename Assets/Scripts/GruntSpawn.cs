using UnityEngine;
using System.Collections;

public class GruntSpawn : MonoBehaviour {
	
	public GameObject gruntPrefab;
	public float maxSpawnTime = 5f;


	private float spawnTimer = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer += Time.fixedDeltaTime;
		if (spawnTimer > maxSpawnTime) {
			SpawnEnemy ();
			spawnTimer = 0f;
		}
	}

	void SpawnEnemy() {
        Vector3 spawnpos = transform.position;
        spawnpos.z += 2f;
		GameObject grunt = Instantiate (gruntPrefab, spawnpos, transform.rotation) as GameObject;
	}
}

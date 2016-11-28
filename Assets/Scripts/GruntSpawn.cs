using UnityEngine;
using System.Collections;

public class GruntSpawn : MonoBehaviour {
	
	public GameObject gruntPrefab;
	public float maxSpawnTime = 5f;
    public int health = 3;
    private float spawnTimer = 0f;
    public int maxSpawned = 1;
    int curSpawned;

	// Use this for initialization
	void Start () {
        curSpawned = 0;
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer += Time.fixedDeltaTime;
		if (spawnTimer > maxSpawnTime && curSpawned < maxSpawned) {
			SpawnEnemy ();
			spawnTimer = 0f;
		}
	}

    public void DecrementSpawned()
    {
        curSpawned--;
    }

	void SpawnEnemy() {
        Vector3 spawnpos = transform.position;
        spawnpos.z += 2f;
        curSpawned++;
		GameObject grunt = Instantiate (gruntPrefab, spawnpos, transform.rotation) as GameObject;
        grunt.transform.Find("Grunt").GetComponent<EnemyGrunt>().setSpawn(this);
	}
    

    void ShowDamage()
    {
        foreach (Transform child in this.transform)
            child.GetComponent<Renderer>().material.color = new Color(200f / 255f, 0f, 0f, 1f);
        Invoke("FinishDamage", 0.25f);
    }

    void FinishDamage()
    {
        foreach (Transform child in this.transform)
            child.GetComponent<Renderer>().material.color = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1f);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 10)
        {
            health--;
            ShowDamage();
            if (health <= 0)
            {
                DestroyIt();
            }
        }
    }

    void DestroyIt()
    {
        Destroy(gameObject);
    }
}

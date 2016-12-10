using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GruntSpawn : MonoBehaviour {
	
	public GameObject gruntPrefab;
	public float maxSpawnTime = 5f;
    public int health = 3;
    private float spawnTimer = 5f;
    public int maxSpawned = 1;
    int curSpawned;

	public GameObject popupNotificationPrefab;	
	private GameObject activePopup; //health bar
	private float maxHealth;

	// Use this for initialization
	void Start () {
        curSpawned = 0;
		ShowPopupNotification ("", true);
		UpdatePopupNotification ("", 1);
		maxHealth = health;
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
        if (transform.rotation == Quaternion.Euler(0, 0, 0))
            spawnpos.z += 2f;
        else if (transform.rotation == Quaternion.Euler(0, 90, 0))
            spawnpos.x += 2f;
        else if (transform.rotation == Quaternion.Euler(0, 180, 0))
            spawnpos.z -= 2f;
        else if (transform.rotation == Quaternion.Euler(0, 270, 0))
            spawnpos.x -= 2f;
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

	public void ShowPopupNotification(string txt, bool showBar = false) {
		Vector3 pos = transform.position;
		pos.y = 6;
		pos.z += 2;
		Destroy (activePopup);
		activePopup = Instantiate (popupNotificationPrefab, pos, popupNotificationPrefab.transform.rotation, transform.parent) as GameObject;
		activePopup.transform.FindChild ("Panel").FindChild ("Text").GetComponent<TextMesh> ().text = txt;
		activePopup.transform.FindChild ("Panel").FindChild ("Slider").gameObject.SetActive (showBar);
	}

	public void UpdatePopupNotification(string txt, float barVal = 0) {
		activePopup.transform.FindChild ("Panel").FindChild ("Text").GetComponent<TextMesh> ().text = txt;
		activePopup.transform.FindChild ("Panel").FindChild ("Slider").GetComponent<Slider> ().value = barVal;
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 10)
        {
            health--;
			UpdatePopupNotification ("", health / maxHealth);
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

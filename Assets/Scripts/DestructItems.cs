using UnityEngine;
using System.Collections;

public class DestructItems : MonoBehaviour {
    public int healthRegen;
    public float percentChance;
    public GameObject healthPowerPrefab;
    public GameObject DestroyedObject;
    public int health = 3;
    public bool crate = false;

    void ShowDamage() {
        GetComponent<Renderer>().material.color = new Color(200f / 255f, 0f, 0f, 1f);
        Invoke("FinishDamage", 0.25f);
    }

    void FinishDamage() {
        GetComponent<Renderer>().material.color = new Color(150f/255f, 150f / 255f, 150f / 255f, 1f);
    }

    void OnCollisionEnter(Collision col) {
		if (col.gameObject.layer == 10 || col.gameObject.tag == "Minotaur") {
            health--;
            ShowDamage();
			if (health <= 0 || col.gameObject.tag == "Minotaur") {
                DestroyBarrel();
                if (crate) {
                    GameObject.Find("Audio").transform.Find("CrateDestroy").GetComponent<AudioSource>().Play();
                } else {
                    GameObject.Find("Audio").transform.Find("VaseDestroy").GetComponent<AudioSource>().Play();
                }
            } else {
                if (crate) {
                    GameObject.Find("Audio").transform.Find("CrateHit").GetComponent<AudioSource>().Play();
                } else {
                    GameObject.Find("Audio").transform.Find("VaseHit").GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    void DestroyIt(){
        if (DestroyedObject != null)
        {
            if (DestroyedObject)
            {
                Instantiate(DestroyedObject, transform.position, transform.rotation);
            }
        }
		Destroy(gameObject);
	}

    void DestroyBarrel() {
        if (DestroyedObject != null)
        {
            Instantiate(DestroyedObject, transform.position, transform.rotation);
            if (Random.value < percentChance)
            {
                GameObject hp = Instantiate(healthPowerPrefab);
                hp.transform.position = transform.position;
                hp.GetComponent<HealthPowerup>().healthToRestore = healthRegen;
            }
        }
        Destroy(this.gameObject);
    }
}
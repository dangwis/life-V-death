using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {
    public int healthRegen;
    public float percentChance;
    public GameObject healthPowerPrefab;
    public GameObject DestroyedObject;
    public int health = 3;

    void ShowDamage() {
        GetComponent<Renderer>().material.color = new Color(200f / 255f, 0f, 0f, 1f);
        Invoke("FinishDamage", 0.25f);
    }

    void FinishDamage() {
        GetComponent<Renderer>().material.color = new Color(150f/255f, 150f / 255f, 150f / 255f, 1f);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.layer == 10) {
            health--;
            ShowDamage();
            if (health <= 0) {
                DestroyIt();
            }
            Debug.Log("test2");
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

    void DestoryBarrel() {
        if (DestroyedObject != null)
        {
            if (Random.value > percentChance)
            {
                Instantiate(DestroyedObject, transform.position, transform.rotation);
                GameObject hp = Instantiate(healthPowerPrefab);
                hp.transform.position = transform.position;
                hp.GetComponent<HealthPowerup>().healthToRestore = healthRegen;
            }
        }
        Destroy(this.gameObject);
    }
}
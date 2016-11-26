﻿using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {

    public int healthRegen;
    public float percentChance;
    public GameObject healthPowerPrefab;

    void OnCollisionEnter(Collision coll)
    {
        GameObject go = coll.gameObject;
        if(go.tag == "Arrow")
        {
			DestoryBarrel ();
        }
		if (go.tag == "Sword" || go.tag == "Hammer") {
			if (go.transform.parent.GetComponent<LifePlayer> ().attacking) {
				DestoryBarrel ();
			}
		}
        
    }

	void DestoryBarrel() {
		if (Random.value > percentChance)
		{
			GameObject hp = Instantiate(healthPowerPrefab);
			hp.transform.position = transform.position;
			hp.GetComponent<HealthPowerup>().healthToRestore = healthRegen;
		}
		Destroy(this.gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class PregameWall : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (GameManager.S.gameStart)
        {
            Destroy(this.gameObject);
        }
	}
}

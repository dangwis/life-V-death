using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectMode : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void toggleMonitors () {
		SetupCameras.multiDisplay = !SetupCameras.multiDisplay;
	}

	public void Players2 () {
		SetupCameras.PlayerCount = 2;
		gameObject.SetActive (false);
		SceneManager.LoadScene ("Game_Play");
	}

	public void Players3 () {
		SetupCameras.PlayerCount = 3;
		gameObject.SetActive (false);
		SceneManager.LoadScene ("Game_Play");
	}

	public void Players4 () {
		SetupCameras.PlayerCount = 4;
		gameObject.SetActive (false);
		SceneManager.LoadScene ("Game_Play");
	}
}

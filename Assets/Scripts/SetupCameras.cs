﻿using UnityEngine;
using System.Collections;

public class SetupCameras : MonoBehaviour {
	static public int PlayerCount = 2;
	static public bool multiDisplay = false;
    public Vector3 playerStart;
    public Vector3 deathCamStart;
    public Canvas deathUI;
	public Canvas lifeUI;

	public GameObject DeathCam, LifeCam, LifeObj;

	void Start () {
		GameObject Camera0 = (GameObject)Instantiate (DeathCam, deathCamStart, DeathCam.transform.rotation), Camera1, Camera2, Camera3, Player1, Player2, Player3;
		Canvas deathOverlay = Instantiate (deathUI);
		deathOverlay.worldCamera = Camera0.GetComponent<Camera> ();
		deathOverlay.planeDistance = 1;
		Camera0.name = "Camera 0";
		Canvas lifeOverlay = Instantiate (lifeUI);
		switch (PlayerCount) {
		case 2:
            Camera1 = Instantiate (LifeCam) as GameObject;
			Camera1.name = "Camera 1";

			Player1 = Instantiate (LifeObj, playerStart, LifeObj.transform.rotation) as GameObject;
			Camera1.GetComponent<FollowCam> ().FollowObject = Player1;

			Player1.name = "Player 1";
            Player1.GetComponent<LifePlayer>().playerNum = 1;

			lifeOverlay.worldCamera = Camera1.GetComponent<Camera> ();
			lifeOverlay.planeDistance = 1;
			lifeOverlay.GetComponent<LifeHUD> ().player = Player1.GetComponent<LifePlayer>();

			if (multiDisplay) {
				Camera0.GetComponent<Camera> ().targetDisplay = 0;
				Camera1.GetComponent<Camera> ().targetDisplay = 1;
				
				if (Display.displays.Length > 1) {
					Display.displays [1].Activate ();
				}
			} else {
				Camera0.GetComponent<Camera> ().rect = new Rect (0f, 0.2505f, 0.499f, 0.499f);
				Camera1.GetComponent<Camera> ().rect = new Rect (0.501f, 0.2505f, 0.499f, 0.499f);
			}

			break;
		case 3:
			Camera1 = Instantiate (LifeCam) as GameObject;
			Camera2 = Instantiate (LifeCam) as GameObject;

			Camera1.name = "Camera 1";
			Camera2.name = "Camera 2";

			Player1 = Instantiate (LifeObj, playerStart + Vector3.right, LifeObj.transform.rotation) as GameObject;
			Camera1.GetComponent<FollowCam> ().FollowObject = Player1;

			Player2 = Instantiate (LifeObj, playerStart + Vector3.right * 3f, Player1.transform.rotation) as GameObject;
			Camera2.GetComponent<FollowCam> ().FollowObject = Player2;

			Player1.name = "Player 1";
            Player1.GetComponent<LifePlayer>().playerNum = 1;
			Player2.name = "Player 2";
            Player2.GetComponent<LifePlayer>().playerNum = 2;

			lifeOverlay.worldCamera = Camera1.GetComponent<Camera> ();
			lifeOverlay.planeDistance = 1;
			lifeOverlay.GetComponent<LifeHUD> ().player = Player1.GetComponent<LifePlayer>();
			lifeOverlay = Instantiate (lifeUI);
			lifeOverlay.worldCamera = Camera2.GetComponent<Camera> ();
			lifeOverlay.planeDistance = 1;
			lifeOverlay.GetComponent<LifeHUD> ().player = Player2.GetComponent<LifePlayer>();

			if (multiDisplay) {
				Camera0.GetComponent<Camera> ().targetDisplay = 0;
				Camera1.GetComponent<Camera> ().targetDisplay = 1;
				Camera2.GetComponent<Camera> ().targetDisplay = 2;

				if (Display.displays.Length > 1) {
					Display.displays [1].Activate ();
				}
				if (Display.displays.Length > 2) {
					Display.displays [2].Activate ();
				}
			} else {
				Camera0.GetComponent<Camera> ().rect = new Rect (0f, 0.501f, 0.499f, 0.499f);
				Camera1.GetComponent<Camera> ().rect = new Rect (0.501f, 0.501f, 0.499f, 0.499f);
				Camera2.GetComponent<Camera> ().rect = new Rect (0.2505f, 0f, 0.499f, 0.499f);
			}
			break;
		case 4:
			Camera1 = Instantiate (LifeCam) as GameObject;
			Camera2 = Instantiate (LifeCam) as GameObject;
			Camera3 = Instantiate (LifeCam) as GameObject;

			Camera1.name = "Camera 1";
			Camera2.name = "Camera 2";
			Camera3.name = "Camera 3";

			Player1 = Instantiate (LifeObj, playerStart, LifeObj.transform.rotation) as GameObject;
			Camera1.GetComponent<FollowCam> ().FollowObject = Player1;

			Player2 = Instantiate (LifeObj, playerStart + Vector3.right * 3f, Player1.transform.rotation) as GameObject;
			Camera2.GetComponent<FollowCam> ().FollowObject = Player2;

			Player3 = Instantiate (LifeObj, playerStart + Vector3.right * 6f, Player2.transform.rotation) as GameObject;
			Camera3.GetComponent<FollowCam> ().FollowObject = Player3;

			Player1.name = "Player 1";
			Player1.GetComponent<LifePlayer> ().playerNum = 1;
			Player2.name = "Player 2";
			Player2.GetComponent<LifePlayer> ().playerNum = 2;
			Player3.name = "Player 3";
			Player3.GetComponent<LifePlayer> ().playerNum = 3;

			lifeOverlay.worldCamera = Camera1.GetComponent<Camera> ();
			lifeOverlay.planeDistance = 1;
			lifeOverlay.GetComponent<LifeHUD> ().player = Player1.GetComponent<LifePlayer>();
			lifeOverlay = Instantiate (lifeUI);
			lifeOverlay.worldCamera = Camera2.GetComponent<Camera> ();
			lifeOverlay.planeDistance = 1;
			lifeOverlay.GetComponent<LifeHUD> ().player = Player2.GetComponent<LifePlayer>();
			lifeOverlay = Instantiate (lifeUI);
			lifeOverlay.worldCamera = Camera3.GetComponent<Camera> ();
			lifeOverlay.planeDistance = 1;
			lifeOverlay.GetComponent<LifeHUD> ().player = Player3.GetComponent<LifePlayer>();

			if (multiDisplay) {
				Camera0.GetComponent<Camera> ().targetDisplay = 0;
				Camera1.GetComponent<Camera> ().targetDisplay = 1;
				Camera2.GetComponent<Camera> ().targetDisplay = 2;
				Camera3.GetComponent<Camera> ().targetDisplay = 3;

				if (Display.displays.Length > 1) {
					Display.displays [1].Activate ();
				}
				if (Display.displays.Length > 2) {
					Display.displays [2].Activate ();
				}
				if (Display.displays.Length > 3) {
					Display.displays [3].Activate ();
				}
			} else {
				Camera0.GetComponent<Camera> ().rect = new Rect (0f, 0.501f, 0.499f, 0.499f);
				Camera1.GetComponent<Camera> ().rect = new Rect (0.501f, 0.501f, 0.499f, 0.499f);
				Camera2.GetComponent<Camera> ().rect = new Rect (0f, 0f, 0.499f, 0.499f);
				Camera3.GetComponent<Camera> ().rect = new Rect (0.501f, 0f, 0.499f, 0.499f);
			}
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

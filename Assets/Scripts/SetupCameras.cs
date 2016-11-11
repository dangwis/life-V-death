﻿using UnityEngine;
using System.Collections;

public class SetupCameras : MonoBehaviour {
	static public int PlayerCount = 2;
	static public bool multiDisplay = false;
    public Vector3 playerStart;
    public Vector3 deathCamStart;

	public GameObject DeathCam, LifeCam, LifeObj;

	void Start () {
		GameObject Camera0 = (GameObject)Instantiate (DeathCam, deathCamStart, DeathCam.transform.rotation), Camera1, Camera2, Camera3, Player1, Player2, Player3;
		Camera0.name = "Camera 0";
		switch (PlayerCount) {
		case 2:
			Camera1 = Instantiate (LifeCam, Camera0.transform.position, Camera0.transform.rotation) as GameObject;
			Camera1.name = "Camera 1";

			Player1 = Instantiate (LifeObj, playerStart + Vector3.right * 3f, LifeObj.transform.rotation) as GameObject;
			Camera1.GetComponent<FollowCam> ().FollowObject = Player1;

			Player1.name = "Player 1";
            Player1.GetComponent<LifePlayer>().playerNum = 1;


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
			Camera1 = Instantiate (LifeCam, Camera0.transform.position, Camera0.transform.rotation) as GameObject;
			Camera2 = Instantiate (LifeCam, Camera1.transform.position, Camera1.transform.rotation) as GameObject;

			Camera1.name = "Camera 1";
			Camera2.name = "Camera 2";

			Player1 = Instantiate (LifeObj, playerStart + Vector3.right * 3f, LifeObj.transform.rotation) as GameObject;
			Camera1.GetComponent<FollowCam> ().FollowObject = Player1;

			Player2 = Instantiate (LifeObj, playerStart + Vector3.right * 6f, Player1.transform.rotation) as GameObject;
			Camera2.GetComponent<FollowCam> ().FollowObject = Player2;

			Player1.name = "Player 1";
            Player1.GetComponent<LifePlayer>().playerNum = 1;
			Player2.name = "Player 2";
            Player2.GetComponent<LifePlayer>().playerNum = 2;

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
			Camera1 = Instantiate (LifeCam, Camera0.transform.position, Camera0.transform.rotation) as GameObject;
			Camera2 = Instantiate (LifeCam, Camera1.transform.position, Camera1.transform.rotation) as GameObject;
			Camera3 = Instantiate (LifeCam, Camera2.transform.position, Camera2.transform.rotation) as GameObject;

			Camera1.name = "Camera 1";
			Camera2.name = "Camera 2";
			Camera3.name = "Camera 3";

			Player1 = Instantiate (LifeObj, playerStart + Vector3.right * 3f, LifeObj.transform.rotation) as GameObject;
			Camera1.GetComponent<FollowCam> ().FollowObject = Player1;

			Player2 = Instantiate (LifeObj, playerStart + Vector3.right * 6f, Player1.transform.rotation) as GameObject;
			Camera2.GetComponent<FollowCam> ().FollowObject = Player2;

			Player3 = Instantiate (LifeObj, playerStart + Vector3.right * 9f, Player2.transform.rotation) as GameObject;
			Camera3.GetComponent<FollowCam> ().FollowObject = Player3;

			Player1.name = "Player 1";
            Player1.GetComponent<LifePlayer>().playerNum = 1;
			Player2.name = "Player 2";
            Player2.GetComponent<LifePlayer>().playerNum = 2;
			Player3.name = "Player 3";
            Player3.GetComponent<LifePlayer>().playerNum = 3;

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

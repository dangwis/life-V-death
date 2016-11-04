using UnityEngine;
using System.Collections;

public class SetupCameras : MonoBehaviour {
	static public int PlayerCount = 2;
	static public bool multiDisplay = true;

	public GameObject DeathCam, LifeCam, LifeObj;

	void Start () {
		GameObject Camera1 = Instantiate (DeathCam), Camera2, Camera3, Camera4, Player2, Player3, Player4;
		Camera1.name = "Camera 1";

		switch (PlayerCount) {
		case 2:
			Camera2 = Instantiate (LifeCam, Camera1.transform.position, Camera1.transform.rotation) as GameObject;
			Camera2.name = "Camera 2";

			Player2 = Instantiate (LifeObj, LifeObj.transform.position + Vector3.right * 3f, LifeObj.transform.rotation) as GameObject;
			Camera2.GetComponent<FollowCam> ().FollowObject = Player2;

			Player2.name = "Player 2";

			if (multiDisplay) {
				Camera1.GetComponent<Camera> ().targetDisplay = 0;
				Camera2.GetComponent<Camera> ().targetDisplay = 1;
			} else {
				Camera1.GetComponent<Camera> ().rect = new Rect (0f, 0.2505f, 0.499f, 0.499f);
				Camera2.GetComponent<Camera> ().rect = new Rect (0.501f, 0.2505f, 0.499f, 0.499f);
			}

			break;
		case 3:
			Camera2 = Instantiate (LifeCam, Camera1.transform.position, Camera1.transform.rotation) as GameObject;
			Camera3 = Instantiate (LifeCam, Camera2.transform.position, Camera2.transform.rotation) as GameObject;

			Camera2.name = "Camera 2";
			Camera3.name = "Camera 3";

			Player2 = Instantiate (LifeObj, LifeObj.transform.position + Vector3.right * 3f, LifeObj.transform.rotation) as GameObject;
			Camera2.GetComponent<FollowCam> ().FollowObject = Player2;

			Player3 = Instantiate (LifeObj, Player2.transform.position + Vector3.right * 3f, Player2.transform.rotation) as GameObject;
			Camera3.GetComponent<FollowCam> ().FollowObject = Player3;

			Player2.name = "Player 2";
			Player3.name = "Player 3";

			if (multiDisplay) {
				Camera1.GetComponent<Camera> ().targetDisplay = 0;
				Camera2.GetComponent<Camera> ().targetDisplay = 1;
				Camera3.GetComponent<Camera> ().targetDisplay = 2;
			} else {
				Camera1.GetComponent<Camera> ().rect = new Rect (0f, 0.501f, 0.499f, 0.499f);
				Camera2.GetComponent<Camera> ().rect = new Rect (0.501f, 0.501f, 0.499f, 0.499f);
				Camera3.GetComponent<Camera> ().rect = new Rect (0.2505f, 0f, 0.499f, 0.499f);
			}
			break;
		case 4:
			Camera2 = Instantiate (LifeCam, Camera1.transform.position, Camera1.transform.rotation) as GameObject;
			Camera3 = Instantiate (LifeCam, Camera2.transform.position, Camera2.transform.rotation) as GameObject;
			Camera4 = Instantiate (LifeCam, Camera3.transform.position, Camera3.transform.rotation) as GameObject;

			Camera2.name = "Camera 2";
			Camera3.name = "Camera 3";
			Camera4.name = "Camera 4";

			Player2 = Instantiate (LifeObj, LifeObj.transform.position + Vector3.right * 3f, LifeObj.transform.rotation) as GameObject;
			Camera2.GetComponent<FollowCam> ().FollowObject = Player2;

			Player3 = Instantiate (LifeObj, Player2.transform.position + Vector3.right * 3f, Player2.transform.rotation) as GameObject;
			Camera3.GetComponent<FollowCam> ().FollowObject = Player3;

			Player4 = Instantiate (LifeObj, Player3.transform.position + Vector3.right * 3f, Player3.transform.rotation) as GameObject;
			Camera4.GetComponent<FollowCam> ().FollowObject = Player4;

			Player2.name = "Player 2";
			Player3.name = "Player 3";
			Player4.name = "Player 4";

			if (multiDisplay) {
				Camera1.GetComponent<Camera> ().targetDisplay = 0;
				Camera2.GetComponent<Camera> ().targetDisplay = 1;
				Camera3.GetComponent<Camera> ().targetDisplay = 2;
				Camera4.GetComponent<Camera> ().targetDisplay = 3;
			} else {
				Camera1.GetComponent<Camera> ().rect = new Rect (0f, 0.501f, 0.499f, 0.499f);
				Camera2.GetComponent<Camera> ().rect = new Rect (0.501f, 0.501f, 0.499f, 0.499f);
				Camera3.GetComponent<Camera> ().rect = new Rect (0f, 0f, 0.499f, 0.499f);
				Camera4.GetComponent<Camera> ().rect = new Rect (0.501f, 0f, 0.499f, 0.499f);
			}
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

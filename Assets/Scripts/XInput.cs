using UnityEngine;
using System.Collections;

public class XInput : MonoBehaviour {
	public static XInput x;
	public static string XboxA, XboxB, XboxX, XboxY, XboxLStickX, XboxLStickY, XboxRStickX, XboxRStickY, XboxRB, XboxLB, XboxStart, XboxBack, XboxRT, XboxLT;

	// Use this for initialization
	void Start () {
		x = this;
		// Setup Input
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
			XboxA = "Win_Xbox_A";
			XboxB = "Win_Xbox_B";
			XboxX = "Win_Xbox_X";
			XboxY = "Win_Xbox_Y";
			XboxLStickX = "Win_Xbox_LStick_X";
			XboxLStickY = "Win_Xbox_LStick_Y";
			XboxRStickX = "Win_Xbox_RStick_X";
			XboxRStickY = "Win_Xbox_RStick_Y";
			XboxRB = "Win_Xbox_RB";
			XboxLB = "Win_Xbox_LB";
			XboxStart = "Win_Xbox_Start";
			XboxBack = "Win_Xbox_Back";
			XboxRT = "Win_Xbox_RT";
			XboxLT = "Win_Xbox_LT";
		} else {
			XboxA = "Mac_Xbox_A";
			XboxB = "Mac_Xbox_B";
			XboxX = "Mac_Xbox_X";
			XboxY = "Mac_Xbox_Y";
			XboxLStickX = "Mac_Xbox_LStick_X";
			XboxLStickY = "Mac_Xbox_LStick_Y";
			XboxRStickX = "Mac_Xbox_RStick_X";
			XboxRStickY = "Mac_Xbox_RStick_Y";
			XboxRB = "Mac_Xbox_RB";
			XboxLB = "Mac_Xbox_LB";
			XboxStart = "Mac_Xbox_Start";
			XboxBack = "Mac_Xbox_Back";
			XboxRT = "Mac_Xbox_RT";
			XboxLT = "Mac_Xbox_LT";
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Debug Input
		//DebugInput();
	}

	// Get RT and LT Presses
	public bool RTDown() {
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
			if (Input.GetAxis (XboxRT) > 0.9) {
				return true;
			}
		} else {
			if (Input.GetAxis (XboxRT) > 0.8) {
				return true;
			}
		}

		return false;
	}

	public bool LTDown() {
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
			if (Input.GetAxis (XboxLT) > 0.9) {
				return true;
			}
		} else {
			if (Input.GetAxis (XboxLT) > 0.8) {
				return true;
			}
		}

		return false;
	}

	void DebugInput() {
		if (Input.GetButtonDown (XboxA)) {
			Debug.Log (XboxA);
		}
		if (Input.GetButtonDown (XboxB)) {
			Debug.Log (XboxB);
		}
		if (Input.GetButtonDown (XboxX)) {
			Debug.Log (XboxX);
		}
		if (Input.GetButtonDown (XboxY)) {
			Debug.Log (XboxY);
		}
		if (Input.GetButtonDown (XboxStart)) {
			Debug.Log (XboxStart);
		}
		if (Input.GetButtonDown (XboxBack)) {
			Debug.Log (XboxBack);
		}
		if (Input.GetButtonDown (XboxLB)) {
			Debug.Log (XboxLB);
		}
		if (Input.GetButtonDown (XboxRB)) {
			Debug.Log (XboxRB);
		}
		if (Input.GetAxis (XboxLStickX) != 0) {
			Debug.Log (XboxLStickX);
		}
		if (Input.GetAxis (XboxLStickY) != 0) {
			Debug.Log (XboxLStickY);
		}
		if (Input.GetAxis (XboxRStickX) != 0) {
			Debug.Log (XboxRStickX);
		}
		if (Input.GetAxis (XboxRStickY) != 0) {
			Debug.Log (XboxRStickY);
		}
		if (RTDown()) {
			Debug.Log (XboxRT);
		}
		if (LTDown()) {
			Debug.Log (XboxLT);
		}
	}
}

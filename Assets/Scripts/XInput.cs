using UnityEngine;
using System.Collections;

public class XInput : MonoBehaviour {
	public static XInput x;
	static string pXboxA, pXboxB, pXboxX, pXboxY, pXboxLStickX, pXboxLStickY, pXboxRStickX, pXboxRStickY, pXboxRB, pXboxLB, pXboxStart, pXboxBack, pXboxRT, pXboxLT;

	// Get proper button names
	static public string XboxA(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxA;
	}

	static public string XboxB(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxB;
	}

	static public string XboxX(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxX;
	}

	static public string XboxY(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxY;
	}

	static public string XboxLStickX(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxLStickX;
	}

	static public string XboxLStickY(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxLStickY;
	}

	static public string XboxRStickX(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxRStickX;
	}

	static public string XboxRStickY(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxRStickY;
	}

	static public string XboxRB(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxRB;
	}

	static public string XboxLB(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxLB;
	}

	static public string XboxStart(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxStart;
	}

	static public string XboxBack(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxBack;
	}

	static public string XboxRT(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxRT;
	}

	static public string XboxLT(int PlayerNum) {
		return "P" + PlayerNum + "_" + pXboxLT;
	}

	// Use this for initialization
	void Awake () {
		x = this;
		// Setup Input
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
			pXboxA = "Win_Xbox_A";
			pXboxB = "Win_Xbox_B";
			pXboxX = "Win_Xbox_X";
			pXboxY = "Win_Xbox_Y";
			pXboxLStickX = "Win_Xbox_LStick_X";
			pXboxLStickY = "Win_Xbox_LStick_Y";
			pXboxRStickX = "Win_Xbox_RStick_X";
			pXboxRStickY = "Win_Xbox_RStick_Y";
			pXboxRB = "Win_Xbox_RB";
			pXboxLB = "Win_Xbox_LB";
			pXboxStart = "Win_Xbox_Start";
			pXboxBack = "Win_Xbox_Back";
			pXboxRT = "Win_Xbox_RT";
			pXboxLT = "Win_Xbox_LT";
		} else {
			pXboxA = "Mac_Xbox_A";
			pXboxB = "Mac_Xbox_B";
			pXboxX = "Mac_Xbox_X";
			pXboxY = "Mac_Xbox_Y";
			pXboxLStickX = "Mac_Xbox_LStick_X";
			pXboxLStickY = "Mac_Xbox_LStick_Y";
			pXboxRStickX = "Mac_Xbox_RStick_X";
			pXboxRStickY = "Mac_Xbox_RStick_Y";
			pXboxRB = "Mac_Xbox_RB";
			pXboxLB = "Mac_Xbox_LB";
			pXboxStart = "Mac_Xbox_Start";
			pXboxBack = "Mac_Xbox_Back";
			pXboxRT = "Mac_Xbox_RT";
			pXboxLT = "Mac_Xbox_LT";
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Debug Input
		DebugInput();
	}

	// Get RT and LT Presses
	public bool RTDown(int PlayerNum) {
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
			if (Input.GetAxis (XboxRT(PlayerNum)) > 0.9) {
				return true;
			}
		} else {
			if (Input.GetAxis (XboxRT(PlayerNum)) > 0.8) {
				return true;
			}
		}

		return false;
	}

	public bool LTDown(int PlayerNum) {
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
			if (Input.GetAxis (XboxLT(PlayerNum)) > 0.9) {
				return true;
			}
		} else {
			if (Input.GetAxis (XboxLT(PlayerNum)) > 0.8) {
				return true;
			}
		}

		return false;
	}

	void DebugInput() {
		for (int i = 1; i < 4; i++) {
			if (Input.GetButtonDown (XboxA (i))) {
				Debug.Log (XboxA (i));
			}
			if (Input.GetButtonDown (XboxB (i))) {
				Debug.Log (XboxB (i));
			}
			if (Input.GetButtonDown (XboxX (i))) {
				Debug.Log (XboxX (i));
			}
			if (Input.GetButtonDown (XboxY (i))) {
				Debug.Log (XboxY (i));
			}
			if (Input.GetButtonDown (XboxStart (i))) {
				Debug.Log (XboxStart (i));
			}
			if (Input.GetButtonDown (XboxBack (i))) {
				Debug.Log (XboxBack (i));
			}
			if (Input.GetButtonDown (XboxLB (i))) {
				Debug.Log (XboxLB (i));
			}
			if (Input.GetButtonDown (XboxRB (i))) {
				Debug.Log (XboxRB (i));
			}
			if (Input.GetAxis (XboxLStickX (i)) != 0) {
				Debug.Log (XboxLStickX (i));
			}
			if (Input.GetAxis (XboxLStickY (i)) != 0) {
				Debug.Log (XboxLStickY (i));
			}
			if (Input.GetAxis (XboxRStickX (i)) != 0) {
				Debug.Log (XboxRStickX (i));
			}
			if (Input.GetAxis (XboxRStickY (i)) != 0) {
				Debug.Log (XboxRStickY (i));
			}
			if (RTDown (i)) {
				Debug.Log (XboxRT (i));
			}
			if (LTDown (i)) {
				Debug.Log (XboxLT (i));
			}
		}
	}
}

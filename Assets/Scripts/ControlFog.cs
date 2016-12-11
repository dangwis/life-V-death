using UnityEngine;
using System.Collections;

public class ControlFog : MonoBehaviour {
	public GameObject fogImage; 
	public GameObject canvas;
    public static bool fogCheck = false;

	// Use this for initialization
	void Start () {
		GameObject go;
		for (int i = 0; i < FogOfWar.xVal; i++) {
			for (int j = 0; j < FogOfWar.yVal; j++) {
				go = Instantiate (fogImage, this.transform) as GameObject;
				go.transform.localRotation = Quaternion.identity;
				//go.transform.position = Vector3.zero;
				go.transform.localPosition= Vector3.zero;
                //if (SetupCameras.multiDisplay) {
                    //go.transform.localScale = new Vector3(1f, 1f, 1f);
                //} else {
                    go.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
                //}
				go.name = "" + (i * 10 + j);
			}
		}
		PlaceFog ();
	}
	
	// Update is called once per frame
	void Update () {
        if (!fogCheck) return;

		for (int i = 0; i < FogOfWar.xVal; ++i) {
			for (int j = 0; j < FogOfWar.yVal; j++) {
				if (FogOfWar.fog [i, j]) {
					transform.Find ("" + (i * 10 + j)).gameObject.SetActive (false);
				}
			}
		}

        fogCheck = false;
	}

	public void PlaceFog() {
		GameObject go;
		int counter = 0;
		for (int i = 1; i < FogOfWar.xVal * 2; i += 2) {
			counter = 0;
			for (int j = FogOfWar.yVal * 2 - 1; j > 0; j -= 2) {
				int temp = (int)((float)(i) / 2) * 10 + counter;
				go = transform.Find ("" + temp).gameObject;
				Vector3 vec;
				vec.x = (float)(i) / (FogOfWar.xVal * 2) * GetComponent<RectTransform>().rect.width;
				vec.y = (float)(j) / (FogOfWar.yVal * 2) * GetComponent<RectTransform> ().rect.height;
				vec.z = 0;

				go.transform.localPosition = vec;
				counter++;
			}
		}
	}
}

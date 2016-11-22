using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FixRatio : MonoBehaviour {

	public void Fix() {
		GetComponent<AspectRatioFitter> ().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
		GetComponent<AspectRatioFitter> ().aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
		GetComponent<AspectRatioFitter> ().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
		GetComponent<ControlFog> ().PlaceFog ();
	}
}

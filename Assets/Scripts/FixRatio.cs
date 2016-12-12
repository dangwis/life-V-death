using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FixRatio : MonoBehaviour {
    AspectRatioFitter fitter;

    public void Start () {
        fitter = GetComponent<AspectRatioFitter>();
    }


	public void Fix() {
		fitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
		fitter.GetComponent<AspectRatioFitter> ().aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
		fitter.GetComponent<AspectRatioFitter> ().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
		fitter.GetComponent<ControlFog> ().PlaceFog ();
	}
}

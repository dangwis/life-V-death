using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	public GameObject        FollowObject; // The Point Of Interest of the CameraFollow script. - JB
	public Vector3          CameraOffset = new Vector3(0,1,-1);
	public float            EasingVal = 0.05f;
	public Rect 			GameBounds = new Rect(-10f, -10f, 20f, 20f);

	private Camera          cam;

	void Start () {
		cam = transform.GetComponent<Camera>();
		// Initially position the camera exactly over the poi - JB
		transform.position = FollowObject.transform.position + CameraOffset;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (FollowObject != null) {
			Vector3 p0, p1, p01;

			p0 = transform.position;
			p1 = FollowObject.transform.position + CameraOffset;

			p01 = (1 - EasingVal) * p0 + EasingVal * p1;

			p01.x = RoundToNearestPixel (p01.x, cam);
			p01.y = RoundToNearestPixel (p01.y, cam);

			transform.position = p01;
		}
	}

    public void setCullingMask(int playerNum)
    {
        if(cam == null)
        {
            cam = transform.GetComponent<Camera>();
        }
        if(playerNum == 1)
        {
            cam.cullingMask |= 1 << LayerMask.NameToLayer("Life1");
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Life2"));
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Life3"));
        }
        else if (playerNum == 2)
        {
            cam.cullingMask |= 1 << LayerMask.NameToLayer("Life2");
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Life1"));
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Life3"));
        }
        else if (playerNum == 3)
        {
            cam.cullingMask |= 1 << LayerMask.NameToLayer("Life3");
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Life2"));
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Life1"));
        }
        else
        {
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Life2"));
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Life1"));
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Life3"));
        }
    }

	// From https://www.reddit.com/r/Unity3D/comments/34ip2j/gaps_between_tiled_sprites_help/ - JB
	private float RoundToNearestPixel(float unityUnits, Camera viewingCamera)
	{
		float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) * unityUnits;
		valueInPixels = Mathf.Round(valueInPixels);
		float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
		return adjustedUnityUnits;
	}
}

using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    public CursorLockMode lockState;
    public Vector3 cursorPos;
    public GameObject deathCursor;
    public float movementSpeed;
    public Camera deathCam;

	// Use this for initialization
	void Start () {
        Cursor.lockState = lockState;
        Cursor.visible = false;
        deathCursor = Instantiate(deathCursor);
        deathCursor.transform.position = cursorPos;
        Debug.Log(deathCam.rect.x);
        Debug.Log(deathCam.rect.y);
        
    }
	
	// Update is called once per frame
	void Update () {
        //float x = Input.GetAxis("Mouse X");
        //float y = Input.GetAxis("Mouse Y");
        float x = 0, y = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            x = 0.15f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            x = -0.15f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            y = 0.15f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            y = -0.15f;
        }
        cursorPos = deathCursor.transform.position;
        
        if (x != 0) {
            cursorPos.x = cursorPos.x + x * movementSpeed;
        }
        if(y != 0)
        {
            cursorPos.z = cursorPos.z + y * movementSpeed;
        }

        if (IsOnDeathCam(cursorPos))
        {
            deathCursor.transform.position = cursorPos;
        }
        
        
	}

    bool IsOnDeathCam(Vector3 cursorPos)
    {
        Vector3 result = cursorPos;
        result.y = result.z;
        result = deathCam.WorldToViewportPoint(result);
        bool ret = true;
        Vector3 newCamPos;
        if (result.x >= 0.81f || result.x <= 0.19f)
        {
            float xDif = cursorPos.x - deathCursor.transform.position.x;
            newCamPos = deathCam.transform.position;
            newCamPos.x += xDif;
            deathCam.transform.position = newCamPos;
            deathCursor.transform.position = cursorPos;
            ret = false;
        }
        if(result.y >= 0.99f || result.y <= 0.01f)
        {
            float zDif = cursorPos.z - deathCursor.transform.position.z;
            newCamPos = deathCam.transform.position;
            newCamPos.z += zDif;
            deathCam.transform.position = newCamPos;
            deathCursor.transform.position = cursorPos;
            ret = false;
        }
        return ret;
    }
}

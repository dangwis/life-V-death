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
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        if (Input.GetKey(KeyCode.RightArrow))
        {
            
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
           
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
           
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
           
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
        bool ret = true;
        Vector3 newCamPos;
        if (SetupCameras.multiDisplay)
        {
            if (cursorPos.x > deathCam.transform.position.x + 7f || cursorPos.x < deathCam.transform.position.x - 7f)
            {
                float xDif = cursorPos.x - deathCursor.transform.position.x;
                newCamPos = deathCam.transform.position;
                newCamPos.x += xDif;
                deathCam.transform.position = newCamPos;
                deathCursor.transform.position = cursorPos;
                ret = false;
            }
            if (cursorPos.z > deathCam.transform.position.z + 5.5f || cursorPos.z < deathCam.transform.position.z - 5.5f)
            {
                float zDif = cursorPos.z - deathCursor.transform.position.z;
                newCamPos = deathCam.transform.position;
                newCamPos.z += zDif;
                deathCam.transform.position = newCamPos;
                deathCursor.transform.position = cursorPos;
                ret = false;
            }
        }
        else
        {
            if (cursorPos.x > deathCam.transform.position.x + 7f || cursorPos.x < deathCam.transform.position.x - 7f)
            {
                float xDif = cursorPos.x - deathCursor.transform.position.x;
                newCamPos = deathCam.transform.position;
                newCamPos.x += xDif;
                deathCam.transform.position = newCamPos;
                deathCursor.transform.position = cursorPos;
                ret = false;
            }
            if(cursorPos.z > deathCam.transform.position.z + 5.5f || cursorPos.z < deathCam.transform.position.z - 5.5f)
            {
                float zDif = cursorPos.z - deathCursor.transform.position.z;
                newCamPos = deathCam.transform.position;
                newCamPos.z += zDif;
                deathCam.transform.position = newCamPos;
                deathCursor.transform.position = cursorPos;
                ret = false;
            }
        }
        return ret;
    }
}

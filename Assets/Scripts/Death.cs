using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    public static Death S;

    public CursorLockMode lockState;
    public Vector3 cursorPos;
    public GameObject deathCursor;
    public float movementSpeed;
    public Camera deathCam;

    bool scrollingL, scrollingR, scrollingU, scrollingD;

	// Use this for initialization
	void Start () {
        S = this;
        Cursor.lockState = lockState;
        Cursor.visible = false;
        deathCursor = Instantiate(deathCursor);
        deathCursor.transform.position = cursorPos;
        Debug.Log(deathCam.rect.x);
        Debug.Log(deathCam.rect.y);
        scrollingL = false;
        scrollingR = false;
        scrollingU = false;
        scrollingD = false;
        
    }
	
	// Update is called once per frame
	void Update () {

        CheckClicks();

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Vector3 newCamPos = deathCam.transform.position;
        cursorPos = deathCursor.transform.position;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            newCamPos.x += 0.15f;
            cursorPos.x += 0.15f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newCamPos.x -= 0.15f;
            cursorPos.x -= 0.15f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            newCamPos.z += 0.15f;
            cursorPos.z += 0.15f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            newCamPos.z -= 0.15f;
            cursorPos.z -= 0.15f;
        }

        deathCursor.transform.position = cursorPos;
        deathCam.transform.position = newCamPos;
        
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

    void CheckClicks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(cursorPos);
            Ray ray = Camera.main.ScreenPointToRay(screenPoint);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Clickable")
                    hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    bool IsOnDeathCam(Vector3 cursorPos)
    {
        bool ret = true;
        Vector3 newCamPos;
        if (scrollingU)
        {
            if(cursorPos.z < deathCursor.transform.position.z)
            {
                scrollingU = false;
                return ret;
            }
            else
            {
                newCamPos = deathCam.transform.position;
                newCamPos.z += 0.15f;
                deathCam.transform.position = newCamPos;
                cursorPos.z = deathCam.transform.position.z + 5.3f;
                deathCursor.transform.position = cursorPos;
                return false;
            }
        }
        else if (scrollingD)
        {
            if (cursorPos.z > deathCursor.transform.position.z)
            {
                scrollingD = false;
                return ret;
            }
            else
            {
                newCamPos = deathCam.transform.position;
                newCamPos.z -= 0.15f;
                deathCam.transform.position = newCamPos;
                cursorPos.z = deathCam.transform.position.z - 5.3f;
                deathCursor.transform.position = cursorPos;
                return false;
            }
        }
        else if (scrollingL)
        {
            if (cursorPos.x > deathCursor.transform.position.x)
            {
                scrollingL = false;
                return ret;
            }
            else
            {
                newCamPos = deathCam.transform.position;
                newCamPos.x -= 0.15f;
                deathCam.transform.position = newCamPos;
                cursorPos.x = deathCam.transform.position.x - 6.7f;
                deathCursor.transform.position = cursorPos;
                return false;
            }
        }
        else if (scrollingR)
        {
            if (cursorPos.x < deathCursor.transform.position.x)
            {
                scrollingR = false;
                return ret;
            }
            else
            {
                newCamPos = deathCam.transform.position;
                newCamPos.x += 0.15f;
                deathCam.transform.position = newCamPos;
                cursorPos.x = deathCam.transform.position.x + 6.7f;
                deathCursor.transform.position = cursorPos;
                return false;
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
                if (cursorPos.x > deathCam.transform.position.x) scrollingR = true;
                else if (cursorPos.x < deathCam.transform.position.x) scrollingL = true;
            }
            if (cursorPos.z > deathCam.transform.position.z + 5.5f || cursorPos.z < deathCam.transform.position.z - 5.5f)
            {
                float zDif = cursorPos.z - deathCursor.transform.position.z;
                newCamPos = deathCam.transform.position;
                newCamPos.z += zDif;
                deathCam.transform.position = newCamPos;
                deathCursor.transform.position = cursorPos;
                ret = false;
                if (cursorPos.z > deathCam.transform.position.z) scrollingU = true;
                else if (cursorPos.z < deathCam.transform.position.z) scrollingD = true;
            }
        }
        return ret;
    }
}

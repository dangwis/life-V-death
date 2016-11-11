using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    public static Death S;

    public CursorLockMode lockState;
    public Vector3 cursorPos;
    public GameObject deathCursor;
    public float movementSpeed;
    public Camera deathCam;

    public bool scrollingL, scrollingR, scrollingU, scrollingD;

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
            }
            else
            {
                newCamPos = deathCam.transform.position;
                newCamPos.z += 0.15f;
                deathCam.transform.position = newCamPos;
                cursorPos.z = deathCam.transform.position.z + 5.3f;
                deathCursor.transform.position = cursorPos;
                ret = false;
            }
        }
        if (scrollingD)
        {          
            if (cursorPos.z > deathCursor.transform.position.z)
            {
                scrollingD = false;
            }
            else
            {
                newCamPos = deathCam.transform.position;
                newCamPos.z -= 0.15f;
                deathCam.transform.position = newCamPos;
                cursorPos.z = deathCam.transform.position.z - 5.3f;
                deathCursor.transform.position = cursorPos;
                ret = false;
            }
        }
        if (scrollingL)
        { 

            if (cursorPos.x > deathCursor.transform.position.x)
            {
                scrollingL = false;
            }
            else
            {
                newCamPos = deathCam.transform.position;
                newCamPos.x -= 0.15f;
                deathCam.transform.position = newCamPos;
                cursorPos.x = deathCam.transform.position.x - 6.7f;
                deathCursor.transform.position = cursorPos;
                ret = false;
            }
        }
        if (scrollingR)
        {
            if (cursorPos.x < deathCursor.transform.position.x)
            {
                scrollingR = false;
            }
            else
            {
                newCamPos = deathCam.transform.position;
                newCamPos.x += 0.15f;
                deathCam.transform.position = newCamPos;
                cursorPos.x = deathCam.transform.position.x + 6.7f;
                deathCursor.transform.position = cursorPos;
                ret = false;
            }
        }

        if (cursorPos.x > deathCam.transform.position.x + 7f)
        {
            ret = false;
            scrollingR = true;
        }
        if (cursorPos.x < deathCam.transform.position.x - 7f)
        {
            ret = false;
            scrollingL = true;
        }
        if (cursorPos.z > deathCam.transform.position.z + 5.5f)
        {
            ret = false;
            scrollingU = true;      
        }
        if (cursorPos.z < deathCam.transform.position.z - 5.5f)
        {
            ret = false;
            scrollingD = true;
        }
        return ret;
    }
}

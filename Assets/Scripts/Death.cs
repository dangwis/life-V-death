using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    public static Death S;

    public CursorLockMode lockState;
    public Vector3 cursorPos;
    public GameObject deathCursor;
    public GameObject skeletonPrefab;
    public GameObject damageTrapPrefab;
    public GameObject placementObjPrefab;
    public Material ableToPlace, notAbleToPlace;
    public float movementSpeed;
    public Camera deathCam;

    bool place;
    GameObject placement;
    AbilityType activeAbility;
    Placing currentPlacing;

    public bool scrollingL, scrollingR, scrollingU, scrollingD;

    public enum AbilityType
    {
        Interact,
        Place
    }

    public enum Placing
    {
        Skeleton,
        Minotaur,
        Damage,
        Slow,
        Teleport
    }

	// Use this for initialization
	void Start () {
        S = this;
        Cursor.lockState = lockState;
        Cursor.visible = false;
        deathCursor = Instantiate(deathCursor);
        Vector3 tempPos = GameObject.Find("Static Objects").GetComponent<SetupCameras>().deathCamStart;
        tempPos.y = 5.6f;
        cursorPos = tempPos;
        deathCursor.transform.position = cursorPos;
        activeAbility = AbilityType.Interact;
        scrollingL = false;
        scrollingR = false;
        scrollingU = false;
        scrollingD = false;
        
    }
	
	// Update is called once per frame
	void Update () {

        ChooseAbility();
        if(activeAbility == AbilityType.Place)
        {
            ShowPlacement();
        }
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
            DeathCursor.S.OnClick();
            if (activeAbility == AbilityType.Interact)
            {
                Vector3 screenPoint = Camera.main.WorldToScreenPoint(cursorPos);
                Ray ray = Camera.main.ScreenPointToRay(screenPoint);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Clickable") {
                        hit.collider.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        hit.collider.gameObject.transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
            else if(activeAbility == AbilityType.Place)
            {
                if (place)
                {
                    if(currentPlacing == Placing.Damage)
                    {
                        GameObject trap = Instantiate(damageTrapPrefab);
                        trap.transform.position = placement.transform.position;
                        Destroy(placement.gameObject); ;
                        activeAbility = AbilityType.Interact;
                    }
                    else if(currentPlacing == Placing.Skeleton)
                    {
                        EnemySkel skel = Instantiate(skeletonPrefab).GetComponent<EnemySkel>();
                        skel.transform.position = placement.transform.position + new Vector3(0,1.6f,0);
                        Destroy(placement.gameObject); ;
                        activeAbility = AbilityType.Interact;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DeathCursor.S.OnRelease();
        }
    }

    void ShowPlacement()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(cursorPos);
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 pos = hit.collider.gameObject.transform.position;
            pos.y += 1;
            placement.transform.position = pos;

            if (hit.collider.tag == "Floor")
            {
                placement.GetComponent<Renderer>().material = ableToPlace;
                place = true;
            }
            else
            {
                placement.GetComponent<Renderer>().material = notAbleToPlace;
                place = false;
            }
        }
    }

    void ChooseAbility()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeAbility = AbilityType.Interact;
            if (placement != null) {
                Destroy(placement);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (activeAbility == AbilityType.Interact) {
                placement = Instantiate(placementObjPrefab);
            }
            activeAbility = AbilityType.Place;
            currentPlacing = Placing.Damage;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (activeAbility == AbilityType.Interact) {
                placement = Instantiate(placementObjPrefab);
            }
            activeAbility = AbilityType.Place;
            currentPlacing = Placing.Skeleton;
        }
    }

    bool IsOnDeathCam(Vector3 cursorPos)
    {
        bool ret = true;
        Vector3 newCamPos;
        if (scrollingU)
        {
            if (Input.GetAxis("Mouse Y") < 0)
            {
                scrollingU = false;
            }
            else
            {
                newCamPos = deathCam.transform.position;
                newCamPos.z += 0.15f;
                deathCam.transform.position = newCamPos;
                cursorPos.z = deathCam.transform.position.z + 5.4f;
                deathCursor.transform.position = cursorPos;
                ret = false;
            }
        }
        if (scrollingD)
        {
            if (Input.GetAxis("Mouse Y") > 0)
            {
                scrollingD = false;
            }
            else
            {
                newCamPos = deathCam.transform.position;
                newCamPos.z -= 0.15f;
                deathCam.transform.position = newCamPos;
                cursorPos.z = deathCam.transform.position.z - 5f;
                deathCursor.transform.position = cursorPos;
                ret = false;
            }
            
        }
        if (scrollingL)
        {

            if (Input.GetAxis("Mouse X") > 0)
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
            if (Input.GetAxis("Mouse X") < 0)
            {
                scrollingR = false;
            }
            else
            {
                newCamPos = deathCam.transform.position;
                newCamPos.x += 0.15f;
                deathCam.transform.position = newCamPos;
                cursorPos.x = deathCam.transform.position.x + 6.5f;
                deathCursor.transform.position = cursorPos;
                ret = false;
            }
        }

        if (cursorPos.x > deathCam.transform.position.x + 6.5f)
        {
            ret = false;
            scrollingR = true;
        }
        if (cursorPos.x < deathCam.transform.position.x - 6.7f)
        {
            ret = false;
            scrollingL = true;
        }
        if (cursorPos.z > deathCam.transform.position.z + 5.4f)
        {
            ret = false;
            scrollingU = true;      
        }
        if (cursorPos.z < deathCam.transform.position.z - 5f)
        {
            ret = false;
            scrollingD = true;
        }
        return ret;
    }
}

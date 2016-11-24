using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

    public static Death S;

    public CursorLockMode lockState;
    public Vector3 cursorPos;
    public GameObject deathCursor;
    public GameObject skeletonPrefab;
    public GameObject damageTrapPrefab;
    public GameObject trapPlacementPrefab;
    public GameObject enemyPlacementPrefab;
    public GameObject minotaurPrefab;
    public GameObject teleporterPrefab;
    public Material ableToPlace, notAbleToPlace;
    public Material firstTeleporterPlace;
    public float movementSpeed;
    public Camera deathCam;
    public float totalMana;
    public float timeToRegen, manaRegenRate;
    public float manaLeft;
    float timeSinceLastUse;
    public float _1080pWidth, _1080pHeight;
    public float scrollSpeed;

    Vector3 teleportIntermediary;

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
        Teleport1,
        Teleport2
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
        manaLeft = totalMana;
        timeSinceLastUse = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

        ChooseAbility();
        if(activeAbility == AbilityType.Place)
        {
            ShowPlacement();
        }
        CheckClicks();
        RegenMana();

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Vector3 newCamPos = deathCam.transform.position;
        cursorPos = deathCursor.transform.position;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            newCamPos.x += scrollSpeed;
            cursorPos.x += scrollSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            newCamPos.x -= scrollSpeed;
            cursorPos.x -= scrollSpeed;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            newCamPos.z += scrollSpeed;
            cursorPos.z += scrollSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            newCamPos.z -= scrollSpeed;
            cursorPos.z -= scrollSpeed;
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

    void RegenMana()
    {
        if(Time.time - timeSinceLastUse > timeToRegen)
        {
            if(manaLeft < totalMana)
            {
                manaLeft += manaRegenRate;
            }
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
                        hit.collider.gameObject.GetComponent<FallingBlock>().DropBlocks();
                    }
                }
            }
            else if(activeAbility == AbilityType.Place)
            {
                if (place)
                {
                    if(currentPlacing == Placing.Damage)
                    {
                        if (manaLeft >= 25f)
                        {
                            UseMana(25f);
                            GameObject trap = Instantiate(damageTrapPrefab);
                            trap.transform.position = placement.transform.position;
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                        }
                        else
                        {
                            //show that not enough mana somehow
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                        }
                        
                    }
                    else if(currentPlacing == Placing.Skeleton)
                    {
                        if (manaLeft >= 35)
                        {
                            UseMana(35f);
                            EnemySkel skel = Instantiate(skeletonPrefab).GetComponent<EnemySkel>();
                            skel.transform.position = placement.transform.position + new Vector3(0, 1.6f, 0);
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                        }
                        else
                        {
                            //show that not enough mana
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                        }
                    }
                    else if(currentPlacing == Placing.Minotaur)
                    {
                        if (manaLeft >= 40)
                        {
                            UseMana(40f);
                            EnemyMin min = Instantiate(minotaurPrefab).GetComponent<EnemyMin>();
                            min.transform.position = placement.transform.position;
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                        }
                        else
                        {
                            //show that not enough mana
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                        }
                    }
                    else if(currentPlacing == Placing.Teleport1)
                    {
                        teleportIntermediary = placement.transform.position;
                        currentPlacing = Placing.Teleport2;
                    }
                    else if(currentPlacing == Placing.Teleport2)
                    {
                        if (manaLeft >= 40)
                        {
                            UseMana(40);
                            TeleportPad tp = Instantiate(teleporterPrefab).GetComponent<TeleportPad>();
                            tp.transform.position = teleportIntermediary;
                            tp.endingUpPosition = placement.transform.position;
                            activeAbility = AbilityType.Interact;
                            Destroy(placement.gameObject);
                        }
                        else
                        {
                            //show that not enough mana
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                        }
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DeathCursor.S.OnRelease();
        }
    }

    void UseMana(float manaCost)
    {
        manaLeft -= manaCost;
        timeSinceLastUse = Time.time;

    }

    void ShowPlacement()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(cursorPos);
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 pos = hit.collider.gameObject.transform.position;
            pos.y += 0.5f;
            placement.transform.position = pos;
            if (hit.collider.tag == "Floor" && NotNearTag(placement, "Life", 3f) && NotNearTag(placement, "Trap", 0.5f))
            {    
                    if (currentPlacing == Placing.Teleport1)
                    {
                        placement.GetComponent<Renderer>().material = firstTeleporterPlace;
                    }
                    else if(currentPlacing == Placing.Damage)
                    {
                        placement.transform.Find("Needle").GetComponent<Renderer>().material = ableToPlace;
                        placement.transform.Find("Trap_Needle").GetComponent<Renderer>().material = ableToPlace;
                    }
                    else
                    {
                        placement.GetComponent<Renderer>().material = ableToPlace;
                    }
                    place = true;
            }
            else
            {
                if (currentPlacing == Placing.Damage)
                {
                    placement.transform.Find("Needle").GetComponent<Renderer>().material = notAbleToPlace;
                    placement.transform.Find("Trap_Needle").GetComponent<Renderer>().material = notAbleToPlace;
                }
                else
                {
                    placement.GetComponent<Renderer>().material = notAbleToPlace;
                }
                place = false;
            }
        }
    }


    bool NotNearTag(GameObject place, string tag, float distance)
    {
        Collider[] tagged = Physics.OverlapSphere(place.transform.position, distance);
        for (int i = 0; i < tagged.Length; i++)
        {
            if (tagged[i].tag == tag)
            {
                return false;
            }
        }
        return true;
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
                placement = Instantiate(trapPlacementPrefab);
            }
            else
            {
                Destroy(placement.gameObject);
                placement = Instantiate(trapPlacementPrefab);
            }
            activeAbility = AbilityType.Place;
            currentPlacing = Placing.Damage;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (activeAbility == AbilityType.Interact) {
                placement = Instantiate(enemyPlacementPrefab);
            }
            else
            {
                Destroy(placement.gameObject);
                placement = Instantiate(enemyPlacementPrefab);
            }
            activeAbility = AbilityType.Place;
            currentPlacing = Placing.Skeleton;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (activeAbility == AbilityType.Interact)
            {
                placement = Instantiate(enemyPlacementPrefab);
            }
            else
            {
                Destroy(placement.gameObject);
                placement = Instantiate(enemyPlacementPrefab);
            }
            activeAbility = AbilityType.Place;
            currentPlacing = Placing.Minotaur;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (activeAbility == AbilityType.Interact)
            {
                placement = Instantiate(enemyPlacementPrefab);
            }
            else
            {
                Destroy(placement.gameObject);
                placement = Instantiate(enemyPlacementPrefab);
            }
            activeAbility = AbilityType.Place;
            currentPlacing = Placing.Teleport1;
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
                newCamPos.z += scrollSpeed;
                deathCam.transform.position = newCamPos;
                cursorPos.z = deathCam.transform.position.z + _1080pHeight;
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
                newCamPos.z -= scrollSpeed;
                deathCam.transform.position = newCamPos;
                cursorPos.z = deathCam.transform.position.z - _1080pHeight + 0.3f;
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
                newCamPos.x -= scrollSpeed;
                deathCam.transform.position = newCamPos;
                cursorPos.x = deathCam.transform.position.x - _1080pWidth;
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
                newCamPos.x += scrollSpeed;
                deathCam.transform.position = newCamPos;
                cursorPos.x = deathCam.transform.position.x + _1080pWidth - 0.4f;
                deathCursor.transform.position = cursorPos;
                ret = false;
            }
        }

        if (cursorPos.x > deathCam.transform.position.x + _1080pWidth - 0.2f)
        {
            ret = false;
            scrollingR = true;
        }
        if (cursorPos.x < deathCam.transform.position.x - _1080pWidth)
        {
            ret = false;
            scrollingL = true;
        }
        if (cursorPos.z > deathCam.transform.position.z + _1080pHeight)
        {
            ret = false;
            scrollingU = true;      
        }
        if (cursorPos.z < deathCam.transform.position.z - _1080pHeight + 0.3f)
        {
            ret = false;
            scrollingD = true;
        }
        return ret;
    }
}

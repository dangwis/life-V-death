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
    public GameObject teleportPlacementPrefab;
    public GameObject gruntPlacementPrefab;
    public GameObject minotaurPrefab;
    public GameObject teleporterPrefab;
    public GameObject gruntSpawnPrefab;
    public GameObject slowPlacementPrefab;
    public GameObject slowTrapPrefab;
    public Material ableToPlace, notAbleToPlace;
    public Material firstTeleporterPlace;
    public float movementSpeed;
    public Camera deathCam;
    public float totalMana;
    public float timeToRegen, manaRegenRate;
    public float manaLeft;
	public float[] manaCosts = { 50f, 35f, 40f, 25f, 40f, 30f }; //[gruntSpawn, Skeleton, Minotaur, Spike Trap, Teleporter, Shrooms]
    float timeSinceLastUse;
    public float _1080pWidth, _1080pHeight;
    public float scrollSpeed;
    public int totalTrapAllowed;
    public int totalBigEnemyAllowed;
    public int totalSpawnerAllowed;
    public int curTrap, curBigEn, curSpawner;

    Vector3 teleportIntermediary;

    bool place;
    GameObject placement;
    AbilityType activeAbility;
    Placing currentPlacing;

    public bool scrollingL, scrollingR, scrollingU, scrollingD;
    bool wEnabled, sEnabled, aEnabled, dEnabled;

    public enum AbilityType
    {
        Interact,
        Place
    }

    public enum Placing
    {
        Skeleton,
        Minotaur,
        GruntSpawn,
        Damage,
        Slow,
        Teleport1,
        Teleport2
    }

	// Use this for initialization
	void Start () {
        S = this;
        totalMana *= (SetupCameras.PlayerCount - 1);
        Cursor.lockState = lockState;
        Cursor.visible = false;
        deathCursor = Instantiate(deathCursor);
        Vector3 tempPos = GameObject.Find("Static Objects").GetComponent<SetupCameras>().deathCamStart;
        tempPos.y = 5.6f;
        cursorPos = tempPos;
        deathCursor.transform.position = cursorPos;
        activeAbility = AbilityType.Interact;
        SetAllScrollsFalse();
        wEnabled = true;
        sEnabled = true;
        aEnabled = true;
        dEnabled = true;
        curBigEn = 0;
        curTrap = 0;
        curSpawner = 0;
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

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && dEnabled)
        {
            newCamPos.x += scrollSpeed;
            cursorPos.x += scrollSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && aEnabled)
        {
            newCamPos.x -= scrollSpeed;
            cursorPos.x -= scrollSpeed;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) && wEnabled)
        {
            newCamPos.z += scrollSpeed;
            cursorPos.z += scrollSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && sEnabled)
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

        if (deathCursor.transform.position.x > 85)
        {
            Vector3 curbound = deathCursor.transform.position;
            curbound.x = 85;
            deathCursor.transform.position = curbound;
            SetAllScrollsFalse();
            dEnabled = false;
        }
        else if(deathCursor.transform.position.x != 85)
        {
            dEnabled = true;
        }

        if (deathCursor.transform.position.x < 0)
        {
            Vector3 curbound = deathCursor.transform.position;
            curbound.x = 0;
            deathCursor.transform.position = curbound;
            SetAllScrollsFalse();
            aEnabled = false;
        }
        else if (deathCursor.transform.position.x != 0)
        {
            aEnabled = true;
        }

        if (deathCursor.transform.position.z > 0)
        {
            Vector3 curbound = deathCursor.transform.position;
            curbound.z = 0;
            deathCursor.transform.position = curbound;
            SetAllScrollsFalse();
            wEnabled = false;
        }
        else if (deathCursor.transform.position.z != 0)
        {
            wEnabled = true;
        }

        if (deathCursor.transform.position.z < -85)
        {
            Vector3 curbound = deathCursor.transform.position;
            curbound.z = -85;
            deathCursor.transform.position = curbound;
            SetAllScrollsFalse();
            sEnabled = false;
        }
        else if (deathCursor.transform.position.z != -85)
        {
            sEnabled = true;
        }

    }

    void SetAllScrollsFalse()
    {
        scrollingR = false;
        scrollingU = false;
        scrollingL = false;
        scrollingD = false;
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

    public void DecrementBigEnemy()
    {
        curBigEn--;
    }

    public void DecrementTrap()
    {
        curTrap--;
    }

    public void DecrementSpawner()
    {
        curSpawner--;
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
                    if(currentPlacing == Placing.GruntSpawn)
                    {
                        if (manaLeft >= manaCosts[0] && curSpawner < totalSpawnerAllowed)
                        {
                            UseMana(manaCosts[0]);
                            GameObject spawn = Instantiate(gruntSpawnPrefab);
                            spawn.transform.position = placement.transform.position;
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                            curSpawner++;
                        }
                        else
                        {
                            //show that not enough mana somehow
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                        }
                        DeathHUD.inst.deselectAllAbilities();
                    }
                    if(currentPlacing == Placing.Damage)
                    {
                        if (manaLeft >= manaCosts[3] && curTrap < totalTrapAllowed)
                        {
							UseMana(manaCosts[3]);
                            GameObject trap = Instantiate(damageTrapPrefab);
                            trap.transform.position = placement.transform.position;
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                            curTrap++;
                        }
                        else
                        {
                            //show that not enough mana somehow
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                        }
                        DeathHUD.inst.deselectAllAbilities();
                    }
                    else if(currentPlacing == Placing.Skeleton)
                    {
						if (manaLeft >= manaCosts[1] && curBigEn < totalBigEnemyAllowed)
                        {
							UseMana(manaCosts[1]);
							EnemySkel skel = Instantiate(skeletonPrefab).transform.FindChild("Skeleton").GetComponent<EnemySkel>();
                            skel.transform.position = placement.transform.position + new Vector3(0, 1.6f, 0);
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                            curBigEn++;
                        }
                        else
                        {
                            //show that not enough mana
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                        }
                        DeathHUD.inst.deselectAllAbilities();
                    }
                    else if(currentPlacing == Placing.Minotaur)
                    {
						if (manaLeft >= manaCosts[2] && curBigEn < totalBigEnemyAllowed)
                        {
							UseMana(manaCosts[2]);
							EnemyMin min = Instantiate(minotaurPrefab).transform.FindChild("Minotaur").GetComponent<EnemyMin>();
                            min.transform.position = placement.transform.position;
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                            curBigEn++;
                        }
                        else
                        {
                            //show that not enough mana
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                        }
                        DeathHUD.inst.deselectAllAbilities();
                    }
                    else if (currentPlacing == Placing.Slow)
                    {
						if (manaLeft >= manaCosts[5] && curTrap < totalTrapAllowed)
                        {
							UseMana(manaCosts[5]);
                            SlowTrap st = Instantiate(slowTrapPrefab).GetComponent<SlowTrap>();
                            st.transform.position = placement.transform.position;
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                            curTrap++;
                        }
                        else
                        {
                            //show that not enough mana
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                        }
                        DeathHUD.inst.deselectAllAbilities();
                    }
                    else if(currentPlacing == Placing.Teleport1)
                    {
                        teleportIntermediary = placement.transform.position;
                        currentPlacing = Placing.Teleport2;
                    }
                    else if(currentPlacing == Placing.Teleport2)
                    {
						if (manaLeft >= manaCosts[4] && curTrap < totalTrapAllowed)
                        {
							UseMana(manaCosts[4]);
                            TeleportPad tp = Instantiate(teleporterPrefab).GetComponent<TeleportPad>();
                            tp.transform.position = teleportIntermediary;
                            tp.endingUpPosition = placement.transform.position;
                            tp.ShowEndPos();
                            activeAbility = AbilityType.Interact;
                            Destroy(placement.gameObject);
                            curTrap++;
                        }
                        else
                        {
                            //show that not enough mana
                            Destroy(placement.gameObject);
                            activeAbility = AbilityType.Interact;
                        }
                        DeathHUD.inst.deselectAllAbilities();
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
            if (hit.collider.tag == "Floor" && NotNearTag(placement, "Life", 3f) && NotNearTag(placement, "Trap", 0.5f) && NotNearTag(placement, "Wall", 0.5f))
            {
                if (!GameManager.S.gameStart && WithinStartingRoom(pos))
                {
                    if (currentPlacing == Placing.Damage || currentPlacing == Placing.GruntSpawn || currentPlacing == Placing.Slow)
                    {
                        foreach (Transform child in placement.transform)
                            child.GetComponent<Renderer>().material = notAbleToPlace;
                    }
                    else
                    {
                        placement.GetComponent<Renderer>().material = notAbleToPlace;
                    }
                    place = false;
                }
                else
                {
                    if (currentPlacing == Placing.Teleport1)
                    {
                        placement.GetComponent<Renderer>().material = firstTeleporterPlace;
                    }
                    else if (currentPlacing == Placing.Damage || currentPlacing == Placing.GruntSpawn || currentPlacing == Placing.Slow)
                    {
                        foreach (Transform child in placement.transform)
                            child.GetComponent<Renderer>().material = ableToPlace;
                    }
                    else
                    {
                        placement.GetComponent<Renderer>().material = ableToPlace;
                    }
                    place = true;
                }
            }
            else
            {
                if (currentPlacing == Placing.Damage || currentPlacing == Placing.GruntSpawn || currentPlacing == Placing.Slow)
                {
                    foreach (Transform child in placement.transform)
                        child.GetComponent<Renderer>().material = notAbleToPlace;
                }
                else
                {
                    placement.GetComponent<Renderer>().material = notAbleToPlace;
                }
                place = false;
            }
        }
    }

    bool WithinStartingRoom(Vector3 pos)
    {
        if (pos.x > 50 || pos.x < 32) return false;
        if (pos.z < -50 || pos.z > -32) return false;
        return true;
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
            if (activeAbility == AbilityType.Interact)
            {
                placement = Instantiate(gruntPlacementPrefab);
                activeAbility = AbilityType.Place;
                currentPlacing = Placing.GruntSpawn;
                DeathHUD.inst.selectAbility(1);
            }
            else if (activeAbility == AbilityType.Place && currentPlacing == Placing.GruntSpawn)
            {
                activeAbility = AbilityType.Interact;
                Destroy(placement.gameObject);
                DeathHUD.inst.deselectAllAbilities();
            }
            else
            {
                Destroy(placement.gameObject);
                placement = Instantiate(gruntPlacementPrefab);
                activeAbility = AbilityType.Place;
                currentPlacing = Placing.GruntSpawn;
                DeathHUD.inst.selectAbility(1);
            }
			
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (activeAbility == AbilityType.Interact) {
                placement = Instantiate(trapPlacementPrefab);
                activeAbility = AbilityType.Place;
                currentPlacing = Placing.Damage;
				DeathHUD.inst.selectAbility (4);
            }
            else if(activeAbility == AbilityType.Place && currentPlacing == Placing.Damage)
            {
                activeAbility = AbilityType.Interact;
                Destroy(placement.gameObject);
				DeathHUD.inst.deselectAllAbilities ();
            }
            else
            {
                Destroy(placement.gameObject);
                placement = Instantiate(trapPlacementPrefab);
                activeAbility = AbilityType.Place;
                currentPlacing = Placing.Damage;
				DeathHUD.inst.selectAbility (4);
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (activeAbility == AbilityType.Interact)
            {
                placement = Instantiate(enemyPlacementPrefab);
                activeAbility = AbilityType.Place;
                currentPlacing = Placing.Skeleton;
				DeathHUD.inst.selectAbility (2);
            }
            else if (activeAbility == AbilityType.Place && currentPlacing == Placing.Skeleton)
            {
                activeAbility = AbilityType.Interact;
                Destroy(placement.gameObject);
				DeathHUD.inst.deselectAllAbilities ();
            }
            else
            {
                Destroy(placement.gameObject);
                placement = Instantiate(enemyPlacementPrefab);
                activeAbility = AbilityType.Place;
                currentPlacing = Placing.Skeleton;
				DeathHUD.inst.selectAbility (2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (activeAbility == AbilityType.Interact)
            {
                placement = Instantiate(enemyPlacementPrefab);
                activeAbility = AbilityType.Place;
                currentPlacing = Placing.Minotaur;
				DeathHUD.inst.selectAbility (3);
            }
            else if (activeAbility == AbilityType.Place && currentPlacing == Placing.Minotaur)
            {
                activeAbility = AbilityType.Interact;
                Destroy(placement.gameObject);
				DeathHUD.inst.deselectAllAbilities ();
            }
            else
            {
                Destroy(placement.gameObject);
                placement = Instantiate(enemyPlacementPrefab);
                activeAbility = AbilityType.Place;
                currentPlacing = Placing.Minotaur;
				DeathHUD.inst.selectAbility (3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (activeAbility == AbilityType.Interact)
            {
                placement = Instantiate(teleportPlacementPrefab);
                activeAbility = AbilityType.Place;
                currentPlacing = Placing.Teleport1;
				DeathHUD.inst.selectAbility (5);
            }
            else if (activeAbility == AbilityType.Place && (currentPlacing == Placing.Teleport1 || currentPlacing == Placing.Teleport2))
            {
                activeAbility = AbilityType.Interact;
                Destroy(placement.gameObject);
				DeathHUD.inst.deselectAllAbilities ();
            }
            else
            {
                Destroy(placement.gameObject);
                placement = Instantiate(teleportPlacementPrefab);
                activeAbility = AbilityType.Place;
                currentPlacing = Placing.Teleport1;
				DeathHUD.inst.selectAbility (5);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (activeAbility == AbilityType.Interact)
            {
                placement = Instantiate(slowPlacementPrefab);
                activeAbility = AbilityType.Place;
                currentPlacing = Placing.Slow;
                DeathHUD.inst.selectAbility(6);
            }
            else if (activeAbility == AbilityType.Place && currentPlacing == Placing.Slow)
            {
                activeAbility = AbilityType.Interact;
                Destroy(placement.gameObject);
                DeathHUD.inst.deselectAllAbilities();
            }
            else
            {
                Destroy(placement.gameObject);
                placement = Instantiate(slowPlacementPrefab);
                activeAbility = AbilityType.Place;
                currentPlacing = Placing.Slow;
                DeathHUD.inst.selectAbility(6);
            }
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

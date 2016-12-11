using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager S;

    public float setupTime;
    public float startTime;
    public float showFountainTime, showLifeTime, numKeysTime, scrollTextTime, tabTime, endGo;
    public bool gameStart, endFountain, endShowLife;
    bool transitionStarted;
    Vector3 targetPlayer;

	// Use this for initialization
	void Start () {
        S = this;
        startTime = Time.time;
        gameStart = false;
        endShowLife = false;
        endFountain = false;
        transitionStarted = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - startTime > setupTime)
        {
            gameStart = true;
            DeathHUD.inst.countdownToStart.SetActive(false);
        }
        if(Time.time - startTime > showFountainTime)
        {
            endFountain = true;
            DeathHUD.inst.fountainText.SetActive(false);
            DeathHUD.inst.killPlayersText.SetActive(true);
            if (!transitionStarted) {
                transitionStarted = true;
                targetPlayer = Death.S.deathCam.transform.position;
                targetPlayer.x = 43f;
                targetPlayer.z = -47f;
                StartCoroutine(Transition());
            }
            
        }
        if(Time.time - startTime > showLifeTime)
        {
            endShowLife = true;
            DeathHUD.inst.killPlayersText.SetActive(false);
            DeathHUD.inst.scrollText.SetActive(true);
        }
        if(Time.time - startTime > scrollTextTime)
        {
            DeathHUD.inst.scrollText.SetActive(false);
            DeathHUD.inst.useNumKeysText.SetActive(true);
        }
        if(Time.time - startTime > numKeysTime)
        {
            DeathHUD.inst.useNumKeysText.SetActive(false);
            DeathHUD.inst.tabText.SetActive(true);
        }
        if(Time.time - startTime > tabTime)
        {
            DeathHUD.inst.tabText.SetActive(false);
        }
        UpdateCountDown();
    }

    void UpdateCountDown()
    {
        DeathHUD.inst.countdownToStart.GetComponent<Text>().text = "Game Start: " + (int)(setupTime - (Time.time - startTime));
    }

    IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startPos = Death.S.deathCam.transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / 2.0f);
            Death.S.deathCam.transform.position = Vector3.Lerp(startPos, targetPlayer, t);
            Vector3 cursor = Death.S.deathCam.transform.position;
            cursor.y = Death.S.cursorPos.y;
            Death.S.deathCursor.transform.position = cursor;
            yield return 0;
        }
    }
}

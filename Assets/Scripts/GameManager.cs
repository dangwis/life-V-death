using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager S;

    public float setupTime;
    float startTime;
    public float showFountainTime, showLifeTime;
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
                targetPlayer.z = -43f;
                StartCoroutine(Transition());
            }
            
        }
        if(Time.time - startTime > showLifeTime)
        {
            endShowLife = true;
            DeathHUD.inst.killPlayersText.SetActive(false);

        }
        
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

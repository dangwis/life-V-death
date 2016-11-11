using UnityEngine;
using System.Collections;

public class DeathCursor : MonoBehaviour {

    public static DeathCursor S;
    public Sprite onClick, onRelease;
    SpriteRenderer spRend;

	// Use this for initialization
	void Start () {
        S = this;
        spRend = transform.Find("Sprite").GetComponent<SpriteRenderer>();
	}
	
	public void OnClick()
    {
        spRend.sprite = onClick;
    }

    public void OnRelease()
    {
        spRend.sprite = onRelease;
    }
}

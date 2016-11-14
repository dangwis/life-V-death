using UnityEngine;
using System.Collections;

public class FallingBlock : MonoBehaviour {

    bool onGround, dropping;
    public int damageToPlayer;
    int groundLayerMask;

	// Use this for initialization
	void Start () {
        onGround = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y <= 1) onGround = true;
	}

    public void DropBlocks()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;
        dropping = true;
        Collider[] otherBlocks = Physics.OverlapSphere(this.transform.position, 2f);
        for(int i = 0; i < otherBlocks.Length; i++)
        {
            if(otherBlocks[i].tag == "Clickable")
            {
                otherBlocks[i].gameObject.GetComponent<FallingBlock>().DropOthers();
            }
        }
    }

    void DropOthers()
    {
        dropping = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        transform.Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject go = coll.gameObject;
        if (!onGround)
        {
            if (go.tag == "Life")
            {
                go.GetComponent<LifePlayer>().health -= damageToPlayer;
                go.GetComponent<LifePlayer>().state = 2;
                Destroy(this.gameObject);
            }
        }
    }
}

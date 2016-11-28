using UnityEngine;
using System.Collections;

public class FallingBlock : MonoBehaviour {

    bool onGround;
    public int damageToPlayer;
    public float lifespan;
    float startOnGround;

	// Use this for initialization
	void Start () {
        onGround = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y <= 1 && !onGround)
        {
            onGround = true;
            startOnGround = Time.time;
        }
        if (onGround)
        {
            if(Time.time - startOnGround > lifespan)
            {
                Destroy(this.gameObject);
            }
        }
	}

    public void DropBlocks()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        
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
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
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

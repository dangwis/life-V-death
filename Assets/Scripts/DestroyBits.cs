using UnityEngine;
using System.Collections;

public class DestroyBits : MonoBehaviour {

    // Use this for initialization
    void Start() {
        Invoke("DestroyIt", 3f);
    }

    void DestroyIt() {
        Destroy(this.gameObject);
    }
}

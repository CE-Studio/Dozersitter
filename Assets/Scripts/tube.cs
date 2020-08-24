using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tube : MonoBehaviour {
    void OnCollisionStay(Collision collision) {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 50, -50));
    }
}

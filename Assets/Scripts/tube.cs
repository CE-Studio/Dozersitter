using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tube : MonoBehaviour {
    void OnCollisionEnter(Collision collision) {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 100, -200));
    }
}

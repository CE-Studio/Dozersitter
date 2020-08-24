using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int thisBox;
    public int durabillity = 5;
    public GameObject newBroken;

    void Start()
    {
        thisBox = BoxList.activeBoxes;
        BoxList.activeBoxes++;
        BoxList.boxID.Add(thisBox, "Usable");
    }

    void Update()
    {
        if (transform.position.y < -5) {
            transform.position = new Vector3(0, 5, 0);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Dozer") {
            durabillity--;
        }
        if (durabillity == 0) {
            durabillity--;
            Pickup.boxes--;
            GameObject broken = Instantiate(newBroken, transform.position, transform.rotation) as GameObject;
            transform.position = new Vector3(0, 200, 0);
            Destroy(gameObject, 3);
        }
    }
}

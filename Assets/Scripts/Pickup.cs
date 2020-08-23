using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public static bool isHolding = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            RaycastHit hit;
            if (isHolding) {

            } else {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)) {
                    if (hit.collider.gameObject.tag == "Dozer") {
                        print(hit.collider.gameObject.GetComponent<DozerAI>().dozerMood);
                        hit.collider.gameObject.transform.position = new Vector3(-22.5f, 0.5f, 0.5f);
                        isHolding = true;
                    } else if (hit.collider.gameObject.tag == "Box") {
                        isHolding = true;
                        hit.collider.gameObject.transform.position = new Vector3(-22.5f, 0.5f, 0.5f);
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public static bool isHolding = false;
    public static string lastGrabbed = "Dozer";
    public static string mood = "Happy";
    bool clicked = true;
    public GameObject placepoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && clicked) {
            clicked = false;
            RaycastHit hit;
            if (isHolding) {
                if (Physics.Raycast(new Vector3(-22.5f, 3f, 0.5f), Vector3.down, out hit)) {
                    if (hit.collider.gameObject.tag == "Dozer") {
                        mood = hit.collider.gameObject.GetComponent<DozerAI>().dozerMood;
                        lastGrabbed = "Dozer";
                        hit.collider.gameObject.transform.position = placepoint.transform.position;
                        isHolding = false;
                    } else if (hit.collider.gameObject.tag == "Box") {
                        lastGrabbed = "Box";
                        isHolding = false;
                        hit.collider.gameObject.transform.position = placepoint.transform.position;
                    }
                }
            } else {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)) {
                    if (hit.collider.gameObject.tag == "Dozer") {
                        mood = hit.collider.gameObject.GetComponent<DozerAI>().dozerMood;
                        lastGrabbed = "Dozer";
                        hit.collider.gameObject.transform.position = new Vector3(-22.5f, 0.5f, 0.5f);
                        isHolding = true;
                    } else if (hit.collider.gameObject.tag == "Box") {
                        lastGrabbed = "Box";
                        isHolding = true;
                        hit.collider.gameObject.transform.position = new Vector3(-22.5f, 0.5f, 0.5f);
                    }
                }
            }
        } else if (!Input.GetMouseButton(0)) {
            clicked = true;
        }
    }
}

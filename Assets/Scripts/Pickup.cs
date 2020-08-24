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
    public GameObject box;
    public Animator heldObject;
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
                if (Physics.Raycast(new Vector3(-22.5f, 3f, 0.5f), Vector3.down, out hit, 5f)) {
                    if (hit.collider.gameObject.tag == "Dozer") {
                        mood = hit.collider.gameObject.GetComponent<DozerAI>().dozerMood;
                        lastGrabbed = "Dozer";
                        hit.collider.gameObject.transform.position = placepoint.transform.position;
                        isHolding = false;
                        heldObject.SetInteger("DozerMood", 0);
                    } else if (hit.collider.gameObject.tag == "Box") {
                        lastGrabbed = "Box";
                        isHolding = false;
                        heldObject.SetBool("CarryingBox", false);
                        hit.collider.gameObject.transform.position = placepoint.transform.position;
                    } 
                }
            } else {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f)) {
                    if (hit.collider.gameObject.tag == "Dozer") {
                        mood = hit.collider.gameObject.GetComponent<DozerAI>().dozerMood;
                        lastGrabbed = "Dozer";
                        hit.collider.gameObject.transform.position = new Vector3(-22.5f, 0.5f, 0.5f);
                        isHolding = true;
                        switch (mood)
                        {
                            case "Excited":
                                heldObject.SetInteger("DozerMood", 3);
                                break;
                            case "Happy":
                                heldObject.SetInteger("DozerMood", 3);
                                break;
                            case "Neutral":
                                heldObject.SetInteger("DozerMood", 2);
                                break;
                            case "Unhappy":
                                heldObject.SetInteger("DozerMood", 1);
                                break;
                            case "Angry":
                                heldObject.SetInteger("DozerMood", 1);
                                break;
                        }
                    } else if (hit.collider.gameObject.tag == "Box") {
                        lastGrabbed = "Box";
                        isHolding = true;
                        heldObject.SetBool("CarryingBox", true);
                        hit.collider.gameObject.transform.position = new Vector3(-22.5f, 0.5f, 0.5f);
                    } else if (hit.collider.gameObject.tag == "AddButton") {
                        GameObject newBox = Instantiate(box, new Vector3(10.88f, 8, 23.94f), transform.rotation) as GameObject;
                    }
                }
            }
        } else if (!Input.GetMouseButton(0)) {
            clicked = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int thisBox;

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
}

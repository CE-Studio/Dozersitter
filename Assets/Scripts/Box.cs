﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    int thisBox;

    void Start()
    {
        thisBox = BoxList.activeBoxes;
        BoxList.activeBoxes++;
        BoxList.boxID.Add(thisBox, "Usable");
    }

    void Update()
    {
        
    }
}

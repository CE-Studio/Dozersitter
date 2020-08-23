using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenSlider : MonoBehaviour
{
    public void ChangeSensitivity(float newSen)
    {
        GlobalVarTracker.mouseSensitivity = newSen;
    }
}

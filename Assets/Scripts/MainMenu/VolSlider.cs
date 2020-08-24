using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolSlider : MonoBehaviour
{
    public void ChangeVolume(float newVol)
    {
        GlobalVarTracker.volume = newVol;
    }
}

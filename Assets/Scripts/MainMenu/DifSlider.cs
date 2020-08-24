using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifSlider : MonoBehaviour
{
    public void ChangeDifficulty(float newDif)
    {
        GlobalVarTracker.difficulty = newDif;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public AudioSource music;
    
    void Start()
    {
        music.volume = GlobalVarTracker.volume / 100;
    }
}

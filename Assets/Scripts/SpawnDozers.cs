using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDozers : MonoBehaviour
{
    int dozersToSpawn;
    public GameObject dozer;
    
    void Start()
    {
        switch (GlobalVarTracker.difficulty)
        {
            case 1:
                dozersToSpawn = 6;
                break;
            case 2:
                dozersToSpawn = 9;
                break;
            case 3:
                dozersToSpawn = 12;
                break;
        }

        while (dozersToSpawn > 0)
        {
            GameObject Dozer = Instantiate(dozer, new Vector3(Random.Range(-15, 15), 0.5f, Random.Range(-25, 25)), new Quaternion(0, Random.Range(-180, 180), 0, 1)) as GameObject;
            dozersToSpawn--;
        }
    }
}

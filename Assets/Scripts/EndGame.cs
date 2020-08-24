using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    bool gameEnded;
    int countdown = 10;
    
    public void StopGame()
    {
        gameEnded = true;
        DozerAI.gameEnded = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void FixedUpdate()
    {
        if (gameEnded && (GlobalVarTracker.endMoods.Contains(1) || GlobalVarTracker.endMoods.Contains(2) || GlobalVarTracker.endMoods.Contains(3) || GlobalVarTracker.endMoods.Contains(4) || GlobalVarTracker.endMoods.Contains(5)))
        {
            countdown--;
            if (countdown <= 0)
            {
                SceneManager.LoadScene("EndScreen");
            }
        }
    }
}

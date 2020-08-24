using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Results : MonoBehaviour
{
    int posInList;
    int excited;
    int happy;
    int neutral;
    int unhappy;
    int angry;
    public Text Excited;
    public Text Happy;
    public Text Neutral;
    public Text Unhappy;
    public Text Angry;
    public Text Final;
    
    void Start()
    {
        while (posInList < 3 + 3 * GlobalVarTracker.difficulty)
        {
            switch (GlobalVarTracker.endMoods[posInList])
            {
                case 5:
                    excited++;
                    break;
                case 4:
                    happy++;
                    break;
                case 3:
                    neutral++;
                    break;
                case 2:
                    unhappy++;
                    break;
                case 1:
                    angry++;
                    break;
            }
            posInList++;
        }

        Excited.text = ("" + excited);
        Happy.text = ("" + happy);
        Neutral.text = ("" + neutral);
        Unhappy.text = ("" + unhappy);
        Angry.text = ("" + angry);
        Final.text = ((excited + happy + neutral + unhappy + angry) + "/" + ((3 + 3 * GlobalVarTracker.difficulty) * 5));
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}

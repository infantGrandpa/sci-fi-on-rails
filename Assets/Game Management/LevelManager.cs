using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public string firstLevelName;
    public float secondsBeforeNextLevel;
    public float graceTimeAtEndOfLevel;

    public float secondsBeforeShowingDeathMenu;

    bool shownDeathMenu;

    private void Awake()
    {
        References.theLevelManager = this;
    }

    void Start()
    {
        SceneManager.LoadScene(firstLevelName);
        secondsBeforeNextLevel = graceTimeAtEndOfLevel;
        shownDeathMenu = false;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Startup");
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        /*//If all enemies are dead,
        if (References.allEnemies.Count < 1)
        {
            //Wait a bit
            secondsBeforeNextLevel -= Time.deltaTime;

            //Stop alarm
            References.alarmManager.StopTheAlarm();

            if (secondsBeforeNextLevel <= 0)
            {
                // go to the next level
                SceneManager.LoadScene(References.levelGenerator.nextLevelName);
            }
        }
        else
        {
            //Not all enemies are dead
            secondsBeforeNextLevel = graceTimeAtEndOfLevel;
        }*/

        if (References.thePlayer == null && shownDeathMenu == false)
        {
            secondsBeforeShowingDeathMenu -= Time.deltaTime;
            if (secondsBeforeShowingDeathMenu <= 0)
            {
                References.theCanvas.ShowMainMenu();
                shownDeathMenu = true;
            }
        }
    }

}

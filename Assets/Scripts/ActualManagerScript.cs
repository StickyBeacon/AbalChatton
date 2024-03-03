using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualManagerScript : MonoBehaviour
{
    GameScript gameScript;
    CannonManager cannonManager;
    CannonScript cannonScript;
    bool hasStarted = false;
    bool talking = false;
    bool paused = false;
    private void Start()
    {
        gameScript = GameObject.Find("GameManager").GetComponent<GameScript>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasStarted && !talking && !paused)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        //Remove all scoreshit
        gameScript.setActive();
        hasStarted = true;
    }

    public void EndGame()
    {
        hasStarted = false;
        gameScript.loseGame();
        cannonManager.Regenerate();
        cannonScript.clearQueue();
    }
}

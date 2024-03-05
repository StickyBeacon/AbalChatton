using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualManagerScript : MonoBehaviour
{
    GameScript gameScript;
    CannonManager cannonManager;
    CannonScript cannonScript;
    ScoreScript scoreScript;
    bool hasStarted = false;
    bool talking = false;
    bool paused = false;
    private void Start()
    {
        gameScript = GameObject.Find("GameManager").GetComponent<GameScript>();
        cannonManager = GameObject.Find("CannonBody").GetComponent<CannonManager>();
        cannonScript = GameObject.Find("CannonShooter").GetComponent<CannonScript>();
        scoreScript = GameObject.Find("ScoreManager").GetComponent<ScoreScript>();
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
        scoreScript.hideScore();
        gameScript.setActive();
        hasStarted = true;
    }

    public void EndGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        hasStarted = false;
        gameScript.loseGame();
        cannonManager.Regenerate();
        cannonScript.clearQueue();
        scoreScript.displayScore();
    }
}

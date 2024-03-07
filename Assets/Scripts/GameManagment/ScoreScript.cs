using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    int score;
    public GameObject scorePanel;
    public GameObject scoreText;
    public GameScript gScript;
    int wave = 0;

    private void FixedUpdate()
    {
        IEnumerator waitabit (){
            if (!scorePanel.activeSelf)
            {
                yield return new WaitForSeconds(1f);
                wave = gScript.getWaves();
            }
        }
        StartCoroutine(waitabit());
    }

    public void AddScore(int i)
    {
        score += i;
    }

    public int GetScore()
    {
        return score;
    }

    public void displayScore()
    {
        scorePanel.transform.Find("Wave").GetComponent<Text>().text = "Wave : " + wave;
        scoreText.GetComponent<Text>().text = "Score: " + score;

        scorePanel.SetActive(true);
    }

    public void hideScore()
    {
        scorePanel.SetActive(false);
    }
}

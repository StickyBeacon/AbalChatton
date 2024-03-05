using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    int score;
    public GameObject scorePanel;
    public GameObject scoreText;
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
        scoreText.GetComponent<Text>().text = "Score: " + score;
        scorePanel.SetActive(true);
    }

    public void hideScore()
    {
        scorePanel.SetActive(false);
    }
}

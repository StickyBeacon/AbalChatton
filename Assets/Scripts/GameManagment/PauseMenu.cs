using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    bool isLoading = false;

    public GameObject transitionElement;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isLoading)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        if (!isLoading)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
        
    }

    public void loadMenu()
    {
        if (!isLoading)
        {
            GameIsPaused = false;
            isLoading = true;
            Time.timeScale = 1f;
            IEnumerator startGame()
            {

                transitionElement.GetComponent<TransitionScript>().transOut();
                yield return new WaitForSeconds(1.2f);
                SceneManager.LoadScene("Menu");
            }
            StartCoroutine(startGame());
        }
        
        

    }

    public void QuitGame()
    {
        if (!isLoading)
        {
            print("quit!!");
            Application.Quit();
        }
        
    }

    void Pause()
    {
        if (!isLoading)
        {
            if (GameObject.Find("SuckPortal") != null)
            {
                GameObject.Find("SuckPortal").SetActive(false);
            }
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        

    }

    //code from https://www.youtube.com/watch?v=JivuXdrIHK0
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuScript : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject ButtonsPanel;
    public AudioMixer audioMixer;

    public GameObject TransitionElement;
    bool isLoading = false;

    public Dropdown resolutionDropDown;

    float timeForNextBall = 0f;
    int ballAmount = 0;

    public GameObject miniPortal;

    private void FixedUpdate()
    {
        timeForNextBall -= Time.fixedDeltaTime;
        if (timeForNextBall <= 0f && ballAmount<15)
        {
            timeForNextBall = Random.Range(1f, 4f);
            ballAmount += 1;
            float RandDeg = Random.Range(0f, 360f);
            Vector3 pos = new Vector3(10f / 1.2f * Mathf.Cos(RandDeg), 10f / 1.2f * Mathf.Sin(RandDeg), 10);
            Vector3 offset = new Vector3(0, 0, 10);
            GameObject portal = Instantiate(miniPortal, pos + offset, Quaternion.identity);
        }
    }

    public void Settings()
    {
        if (!isLoading)
        {
            print("activate them settings!");
            ButtonsPanel.SetActive(false);
            SettingsPanel.SetActive(true);
        }
    }

    Resolution[] resolutions;

    public void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        int currentResolutionIndex = 0;

        List<string> options = new List<string>();
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        } 

        resolutionDropDown.AddOptions(options);

        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SettingsGone()
    {
        ButtonsPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void loadGame()
    {
        if (!isLoading)
        {
            TransitionElement.GetComponent<TransitionScript>().transOut();
            isLoading = true;
            IEnumerator startGame()
            {
                yield return new WaitForSeconds(1.2f);
                SceneManager.LoadScene("Game");
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

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void setFullScreen(bool value)
    {
        Screen.fullScreen = value;
    }
}
